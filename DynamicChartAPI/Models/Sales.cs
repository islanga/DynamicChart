using System.ComponentModel.DataAnnotations;

namespace DynamicChartAPI.Models
{
    public class Sales
    {
        [Key]
        public int SaleID { get; set; }

        [Required(ErrorMessage = "Sale Month is required")]
        public string SaleMonth { get; set; } = string.Empty;

        [Required(ErrorMessage = "Sale is required")]
        public int Sale { get; set; }
    }
}
