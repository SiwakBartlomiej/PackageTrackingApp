﻿using PackageTrackingApp.Core.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackageTrackingApp.Infrastructure.DTOs
{
    public class PackageDto
    {
        public Guid Guid { get; protected set; }
        public string CustomerFirstName { get; protected set; }
        public string CustomerLastName { get; protected set; }
        public string SellerFirstName { get; protected set; }
        public string SellerLastName { get; protected set; }
        public string Name { get; protected set; }
        public bool IsPaid { get; protected set; }
        public float Weight { get; protected set; }
        public float Height { get; protected set; }
        public float Length { get; protected set; }
        public float Width { get; protected set; }
        public string PackageCategory { get; set; }
    }
}
