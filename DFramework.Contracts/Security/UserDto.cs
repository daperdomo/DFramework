﻿namespace DFramework.Contracts.Security
{
    public record UserDto
    {
        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public string Fullname { get; set; } = null!;
        public string Email { get; set; } = null!;
    }
}
