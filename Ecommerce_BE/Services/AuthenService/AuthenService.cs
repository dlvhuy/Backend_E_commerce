using Ecommerce_BE.Models;
using Ecommerce_BE.Repository;
using Ecommerce_BE.Repository.Abstractions;
using Ecommerce_BE.Services.AuthenService.Dto;
using Ecommerce_BE.Shared.Caching;
using Ecommerce_BE.Shared.Exceptions.UserExceptions;
using Ecommerce_BE.Shared.Secure.Bcrypt;
using Ecommerce_BE.Shared.Secure.Jwt.JwtConfiguration;
using Ecommerce_BE.Shared.Secure.Jwt.JwtService;
using Microsoft.Win32;
using System.Security.Claims;

namespace Ecommerce_BE.Services.AuthenService
{
  public class AuthenService : IAuthenService
  {
    private readonly IJwtService _jwtService;
    private readonly UserRepository _userRepository;
    private readonly IPasswordService _passwordService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICachingService<string> _cachingService;
    public AuthenService(IJwtService jwtService,
      IPasswordService passwordService,
      IUnitOfWork unitOfWork,
      UserRepository userRepository,
      ICachingService<string> cachingService
      )
    {
      _jwtService = jwtService;
      _userRepository = userRepository;
      _passwordService = passwordService;
      _unitOfWork = unitOfWork;
      _cachingService = cachingService;
    }
    public string Login(DtoLogin login)
    {
      bool isAlreadyHaveEmail = _userRepository.FindItemByCriteria(user => user.Email == login.Email).Any();
      if (!isAlreadyHaveEmail)  throw new UserException.FieldNotExistException(nameof(login.Email));

      var user = _userRepository.GetItemByCriteria(user => user.Email == login.Email);
      bool isMatchPassword = _passwordService.VerifyPassword(user.PasswordHash, login.Password);
      if (!isMatchPassword) throw new UserException.FieldNotMatch(nameof(login.Password)); 

      var claims = new[] 
       {
          new Claim(ClaimRole.Id,user.Id.ToString()),
      };

      string token = _jwtService.GenerateAccessToken(claims);
      _cachingService.SetAsync(user.Id.ToString(), token,TimeSpan.FromMinutes(20));

      return token;

    }

    public void Logout()
    {
      throw new NotImplementedException();
    }

    public bool Register(DtoRegister register)
    {
      
        bool isAlreadyHaveUserName = _userRepository.FindItemByCriteria(user => user.UserName == register.UserName).Any();
        if (isAlreadyHaveUserName) throw new UserException.AlreadyHaveFieldException(nameof(register.UserName), register.UserName);
        bool isAlreadyHaveEmail = _userRepository.FindItemByCriteria(user => user.Email == register.Email).Any();
        if (isAlreadyHaveEmail) throw new UserException.AlreadyHaveFieldException(nameof(register.Email), register.Email);
        bool isAlreadyHavePhoneNumber = _userRepository.FindItemByCriteria(user => user.PhoneNumber == register.PhoneNumber).Any();
        if (isAlreadyHavePhoneNumber) throw new UserException.AlreadyHaveFieldException(nameof(register.PhoneNumber), register.PhoneNumber);

        User newUser = new User()
        {
          UserName = register.UserName,
          Email = register.Email,
          isActive = true,
          isSeller = false,
          PasswordHash = _passwordService.HashPassword(register.Password),
          PhoneNumber = register.PhoneNumber,
        };
        
        
        _unitOfWork.BeginTransaction();
        _userRepository.Add(newUser);
        _unitOfWork.Commit();
        return true;
     



    }
  }
}
