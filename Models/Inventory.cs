using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelManagement.Models
{
public class Inventory
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int InventoryId { get; set; }

    public string? ItemName { get; set; }
    public int Quantity { get; set; }
    public string? Unit { get; set; }
    public string? Status { get; set; }
    public DateTime ImportDate { get; set; }
    public string? Supplier { get; set; }
}
}