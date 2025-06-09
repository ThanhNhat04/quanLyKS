using System;
using System.Collections.Generic;

namespace HotelManagement.Models
{
public class Payment
{
    public int PaymentId { get; set; }
    public int InvoiceId { get; set; }
    public string PaymentMethod { get; set; }
    public decimal Amount { get; set; }
    public DateTime PaymentDate { get; set; }

    public Invoice Invoice { get; set; }
}
}