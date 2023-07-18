﻿using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    public class Booking
    {
        public Guid Guid { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Status { get; set; }
        public string Remarks { get; set; }
        [ForeignKey("Room")]
        public Guid RoomGuid { get; set; }
        [ForeignKey("Employee")]
        public Guid EmployeeGuid { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
