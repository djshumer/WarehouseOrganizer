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
    public abstract class GenericController<TEntity> : ControllerBase where TEntity : BaseEntitySimpleModel 
    {
        protected readonly AppDbContext appDbContext;
        
        public GenericController(AppDbContext dbContext)
        {
            appDbContext = dbContext;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public virtual IEnumerable<TEntity> List()
        {
            return appDbContext.Set<TEntity>().ToList();
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public virtual async Task<IActionResult> GetEntity([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var entity = await appDbContext.Set<TEntity>().FindAsync(id);

            if (entity == null)
            {
                return NotFound();
            }

            return Ok(entity);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public virtual async Task<IActionResult> Create([FromBody] TEntity entity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            appDbContext.Set<TEntity>().Add(entity);
            await appDbContext.SaveChangesAsync();
        
            return CreatedAtAction("GetEntity", new { id = entity.Id }, entity);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public virtual async Task<IActionResult> Edit([FromRoute] long id, [FromBody] TEntity entity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != entity.Id)
            {
                return BadRequest();
            }

            appDbContext.Entry(entity).State = EntityState.Modified;

            try
            {
                await appDbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EntityExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public virtual async Task<IActionResult> Delete([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            TEntity entity = await appDbContext.Set<TEntity>().FindAsync(id);

            if (entity == null)
                return NotFound();

            appDbContext.Set<TEntity>().Remove(entity);
            await appDbContext.SaveChangesAsync();

            return Ok(entity);
        }

        private bool EntityExists(long id)
        {
            return appDbContext.Set<TEntity>().Any(e => e.Id == id);
        }
    }
}
