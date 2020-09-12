using LMIS.Bll.Helpers;
using LMIS.Infrastructure.Data.DTOs;
using LMIS.Infrastructure.Data.Entities;
using LMIS.Infrastructure.Interfaces.Managers;
using LMIS.Infrastructure.Interfaces.Repositories;

using System;
using System.Collections.Generic;

namespace LMIS.Bll.Managers
{
    public class RequestLogManager : IRequestLogManager
    {
        private static readonly IRequestLogRepository Repo = DalFactory.Singleton.RequestLog;

        public ModelResponse ListPendingByAdminId(UserInfo admin, int langId = 1, bool forAllAdmins = false)
        {
            List<RequestLogVm> ds;

            try
            {
                //Authorization
                if (admin == null) return new ModelResponse(101);

                //Validation
                forAllAdmins &= DalFactory.Singleton.DataService.IsSuperAdmin(admin.UserId);

                ds = Repo.ListPendingByAdminId(forAllAdmins ? "" : admin.UserId, langId);
            }
            catch (Exception ex)
            {
                return new ModelResponse(ex);
            }

            return new ModelResponse(0, ds);
        }

        public List<RequestLogVm> ListRequestLog()
        {
            List<RequestLogVm> ds;

            try
            {
              
              

                ds = Repo.ListRequestLog();
            }
            catch (Exception ex)
            {
                return null ;
            }

            return  ds;
        }

        public ModelResponse ReassignAdmin(UserInfo admin, long requestId, string newAdminId)
        {
            bool ret;

            try
            {
                //Authorization
                if (admin == null) return new ModelResponse(101);
                if (!DalFactory.Singleton.DataService.IsSuperAdmin(admin.UserId)) return new ModelResponse(101);

                ret = Repo.ReassignAdmin(requestId, newAdminId);
            }
            catch (Exception ex)
            {
                return new ModelResponse(ex);
            }

            return new ModelResponse(0, ret);
        }
    }
}