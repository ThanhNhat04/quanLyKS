using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelManagement.Models
{
    public class BookingService
    {
        // Khóa chính kết hợp
        public int BookingId { get; set; }
        public Booking Booking { get; set; } = null!;

        public int ServiceId { get; set; }
        public Service Service { get; set; } = null!;

        public DateTime ServiceDate { get; set; } = DateTime.Now;
    }
}
