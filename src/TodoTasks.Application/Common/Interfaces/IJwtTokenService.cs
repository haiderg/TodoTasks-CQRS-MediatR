using System;
using System.Collections.Generic;
using System.Text;

namespace TodoTasks.Application.Common.Interfaces
{
    public interface IJwtTokenService
    {
        string GenerateToken(Guid userId, string email, string role);
    }

}
