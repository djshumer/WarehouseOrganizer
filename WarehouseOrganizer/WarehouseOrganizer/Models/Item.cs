using System;
using System.ComponentModel.DataAnnotations;

namespace WarehouseOrganizer.Models
{
    public class Item : BaseEntitySimpleModel
    {
        [MaxLength(150)]
        [Required]
        public String ItemType { get; set; }

        [Required]
        public Double SizeWidth { get; set; }

        [Required]
        public Double SizeHeight { get; set; }

        [Required]
        public Double SizeDepth { get; set; }

        [Required]
        public DateTime DateOfProduction { get; set; }

        public long? WarehousePlaceId {get; set;}

        public WarehousePlace WarehousePlace { get; set; }
          
    }
}
