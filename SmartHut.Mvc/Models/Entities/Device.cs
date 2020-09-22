using System;

namespace SmartHut.Mvc.Models.Entities
{
    public class Device
    {
        public Guid Id { get; set; }
        public Guid UnitId { get; set; }
        public string Name { get; set; }
        public int MinValue { get; set; }
        public int MaxValue { get; set; }
        public Units Units { get; set; }
        public int MetricType { get; set; }
        public Guid BuildingId { get; set; }
    }
}