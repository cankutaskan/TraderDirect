﻿namespace TraderDirect.Domain.Ports.Contracts;
public interface IUser
{
    public int Id { get; set; }
    public string Email { get; set; }
}

