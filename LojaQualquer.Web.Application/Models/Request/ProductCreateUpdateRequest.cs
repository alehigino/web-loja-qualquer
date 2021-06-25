namespace LojaQualquer.Web.Application.Models.Request
{
    public class ProductCreateUpdateRequest
    {
        public string Name { get; set; }
        public int Category { get; set; }
        public decimal Price { get; set; }
    }
}