using System;
using System.Collections.Generic;

namespace HotelManagement.Models
{
public class Inventory
{
    public int InventoryId { get; set; }
    public string? ItemName { get; set; }
    public int Quantity { get; set; }
    public string? Unit { get; set; }
    public string? Status { get; set; }
    public DateTime ImportDate { get; set; }
    public string? Supplier { get; set; }
}
}