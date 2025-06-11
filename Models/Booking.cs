using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelManagement.Models
{
public class Booking
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int BookingId { get; set; }

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