using System;
using DFramework.Application.Common.Interfaces.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace DFramework.Application.Services.Authentication
{
    public class UserProfile : IUserProfile
    {
        private readonly IHttpContextAccessor _context;
        public UserProfile(IHttpContextAccessor context)
        {
            _context = context;
        }

        public int? CurrentUserId
        {
            get
            {
                var userIdClaim = _context.HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
                int.TryParse(userIdClaim?.Value, out int userId);
                return userId == 0 ? null : userId;
            }
        }
    }
}

