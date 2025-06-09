using System;
using System.Collections.Generic;

namespace HotelManagement.Models
{
public class Role
{
    public int RoleId { get; set; }
    public string RoleName { get; set; }

    public ICollection<Account> Accounts { get; set; }
}
}