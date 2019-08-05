using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WarehouseOrganizer.MobileAppService.Models;

namespace WarehouseOrganizer.MobileAppService.Controllers
{
    [Route("api/item")]
    [ApiController]
    public class ItemsController : GenericController<Item>
    {
        public ItemsController(AppDbContext dbContext) : base(dbContext)
        {
        }

        [HttpGet("placeId={placeId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IEnumerable<Item>> GetItemsByPlaceId([FromRoute] long placeId)
        {
            List<Item> itemsList = await appDbContext.Items.Where(q => q.WarehousePlaceId == placeId).OrderBy(q => q.Id).ToListAsync();

            return itemsList;
        }

        [HttpPut("itemId={itemId}&&bindPlaceId={placeId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> BindItemToPlace([FromRoute] long itemId, [FromRoute] long placeId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                Item item = await appDbContext.Items.FindAsync(itemId);
                if (item == null)
                    return NotFound("Item not found");
                if (placeId <= 0)
                {
                    item.WarehousePlaceId = null;
                    item.WarehousePlace = null;
                }
                else
                {
                    item.WarehousePlaceId = placeId;
                }
                appDbContext.Items.Update(item);
                appDbContext.SaveChanges();
            }
            catch (Exception)
            {
                return BadRequest("Error while bind item to warehouseplace");
            }

            return NoContent();
        }
    }
}
