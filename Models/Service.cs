using System;
using System.Collections.Generic;

namespace HotelManagement.Models
{
public class Service
{
    public int ServiceId { get; set; }
    public string ServiceName { get; set; }
    public decimal Price { get; set; }

    public ICollection<BookingService> BookingServices { get; set; }
}
}