﻿namespace AssignmentScheduler.Entity;

public class User
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public DateTime CreatedDate{ get; set; }
}
