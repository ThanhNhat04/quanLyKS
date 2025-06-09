using System;
using System.Collections.Generic;

namespace HotelManagement.Models
{
public class Booking
{
    public int? BookingId { get; set; }
    public int? CustomerId { get; set; }
    public int? RoomId { get; set; }
    public DateTime CheckInTime { get; set; }
    public DateTime CheckOutTime { get; set; }
    public string Status { get; set; }

    public Customer Customer { get; set; }
    public Room Room { get; set; }
    public Invoice Invoice { get; set; }
    public ICollection<BookingService> BookingServices { get; set; }
}
}