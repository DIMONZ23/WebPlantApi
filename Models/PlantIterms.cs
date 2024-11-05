using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebPlantApi.Models
{
    public class PlantIterms
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Tự động tăng
        public int ID { get; set; }
        public string NamePlant { get; set; }
        public string DescriptionPlant { get; set;}
        public string ImageUrl { get; set;}
        public int PricePlant { get; set;}
        public bool IsTrendy {  get; set; }
    }
}
