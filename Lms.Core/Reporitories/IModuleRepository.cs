using Lms.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lms.Core.Reporitories
{
   public interface IModuleRepository
    {
        Task<IEnumerable<Module>> GetAllModules();
        Task<Module> GetModule(int? id);
        Task<Module> FindAsync(int? id);
        Task<bool> AnyAsync(int? id);
        void Add(Module Module);
        void Update(Module Module);
        void Remove(Module Module);
        Task<Module> GetAsync(string title, int courseId);
    }
}
