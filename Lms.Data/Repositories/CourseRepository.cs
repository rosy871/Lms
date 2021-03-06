using Lms.Core.Entities;
using Lms.Core.Reporitories;
using Lms.Data.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lms.Data.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private LmsApiContext db;

        public CourseRepository(LmsApiContext context)
        {
            db = context;
        }

        public void Add(Course course)
        {
            db.Add(course);
        }

        public async Task<bool> AnyAsync(int? id)
        {
            return await db.Course.AnyAsync(c => c.Id == id);
        }
        public bool Any(int id)
        {
            return db.Course.Any(e => e.Id == id);
        }

        public async Task<Course> FindAsync(int? id)
        {
            return await db.Course.FindAsync(id);
        }

        public async Task<IEnumerable<Course>> GetAllCourses(bool includemodules)
        {
            return includemodules?
                await db.Course.Include(c => c.Modules).ToListAsync():await db.Course.ToListAsync();
        }
        public async Task<IEnumerable<Course>> GetAllCourses() => await db.Course.ToListAsync();

        public async Task<Course> GetCourse(int? id)
        {
            return await db.Course.FirstOrDefaultAsync(m => m.Id == id);
        }

        public void Remove(Course course)
        {
            db.Course.Remove(course);
        }

        public void Update(Course course)
        {
            db.Update(course);
        }

        public async Task<Course> GetAsync(string title)
        {
            return await db.Course.FirstOrDefaultAsync(m => m.Title == title);
        }

       

        public async Task<Course> GetCourse(int? id, bool includemodules)
        {
            var query = db.Course.AsQueryable();
            if (includemodules)
                query = query.Include(c => c.Modules);

            return await query.FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
