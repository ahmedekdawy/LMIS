﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using LMIS.Infrastructure.Data.DTOs;

namespace LMIS.Infrastructure.Interfaces.Managers
{
    public interface IAspNetUsersManager
    {
        UserInfo GetUserInfo(string email);
        List<UserInfo> GetUsersAdmin();
    }
}