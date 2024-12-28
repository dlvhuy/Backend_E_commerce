namespace Ecommerce_BE.Services.StoreService.Dtos.Store
{
    public class dtoStore
    {
        public string Name { get; set; }
        public IFormFile Image { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public int idUser { get; set; }
    }
}
