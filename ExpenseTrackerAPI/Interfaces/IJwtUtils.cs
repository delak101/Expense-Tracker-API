using System;
using ExpenseTrackerAPI.Models;

namespace ExpenseTrackerAPI.Interfaces
{
    public interface IJwtUtils
    {
        string GenerateToken(User user);
        int? ValidateToken(string token);
    }
}