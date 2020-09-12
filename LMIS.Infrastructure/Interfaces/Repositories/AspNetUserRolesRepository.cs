

using System.Collections.Generic;
using LMIS.Infrastructure.Data.DTOs;

namespace LMIS.Infrastructure.Interfaces.Repositories
{
    public interface IAspNetUserRolesRepository
    {
        List<string> GetUserRoles(string userid);
    }
}
