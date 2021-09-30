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
    [Route("api/courses")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IUoW uow;

        public CoursesController(IUoW uow, IMapper mapper)
        {
            this.mapper = mapper;
            this.uow = uow;
        }

        // GET: api/Courses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Course>>> GetCourse()
        {
            var courses = await uow.CourseRepository.GetAllCourses();
            var courseDto = mapper.Map<IEnumerable<CourseDto>>(courses);
            return Ok(courseDto);
        }

        // GET: api/Courses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Course>> GetCourse(int id)
        {
            var course = await uow.CourseRepository.GetCourse(id);

            if (course == null)
            {
                return NotFound();
            }
            var dto = mapper.Map<CourseDto>(course);

            return Ok(dto);
        }

        // PUT: api/Courses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutCourse(int id, Course course)
        //{
        //    if (id != course.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(course).State = EntityState.Modified;


        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!CourseExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCourse(int id,CourseDto coursedto)
        {
            if (id != coursedto.Id)
            {
                return BadRequest();
            }
            var course = await uow.CourseRepository.GetCourse(id);
            if (course==null)
            {
                //return NotFound(); 
                //same meaning diff way
               return StatusCode(StatusCodes.Status404NotFound);
            }

            mapper.Map(coursedto, course);
           
            if (!await uow.CompleteAsyncCheck())
            {
                //code for not able to save in db
                return StatusCode(500);
            }
            var dto = mapper.Map<CourseDto>(course);
            return Ok(dto);
        }



        // POST: api/Courses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Course>> PostCourse(CourseDto coursedto)
        {

            //uow.CourseRepository.Add(course);
            //await uow.CompleteAsync();
            //return CreatedAtAction("GetCourse", new { id = course.Id }, course);
            if (await uow.CourseRepository.GetAsync(coursedto.Title) != null)
            {
                ModelState.AddModelError("Title", "Course Title already exists");
                return BadRequest(ModelState);
            }
            var course=mapper.Map<Course>(coursedto);
            uow.CourseRepository.Add(course);

            if (!await uow.CompleteAsyncCheck())
            {
                //return StatusCode(500);
                //or
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
            var addedcourse = mapper.Map<CourseDto>(course);
            return CreatedAtAction(nameof(GetCourse), new { id = addedcourse.Id }, course);
           

        }

        // DELETE: api/Courses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            var course = await uow.CourseRepository.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }

            uow.CourseRepository.Remove(course);
            await uow.CompleteAsync();

            return NoContent();
        }

        private bool CourseExists(int id)
        {
           return uow.CourseRepository.Any(id);
           
        }
    }
}
