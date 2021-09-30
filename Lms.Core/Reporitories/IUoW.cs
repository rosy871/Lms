﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lms.Core.Reporitories
{
    public interface IUoW
    {
        ICourseRepository CourseRepository { get; }
        IModuleRepository ModuleRepository { get; }
        Task CompleteAsync();
    }
}
