﻿using Microsoft.AspNetCore.JsonPatch;
using PackageTrackingApp.Core.Domains;
using PackageTrackingApp.Infrastructure.Commands;
using PackageTrackingApp.Infrastructure.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackageTrackingApp.Infrastructure.Services
{
    public interface IPackageService
    {
        Task<PackageDto> GetAsync(Guid guid);
        Task<IEnumerable<PackageDto>> GetAllAsync();
        Task<Guid> AddAsync(CreatePackage package);
        Task RemoveAsync(Guid guid);
        Task UpdateAsync(Guid guid, UpdatePackage package);
        Task UpdateAsync(Guid packageId, JsonPatchDocument<PatchPackage> patchDoc);
    }
}
