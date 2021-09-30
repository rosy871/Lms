using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lms.Core.Dto
{
   public class ModuleDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime StopDate { get { return StartDate.AddMonths(1); } }
        public int CourseId { get; set; }
    }
}
