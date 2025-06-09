using System;
using System.Collections.Generic;

namespace HotelManagement.Models
{
public class RoomType
{
    public int RoomTypeId { get; set; }
    public string RoomTypeName { get; set; }

    public ICollection<Room> Rooms { get; set; }
}
}