using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelManagement.Models
{
    public class Invoice
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int InvoiceId { get; set; }

        public int BookingId { get; set; }
        public decimal TotalAmount { get; set; }
        public string? Status { get; set; }

        public Booking Booking { get; set; }
        public ICollection<Payment> Payments { get; set; } = new List<Payment>();
    }
}