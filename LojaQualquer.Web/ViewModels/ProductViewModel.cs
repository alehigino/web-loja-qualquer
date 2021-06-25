using System.Collections.Generic;

namespace LojaQualquer.Web.ViewModels
{
    public class ProductViewModel
    {
        public IList<ProductContent> Content { get; set; }
        public FilterProduct Filter { get; set; }

        public class ProductContent
        {
            public int ProductId { get; set; }
            public string Name { get; set; }
            public int Category { get; set; }
            public decimal Price { get; set; }
        }

        public class FilterProduct
        {
            public string Name { get; set; }
            public string Category { get; set; }
        }
    }
}