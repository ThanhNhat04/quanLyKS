using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelManagement.Models
{
public class Room
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int RoomId { get; set; }

    public string RoomNumber { get; set; }
    public int RoomTypeId { get; set; }
    public decimal Price { get; set; }
    public string Status { get; set; }

    public RoomType RoomType { get; set; }
    public ICollection<Booking> Bookings { get; set; }
}
}