namespace LojaQualquer.Web.Application.Models.Response
{
    public class ProductResponse : BaseResponse
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public int Category { get; set; }
        public decimal Price { get; set; }
    }
}