using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lms.Data.Data;
using Lms.Core.Entities;
using Lms.Core.Reporitories;

namespace Lms.Api.Controllers
{
    [Route("api/Modules")]
    [ApiController]
    public class ModulesController : ControllerBase
    {
        private readonly LmsApiContext _context;
        private readonly IUoW uow;

        public ModulesController(LmsApiContext context, IUoW uow)
        {
            _context = context;
            this.uow = uow;
        }

        // GET: api/Modules
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Module>>> GetModule()
        {
            var modules = await uow.ModuleRepository.GetAllModules();
            return Ok(modules);
        }

        // GET: api/Modules/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Module>> GetModule(int id)
        {
            var modul = await uow.ModuleRepository.GetModule(id);

            if (modul == null)
            {
                return NotFound();
            }

            return Ok(modul);
        }

        // PUT: api/Modules/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutModule(int id, Module modul)
        {
            if (id != modul.Id)
            {
                return BadRequest();
            }

           
            if(!uow.ModuleRepository.AnyAsync(id).Result)
            { return NotFound(); }

            uow.ModuleRepository.Update(modul);
            await uow.CompleteAsync();
            return CreatedAtAction("GetModule", new { id = modul.Id }, modul);
           

           
        }

        // POST: api/Modules
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Module>> PostModule(Module modul)
        {

            uow.ModuleRepository.Add(modul);
            await uow.CompleteAsync();
            return CreatedAtAction("GetModule", new { id = modul.Id }, modul);
        }

        // DELETE: api/Modules/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteModule(int id)
        {
            var modul = await uow.ModuleRepository.FindAsync(id);
            if (modul == null)
            {
                return NotFound();
            }

            uow.ModuleRepository.Remove(modul);
            await uow.CompleteAsync();

            return NoContent();
        }

        private Task<bool> ModuleExists(int id)
        {
            // return _context.Module.Any(e => e.Id == id);
            return uow.ModuleRepository.AnyAsync(id);
        }
    }
}
