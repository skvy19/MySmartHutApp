using SmartHut.Mvc.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartHut.Mvc.ViewModels
{
    public class RoomsViewModel
    {
        public Building Building { get; set; }
        public IEnumerable<Device> Devices { get; set; }
        public IEnumerable<Units> Units { get; set; }
    }
}
