using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleCheckIn.Models
{
    public class CheckIn
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public DateTime CheckInTime { get; set; }
        public DateTime? CheckoutTime { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Address { get; set; }
    }
}