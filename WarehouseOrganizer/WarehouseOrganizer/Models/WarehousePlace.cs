using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WarehouseOrganizer.Models
{
    public class WarehousePlace : BaseEntitySimpleModel
    {
        [Required]
        [MaxLength(150)]
        public String PlaceName { get; set; }

        [Required]
        public int PlaceColumn { get; set; }

        [Required]
        public int PlaceRow { get; set; }

        public List<Item> ItemsOnPlace { get; set; }
    }
}
