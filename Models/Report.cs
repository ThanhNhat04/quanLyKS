using System;
using System.Collections.Generic;

namespace HotelManagement.Models
{
public class Report
{
    public int ReportId { get; set; }
    public DateTime ReportTime { get; set; }
    public decimal TotalRevenue { get; set; }
    public int TotalCustomers { get; set; }
    public decimal OperatingCosts { get; set; }
}
}