﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SmartHut.Mvc.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace SmartHut.Mvc.Models.Services
{
    public class SmartHutService : ISmartHutService
    {
        private readonly HttpClient _client;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private string _token { get; set; }

        public SmartHutService(
            HttpClient client,
            IConfiguration configuration,
            IHttpContextAccessor httpContextAccessor
            )
        {
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            _token = httpContextAccessor.HttpContext.GetTokenAsync("id_token").Result;

            client.BaseAddress = configuration.GetValue<Uri>("SmartHutApi:BaseUri");
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {_token}");

            _client = client;
        }
        public async Task<Building> GetBuilding()
        {
            var response = await _client.GetStringAsync("/buildingInfo/getMyBuilding");

            Building building = JsonConvert.DeserializeObject<Building>(response);

            return building;
        }

        public async Task<IEnumerable<Device>> GetDevices()
        {
            var building = await GetBuilding();

            var response = await _client.GetStringAsync($"/BuildingInfo/{building.Id}/true");

            Building buildingWithDevices = JsonConvert.DeserializeObject<Building>(response);
            var devices = buildingWithDevices.Devices;

            foreach (var device in devices)
            {
                foreach (var unit in await GetUnits())
                {
                    if (device.UnitId == unit.Id)
                    {
                        device.Units = unit;
                    }
                }
            }

            return devices;
        }

        public async Task<IEnumerable<Units>> GetUnits()
        {
            var response = await _client.GetStringAsync("/unit");

            var units = JsonConvert.DeserializeObject<IEnumerable<Units>>(response);

            return units;
        }
    }
}
