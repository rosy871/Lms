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
    [Route("api/courses")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly LmsApiContext _context;
        private readonly IUoW uow;

        public CoursesController(IUoW uow, LmsApiContext context)
        {
            _context = context;
            this.uow = uow;
        }

        // GET: api/Courses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Course>>> GetCourse()
        {
            var courses = await uow.CourseRepository.GetAllCourses();
            return Ok(courses);
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

            return course;
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
        public async Task<IActionResult> PutCourse(int id, Course course)
        {
            if (id != course.Id)
            {
                return BadRequest();
            }

            if (!uow.CourseRepository.Any(id))
            { return NotFound(); }

            uow.CourseRepository.Update(course);
            await uow.CompleteAsync();
            return CreatedAtAction("GetCourse", new { id = course.Id }, course);

        }



        // POST: api/Courses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Course>> PostCourse(Course course)
        {
           // _context.Course.Add(course);
            //await _context.SaveChangesAsync();
            uow.CourseRepository.Add(course);
            await uow.CompleteAsync();
            return CreatedAtAction("GetCourse", new { id = course.Id }, course);
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
