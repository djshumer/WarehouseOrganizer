using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WarehouseOrganizer.MobileAppService.Models;

namespace WarehouseOrganizer.MobileAppService.Controllers
{
    [Route("api/warehouseplace")]
    [ApiController]
    public class WarehousePlacesController: GenericController<WarehousePlace>
    {
       
        public WarehousePlacesController(AppDbContext dbContext) : base(dbContext)
        {
        }

        
    }
}
