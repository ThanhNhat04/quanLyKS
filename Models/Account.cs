using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace HotelManagement.Models
{
public class Account
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // 👈 tự động tăng
    public int AccountId { get; set; }

    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;

    public string DisplayName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;


    public int RoleId { get; set; }
    public Role? Role { get; set; }
}
}