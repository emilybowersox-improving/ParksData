﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkData
{
    public class ParkResponse
    {
        public List<Park> Parks {
            get; set;
        }
    }
    public class Park
    {

        public string ParkID { get; set; }
        public string ParkName { get; set; }
        public string SantuaryName { get; set; }
        public string Borough { get; set; }
        public string Acres { get; set; }
        public string Directions { get; set; }
        public string Description { get; set; }
        public string HabitatType { get; set; }

}
}
