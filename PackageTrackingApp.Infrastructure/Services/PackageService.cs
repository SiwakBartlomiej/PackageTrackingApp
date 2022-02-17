﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PackageTrackingApp.Core.Domain;
using PackageTrackingApp.Core.Domains;
using PackageTrackingApp.Core.Repositories;
using PackageTrackingApp.Infrastructure.DTOs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackageTrackingApp.Infrastructure.Services
{
    public class PackageService : IPackageService
    {
        private readonly IMapper _mapper;
        private readonly ILogger<PackageService> _logger;
        private readonly IPackageRepository _packageRepository;

        public PackageService(IMapper mapper, ILogger<PackageService> logger,
            IPackageRepository packageRepository)
        {
            _mapper = mapper;
            _logger = logger;
            _packageRepository = packageRepository;
        }

        public async Task<Guid> AddAsync(CreatePackageDto packageDto)
        {
            var package = _mapper.Map<CreatePackageDto, Package>(packageDto);

            await _packageRepository.AddAsync(package);

            return package.Guid;
        }

        public async Task<PackageDto> GetAsync(Guid guid)
        {
            var package = await _packageRepository.GetAsync(guid);

            return _mapper.Map<Package, PackageDto>(package);
        }

        public async Task<List<PackageDto>> GetAllAsync()
        {
            var packages = await _packageRepository.GetAllAsync();

            if (!packages.Any())
            {
                throw new ArgumentException($"There are no packages yet!");
            }

            return _mapper.Map<List<Package>, List<PackageDto>>(packages);
        }

        public async Task RemoveAsync(Guid guid)
        {
            var package = await _packageRepository.GetAsync(guid);

            if (package is null)
            {
                throw new ArgumentException($"Package with id: {guid} doesn't exist!");
            }

            await _packageRepository.RemoveAsync(guid);
        }

        public async Task UpdateAsync(Package package)
        {
            if (package is null)
            {
                throw new ArgumentException($"Package with id: {package.Guid} doesn't exist!");
            }

            await _packageRepository.UpdateAsync(package);
        }
    }
}
