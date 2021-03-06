﻿using SmartHut.Mvc.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartHut.Mvc.Models.Services
{
    public interface ISmartHutService
    {
        public Task<IEnumerable<Units>> GetUnits();
        public Task<Building> GetBuilding();
        public Task<IEnumerable<Device>> GetDevices();
    }
}
