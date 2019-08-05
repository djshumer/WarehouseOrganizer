using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WarehouseOrganizer.MobileAppService.Models
{
    public abstract class BaseEntitySimpleModel
    {
        [Key]
        public long Id { get; set; }
    }
}
