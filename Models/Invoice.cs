using System;
using System.Collections.Generic;

namespace HotelManagement.Models
{
public class Invoice
{
    public int InvoiceId { get; set; }
    public int BookingId { get; set; }
    public decimal TotalAmount { get; set; }
    public string? Status { get; set; }

    public Booking Booking { get; set; }
    public ICollection<Payment> Payments { get; set; }
}
}