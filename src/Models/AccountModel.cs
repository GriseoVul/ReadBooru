﻿using System.ComponentModel.DataAnnotations;

namespace ReadBooru.API.Models;

public class AccountModel
{
    public int Id{ get; set; }
    public string Name { get; set;}
    public string PasswordHash { get; set;}
    public string Role{get; set;}
    public AccountModel(int id, string name, string passwordHash, string role)
    {
        Id = id;
        Name = name;
        PasswordHash = passwordHash;
        Role = role;
    }
}
