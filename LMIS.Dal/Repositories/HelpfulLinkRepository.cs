using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;
using LMIS.Dal.Entity;
using LMIS.Infrastructure.Data.Entities;
using LMIS.Infrastructure.Interfaces.Repositories;

namespace LMIS.Dal.Repositories
{
    public class HelpfulLinkRepository : IHelpfulLinkRepository
    {
        public Infrastructure.Data.Entities.HelpfulLinkVm Post(Infrastructure.Data.Entities.HelpfulLinkVm vm, string Userid)
        {
            using (var db = new LMISEntities())
            {
                try
                {
                    var id = vm.HelpfulLinkID;
                    var checkExist = db.HelpfulLinks.Count(c => (c.HelpfulLinkURL == vm.HelpfulLinkURL || c.HelpfulLinkName == vm.HelpfulLinkName) && c.HelpfulLinkLanguage == vm.HelpfulLinkLanguage && c.HelpfulLinkID != vm.HelpfulLinkID && c.IsDeleted == null);
                    if (checkExist > 0)
                    {return null;}
                    if (id > 0) //Update
                    {
                        var tr = db.HelpfulLinks
                            .Where(r => r.IsDeleted == null && r.HelpfulLinkID == id)
                            .ToList().Single();

                        tr.HelpfulLinkURL = vm.HelpfulLinkURL;
                        tr.HelpfulLinkName = vm.HelpfulLinkName;
                        tr.HelpfulLinkLanguage = vm.HelpfulLinkLanguage;
                        tr.GroupID = vm.GroupID;
                        tr.UpdateUserID = Userid;
                        tr.UpdateDate = DateTime.UtcNow;
                        
                    }
                    else //Insert
                    {
                        var tr = new HelpfulLink()
                        {
                               HelpfulLinkURL = vm.HelpfulLinkURL,
                        HelpfulLinkName = vm.HelpfulLinkName,
                        HelpfulLinkLanguage = vm.HelpfulLinkLanguage,
                        GroupID = vm.GroupID,
                            PostUserID = Userid,
                            PostDate = DateTime.UtcNow

                        };

                        db.HelpfulLinks.Add(tr);
                        db.SaveChanges();
                        vm.HelpfulLinkID = tr.HelpfulLinkID;
                    }

                    db.SaveChanges();

                }
                catch (Exception ex)
                {
                    ExceptionDispatchInfo.Capture(ex).Throw();
                }


                return vm;
            }
        }

        public int Delete(string userId, long id)
        {
            int result;
            using (var db = new LMISEntities())
            {
                var tr = db.HelpfulLinks.Single(r => r.HelpfulLinkID == id);

                tr.IsDeleted = true;
                tr.DeleteUserID = userId;
                tr.DeleteDate = DateTime.UtcNow;

                result = db.SaveChanges();
            }
            return result;
        }

        public List<Infrastructure.Data.Entities.HelpfulLinkVm> Get(int languageId, decimal? HelpfulLinkURLId)
        {
            List<HelpfulLinkVm> result;
            using (var db = new LMISEntities())
            {
                result =
                    db.HelpfulLinks.Where(
                        w =>
                            w.IsDeleted == null &&
                            (w.HelpfulLinkLanguage == languageId || languageId==0) &&
                            w.HelpfulLinkID == (HelpfulLinkURLId > 0 ? HelpfulLinkURLId : w.HelpfulLinkID))
                            
                        .Select(s => new HelpfulLinkVm
                        {
                            HelpfulLinkID = s.HelpfulLinkID,
                            HelpfulLinkURL = s.HelpfulLinkURL,

                            HelpfulLinkName = s.HelpfulLinkName,
                            HelpfulLinkLanguage = s.HelpfulLinkLanguage,
                            GroupID = s.GroupID,
                            GroupName=SqlUdf.SubCodeName(s.GroupID, languageId)
                            
                        })
                        .OrderBy(o=>o.GroupName)
                        .ToList();
            }

            return result;
        }
    }
}
