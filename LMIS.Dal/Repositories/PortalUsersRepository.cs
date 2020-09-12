using System;
using System.Collections.Generic;
using System.Data.Common.CommandTrees;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMIS.Dal.Entity;
using LMIS.Infrastructure.Data.DTOs;
using LMIS.Infrastructure.Enums;
using LMIS.Infrastructure.Interfaces.Repositories;

namespace LMIS.Dal.Repositories
{
    public class PortalUsersRepository : IPortalUsersRepository
    {
        public int SubscribeNewsLetter(decimal portalUsersID)
       {
           int affectedRows = 0;
           using (var db = new LMISEntities())
           {
               var tr = db.PortalUsers
                   .Where(r => r.PortalUsersID == portalUsersID)
                   .ToList().Single();

               tr.IsSubscriper  = true;
            

            affectedRows=db.SaveChanges();
           }
           return affectedRows;
       }

        public List<UserInfo> SubscribeNewsLetterUsers()
        {
            List<UserInfo> result;
            using (var db = new LMISEntities())
            {
                result = (from pusr in db.PortalUsers
                    join individual in db.IndividualDetails on pusr.PortalUsersID equals individual.PortalUsersID
                    where pusr.IsSubscriper
                    select new UserInfo {Email = individual.Email})
                    .Concat(from pusr in db.PortalUsers
                        join org in db.OrganizationContact_Info on pusr.PortalUsersID equals org.PortalUsersID
                        where pusr.IsSubscriper
                        select new UserInfo {Email = org.Email})
                    .ToList();



            }
            return result;
        }

        public UserClass GetUserClass(UserInfo user)
        {
            using (var db = new LMISEntities())
            try
            {
                var cat = db.PortalUsers
                    .Single(r => r.PortalUsersID == user.PortalUserId)
                    .UserCategory.Trim().ToLower();

                if (cat != "ind" && cat != "org") return UserClass.Unknown;

                if (cat == "ind")
                {
                    return db.IndividualDetails
                        .Any(r => r.UserID == user.UserId && r.PortalUsersID == user.PortalUserId)
                        ? UserClass.Individual
                        : UserClass.Unknown;
                }

                var contact = db.OrganizationContact_Info
                    .Single(r => r.UserID == user.UserId && r.PortalUsersID == user.PortalUserId && r.IsDeleted == null);

                if (contact.OrganizationContactID != user.OrgContactId) return UserClass.Unknown;
                    
                return contact.JobTitleID.Trim().ToLower() == "admin"
                    ? UserClass.OrgAdmin
                    : UserClass.OrgContact;

            }
            catch (Exception)
            {
                return UserClass.Unknown;
            }
        }

        public UserCat GetUserCat(long portalUserId)
        {
            using (var db = new LMISEntities())
                try
                {
                    var cat = db.PortalUsers
                        .Single(r => r.PortalUsersID == portalUserId)
                        .UserCategory.Trim().ToLower();

                    switch (cat)
                    {
                        case "org":
                            return UserCat.Organization;
                        case "ind":
                            return UserCat.Individual;
                    }

                    return UserCat.Unknown;
                }
                catch (Exception)
                {
                    return UserCat.Unknown;
                }
        }
    }
}
