using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LojaQualquer.Web.ViewModels
{
    public class ProductCreateUpdateViewModel
    {
        [Required(ErrorMessage = "O campo nome é obrigatório")]
        [DisplayName("Nome")]
        public string Name { get; set; }

        [Required(ErrorMessage = "O campo categoria é obrigatório")]
        [DisplayName("Categoria")]
        public int? Category { get; set; }

        [Required(ErrorMessage = "O campo preço é obrigatório")]
        [DisplayName("Preço")]
        public decimal? Price { get; set; }
    }
}