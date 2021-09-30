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
    public class ModuleRepository : IModuleRepository
    {
        private LmsApiContext db;

        public ModuleRepository(LmsApiContext context)
        {
           db = context;
        }

        public void Add(Module module)
        {
            db.Add(module);
        }

        public async Task<bool> AnyAsync(int? id)
        {
            return await db.Module.AnyAsync(m => m.Id == id);
        }

        public async Task<Module> FindAsync(int? id)
        {
            return await db.Module.FindAsync(id);
        }

        public async Task<IEnumerable<Module>> GetAllModules()
        {
            return await db.Module.ToListAsync();
        }

        public async Task<Module> GetAsync(string title, int courseId)
        {
            var model = await db.Module.FirstOrDefaultAsync(m=>m.Title==title && m.CourseId==courseId);
            return model;
        }

        public async Task<Module> GetModule(int? id)
        {
            return await db.Module.FirstOrDefaultAsync(m => m.Id == id);
        }

        public void Remove(Module module)
        {
            db.Module.Remove(module);
        }

        public void Update(Module module)
        {
            db.Update(module);
        }
    }
}
