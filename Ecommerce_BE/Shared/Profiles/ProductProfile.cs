using AutoMapper;
using Ecommerce_BE.Models;
using Ecommerce_BE.Services.StoreService.Dtos.Product;
using Ecommerce_BE.Shared.Files;
using Ecommerce_BE.Shared.TIme;

namespace Ecommerce_BE.Shared.Profiles
{
  public class ProductProfile :Profile
  {
    private readonly IFileService _fileService;
    public ProductProfile(IFileService fileService) // Inject IFileService trực tiếp
    {

      _fileService = fileService;

      CreateMap<dtoProduct, Product>()
          .ForMember(dest => dest.Image, opt =>
              opt.MapFrom(src => _fileService.UploadFileAsync(src.Image)))
           .ForMember(dest => dest.CreateTime, opt =>
              opt.MapFrom(src => DateTime.UtcNow));

      CreateMap<Product, dtoProductResponse>()
         .ForMember(dest => dest.CreateTime, opt =>
              opt.MapFrom(src => Time.GetTimeToString(src.CreateTime)))
         .ForMember(dest => dest.Image, opt =>
              opt.MapFrom(src => _fileService.SetCoverImage(src.Image)));
    }

  }
}
