using Bogus;
using Lms.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lms.Data.Data
{
    public class SeedData
    {
        private static LmsApiContext db;
        private static Faker faker;
        public static async Task InitAsync(IServiceProvider services, LmsApiContext context)
        {

            db = context;

            if (await db.Course.AnyAsync()) return;

            faker = new Faker("sv");
            var courses = GetCourses();
            await db.AddRangeAsync(courses);

            var modules = GetModules(courses);
            await db.AddRangeAsync(modules);

            await db.SaveChangesAsync();

        }

        private static IEnumerable<Module> GetModules(IEnumerable<Course> courses)
        {
            var modules = new List<Module>();

            foreach (var course in courses)
            {
                var randomnum = faker.Random.Int(2, 6);
                for(int i=0;i<randomnum;i++)
                {
                    var module = new Module
                    {
                        Course = course,
                        Title = faker.Company.CatchPhrase(),
                        StartDate = course.StartDate.AddDays(faker.Random.Int(0, 30))

                    };
                    modules.Add(module);
                }


            }
            return modules;
        }

        private static IEnumerable<Course> GetCourses()
        {
            var courses = new List<Course>();
            for(int i=0; i<20;i++)
            {
                var course = new Course
                {
                    Title = faker.Company.CatchPhrase(),
                    StartDate = DateTime.Now.AddDays(faker.Random.Int(-10, 30))
                };
                courses.Add(course);
            }


            return courses;
        }


    }
}
