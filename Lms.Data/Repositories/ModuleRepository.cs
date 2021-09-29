using Lms.Core.Entities;
using Lms.Core.Reporitories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lms.Data.Repositories
{
    public class ModuleRepository : IModuleRepository
    {
        public void Add(Module Module)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AnyAsync(int? id)
        {
            throw new NotImplementedException();
        }

        public Task<Module> FindAsync(int? id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Module>> GetAllCourses()
        {
            throw new NotImplementedException();
        }

        public Task<Module> GetCourse(int? id)
        {
            throw new NotImplementedException();
        }

        public void Remove(Module Module)
        {
            throw new NotImplementedException();
        }

        public void Update(Module Module)
        {
            throw new NotImplementedException();
        }
    }
}
