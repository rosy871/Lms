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
using AutoMapper;
using Lms.Core.Dto;

namespace Lms.Api.Controllers
{
    [Route("api/Modules")]
    [ApiController]
    public class ModulesController : ControllerBase
    {
        private readonly LmsApiContext _context;
        private readonly IUoW uow;
        private readonly IMapper mapper;

        public ModulesController(LmsApiContext context, IUoW uow,IMapper mapper)
        {
            _context = context;
            this.uow = uow;
            this.mapper = mapper;
        }

        // GET: api/Modules
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Module>>> GetModule()
        {
            var modules = await uow.ModuleRepository.GetAllModules();
            var modulesdto = mapper.Map< IEnumerable<ModuleDto >> (modules);
            return Ok(modulesdto);
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
            var moduledto = mapper.Map<ModuleDto>(modul);
            return Ok(moduledto);
        }

        // PUT: api/Modules/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutModule(int id, Module modul)
        //{
        //    if (id != modul.Id)
        //    {
        //        return BadRequest();
        //    }
                       
        //    if(!uow.ModuleRepository.AnyAsync(id).Result)
        //    { return NotFound(); }

        //    uow.ModuleRepository.Update(modul);
        //    await uow.CompleteAsync();
        //    return CreatedAtAction("GetModule", new { id = modul.Id }, modul);
                      
        //}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutModule(int id, ModuleDto moduldto)
        {
            if (id != moduldto.Id)
            {
                return BadRequest();
            }

            var modul = await uow.ModuleRepository.GetModule(id);
            if(modul==null)
            {
                return StatusCode(404);//not found status code
            }
            mapper.Map(moduldto, modul);
            if (!await uow.CompleteAsyncCheck())
                return StatusCode(500);//not able to save in db status code

            var changedmoduldto = mapper.Map<ModuleDto>(modul);
            return Ok(changedmoduldto);
                      
        }

        // POST: api/Modules
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Module>> PostModule(ModuleDto moduledto)
        {
            if(moduledto.CourseId==0)
            {
                ModelState.AddModelError("Module", "module must belong to a course");
                return BadRequest(ModelState);
            }
            if (await uow.ModuleRepository.GetAsync(moduledto.Title, moduledto.CourseId) != null)
            {
                ModelState.AddModelError("Module", "module already exist in course");
                    return BadRequest(ModelState);
            }

            var modul = mapper.Map<Module>(moduledto);
            uow.ModuleRepository.Add(modul);

            if (await uow.CompleteAsyncCheck())
            {
                var addedmodule = mapper.Map<ModuleDto>(modul);
                return CreatedAtAction(nameof(GetModule), new { id = addedmodule.Id }, addedmodule);
            }
            else 
            {
                return StatusCode(500);//internalservererror
            }
          
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
