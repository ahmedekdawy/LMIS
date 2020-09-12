using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMIS.Dal.Entity;
using LMIS.Infrastructure.Data.DTOs;
using LMIS.Infrastructure.Interfaces.Repositories;

namespace LMIS.Dal.Repositories
{
  public   class OrganizationContactInfoRepository : IOrganizationContactInfoRepository
    {
        private LMISEntities _ctx = new LMISEntities();
        public UserInfo GetOrgContactInfo(string UserId)
        {

            var userinfo = (from orgcontactinfo in _ctx.OrganizationContact_Info
                            join puser in _ctx.PortalUsers on orgcontactinfo.PortalUsersID equals puser.PortalUsersID
                            where orgcontactinfo.UserID == UserId 
                            select new UserInfo
                            {
                                UserId = orgcontactinfo.UserID,
                                PortalUserId = orgcontactinfo.PortalUsersID,
                                IsApproved = orgcontactinfo.IsApproved == 2 ? true : false,
                                IsInternal = puser.Internal,
                                IsJobSeeker = puser.JobSeeker,
                                IsTrainingProvider = puser.TrainingProvider,
                                IsTrainingSeeker = puser.TrainingSeeker,
                                IsEmployer = puser.Employer,
                                IsResearcher = puser.Researcher,
                                OrgContactId=orgcontactinfo.OrganizationContactID,

                                IsIndividual = false,
                                
                                Email = orgcontactinfo.Email 

                            });




            return userinfo.ToList().FirstOrDefault();





        }

    public   int Insert(UserInfo contact)
      {
          using (var db = new LMISEntities())
          {

              db.OrganizationContact_Info.Add(new OrganizationContact_Info()
              {
                  UserID = contact.UserId,
                  JobTitleID = "admin",
                  Telephone = "",
                  Mobile = "",
                  Email = contact.UserName,
                  IsApproved =2,
                  PostDate = DateTime.UtcNow,
                  PostUSerID = contact.UserId,
                  PortalUsersID =contact.PortalUserId 
              });

             return  db.SaveChanges();
          }

      }

      public int Delete(string UserId)
      {
          int result;
          using (var db = new LMISEntities())
          {
              var tr = db.OrganizationContact_Info.SingleOrDefault(r => r.UserID == UserId);
              if (tr != null)
              {
                  db.OrganizationContact_Info.Remove(tr);
              }
              result = db.SaveChanges();
          }
          return result;
      }
    }
}
