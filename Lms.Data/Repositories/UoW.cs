﻿using Lms.Core.Reporitories;
using Lms.Data.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lms.Data.Repositories
{
    public class UoW : IUoW
    {
        private readonly LmsApiContext db;
        public ICourseRepository CourseRepository { get; }

        public IModuleRepository ModuleRepository { get; }

        public UoW(LmsApiContext context)
        {
            db = context;
            CourseRepository = new CourseRepository(context);
            ModuleRepository = new ModuleRepository(context);


        }

        public async Task CompleteAsync()
        {
            await db.SaveChangesAsync();
        }
    }
}
