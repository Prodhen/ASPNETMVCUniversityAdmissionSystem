using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _1262228_Arosh.Models
{
    public class SeatPlanViewModel
    {
        public int SeatPlanID { get; set; }
        public string Institution { get; set; }
        public string BuildingName { get; set; }
        public int FloorNumber { get; set; }
        public int StudentID { get; set; }
        public virtual Student Student { get; set; }
    }
}