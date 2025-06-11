using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelManagement.Models
{
public class Staff
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int StaffId { get; set; }

    public string FullName { get; set; }
    public string Position { get; set; }
    public decimal Salary { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string WorkShift { get; set; }
    public DateTime StartDate { get; set; }
}
}