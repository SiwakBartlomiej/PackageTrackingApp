﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackageTrackingApp.Infrastructure.Services
{
    public interface IDataInitializer
    {
        Task InitializeDataAsync();
    }
}
