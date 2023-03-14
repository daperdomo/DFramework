﻿namespace DFramework.Contracts.Authentication
{
    public record AuthenticateRequest
    {
        public string? Username { get; set; } = null!;
        public string? Password { get; set; } = null!;
    }
}
