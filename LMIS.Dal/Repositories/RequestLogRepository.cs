using LMIS.Dal.Entity;
using LMIS.Infrastructure.Data.DTOs;
using LMIS.Infrastructure.Data.Entities;
using LMIS.Infrastructure.Enums;
using LMIS.Infrastructure.Interfaces.Repositories;

using System.Collections.Generic;
using System.Linq;

namespace LMIS.Dal.Repositories
{
    public class RequestLogRepository : IRequestLogRepository
    {
        public List<RequestLogVm> ListPendingByAdminId(string adminId = "", int langId = 1)
        {
            using (var db = new LMISEntities())
            {
                return db.RequestLogs
                    .AsNoTracking()
                    .Where(r => r.Is_Approved == 1 && (r.AdminID == adminId || adminId == ""))
                    .Select(r => new
                    {
                        RequestLog = r,
                        db.Admins.FirstOrDefault(a => a.AdminId == r.AdminID).AdminName,
                        PortalUserName = SqlUdf.PortalUserName(r.PortalUserID, langId),
                        RequestTypeDesc = SqlUdf.SubCodeName(r.RequestType, langId),
                        Title = SqlUdf.RequestTitle(r.RequestType, r.RequestID, langId),
                        DeleteDate = SqlUdf.RequestDeleteDate(r.RequestType, r.RequestID)
                    })
                    .ToList()
                    .Select(r => new RequestLogVm
                    {
                        Id = (long)r.RequestLog.ID,
                        Admin = new CodeSet
                        {
                            id = r.RequestLog.AdminID,
                            desc = r.AdminName
                        },
                        PortalUserId = (long)r.RequestLog.PortalUserID,
                        PortalUserName = r.PortalUserName,
                        RequestType = new CodeSet
                        {
                            id = r.RequestLog.RequestType,
                            desc = r.RequestTypeDesc
                        },
                        RequestId = (long)r.RequestLog.RequestID,
                        Title = r.Title,
                        PostDate = r.RequestLog.PostDate,
                        UpdateDate = r.RequestLog.UpdateDate,
                        DeleteDate = r.DeleteDate,
                        Approval = (Approval)r.RequestLog.Is_Approved
                   })
                   .ToList();
            }
        }
        public List<RequestLogVm> ListRequestLog()
        {
            using (var db = new LMISEntities())
            {
                return db.RequestLogs
                    .AsNoTracking()
              
                    .Select(r => new
                    {
                        RequestLog = r,
                        db.Admins.FirstOrDefault(a => a.AdminId == r.AdminID).AdminName,
                        PortalUserName = SqlUdf.PortalUserName(r.PortalUserID, 1),
                        RequestTypeDesc = SqlUdf.SubCodeName(r.RequestType, 1),
                        Title = SqlUdf.RequestTitle(r.RequestType, r.RequestID, 1),
                        DeleteDate = SqlUdf.RequestDeleteDate(r.RequestType, r.RequestID)
                    })
                    .ToList()
                    .Select(r => new RequestLogVm
                    {
                        Id = (long)r.RequestLog.ID,
                        Admin = new CodeSet
                        {
                            id = r.RequestLog.AdminID,
                            desc = r.AdminName
                        },
                        PortalUserId = (long)r.RequestLog.PortalUserID,
                        PortalUserName = r.PortalUserName,
                        RequestType = new CodeSet
                        {
                            id = r.RequestLog.RequestType,
                            desc = r.RequestTypeDesc
                        },
                        RequestId = (long)r.RequestLog.RequestID,
                        Title = r.Title,
                        PostDate = r.RequestLog.PostDate,
                        UpdateDate = r.RequestLog.UpdateDate,
                        DeleteDate = r.DeleteDate,
                        Approval = (Approval)r.RequestLog.Is_Approved
                    })
                   .ToList();
            }
        }

        public bool ReassignAdmin(long id, string newAdminId)
        {
            using (var db = new LMISEntities())
            {
                var tr = db.RequestLogs
                    .Where(r => r.ID == id)
                    .ToList().Single();

                tr.AdminID = newAdminId;
                db.SaveChanges();

                return true;
            }
        }

        public string AssignedTo(long id)
        {
            using (var db = new LMISEntities())
            {
                var tr = db.RequestLogs
                    .Where(r => r.ID == id)
                    .ToList().Single();

                return tr.AdminID;
            }
        }
    }
}