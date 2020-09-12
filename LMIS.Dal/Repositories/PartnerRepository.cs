
using LMIS.Infrastructure.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using LMIS.Dal.Entity;
using LMIS.Infrastructure.Data.DTOs;
using LMIS.Infrastructure.Data.Entities;
using System.Data.Entity;
using System.Runtime.ExceptionServices;
using LMIS.Infrastructure.Enums;
using LMIS.Infrastructure.Helpers;

namespace LMIS.Dal
{
   public class PartnerRepository : IPartnerRepository
    {
       private LMISEntities Context = new LMISEntities();
       public List<PartnerVM> GetActivePartners(int lang, decimal? partnerID)
       {
          
            var result = (from partner in Context.Partners
                          join partnersDet in Context.PartnersDetails on partner.PartnerID equals  partnersDet.PartnerID
                          join orgDetail in Context.OrganizationDetails on partner.PortalUserID equals orgDetail.PortalUsersID 
                          join orgDetailDet in Context.OrganizationDetails_Det on partner.PortalUserID equals orgDetailDet.PortalUsersID
                          where partner.Is_Approved == 2 && orgDetailDet.LanguageID == lang && partner.PartnerID == (partnerID > 0 ? partnerID : partner.PartnerID)
                          select new PartnerVM
                          {
                           PartnerID=            partner.     PartnerID          ,
                           PortalUserID=         partner.     PortalUserID       ,
                           CEOEmail=             partner.     CEOEmail           ,
                           Is_Approved=          partner.     Is_Approved         ,
                           YearFounded = partner.YearFounded,
                           RejectReason=         partner.     RejectReason        ,
                           PostDate=             partner.     PostDate            ,
                           ZipPostalCode = orgDetail .ZipPostalCode,
                           Telephone = orgDetail.Telephone,
                           OrganizationWebsite = orgDetail.OrganizationWebsite,
                           OrganizationName = orgDetailDet.OrganizationName,
                           Address = orgDetailDet.Address,
                           OrganizationLogoPath = orgDetail.OrganizationLogoPath,
                           CEOFirstName = partnersDet.CEOFirstName,
                           CEOLastName = partnersDet.CEOLastName,
                           GeneralDescriptionCoreBusiness = partnersDet.GeneralDescriptionCoreBusiness ,
                           PossibleAreaOfCooperation = partnersDet.PossibleAreaOfCooperation
                                                                                 
                          });
            return result.ToList();
        }

       public int Insert(PartnerVM partener)
       {
           var partnerUpdate = Context.Partners.Where(p => p.PortalUserID == partener.PortalUserID).FirstOrDefault();
           if (partnerUpdate == null)
           {
               var _Partner = new Partner
               {
                   CEOEmail = partener.CEOEmail,
                   PortalUserID = partener.PortalUserID,
                   YearFounded = partener.YearFounded,
                   PostDate = DateTime.Now,
                   OrganizationContactID = partener.OrganizationContactID,
                   PostUserID = partener.PostUserID,
                   Is_Approved = 1
               };
               var _PartnersDetails = new PartnersDetail
               {
                   PartnerID = partener.PartnerID,
                   LanguageID = partener.LanguageID,
                   CEOFirstName = partener.CEOFirstName,
                   CEOLastName = partener.CEOLastName,
                   GeneralDescriptionCoreBusiness = partener.GeneralDescriptionCoreBusiness,
                   PossibleAreaOfCooperation = partener.PossibleAreaOfCooperation
               };
            
               _Partner.PartnersDetails.Add(_PartnersDetails);
               Context.Partners.Add(_Partner);
           }
           else
           {
               partnerUpdate.Is_Approved = 1;
               partnerUpdate.CEOEmail = partener.CEOEmail;
               partnerUpdate.UpdateUserID = partener.UpdateUserID;
               partnerUpdate.YearFounded = partener.YearFounded;
               partnerUpdate.UpdateDate = DateTime.Now;
               partnerUpdate.OrganizationContactID = partener.OrganizationContactID;
               Context.Partners.Attach(partnerUpdate);
               Context.Entry(partnerUpdate).State = EntityState.Modified;
               var partnerUpdatedet = Context.PartnersDetails.Where(p => p.PartnerID == partnerUpdate.PartnerID && p.LanguageID == partener.LanguageID).FirstOrDefault();
               if (partnerUpdatedet != null)
               {
                   partnerUpdatedet.CEOFirstName = partener.CEOFirstName;
                   partnerUpdatedet.CEOLastName = partener.CEOLastName;
                   partnerUpdatedet.LanguageID = partener.LanguageID;
                   partnerUpdatedet.GeneralDescriptionCoreBusiness = partener.GeneralDescriptionCoreBusiness;
                   partnerUpdatedet.PossibleAreaOfCooperation = partener.PossibleAreaOfCooperation;
                   Context.PartnersDetails.Attach(partnerUpdatedet);
                   Context.Entry(partnerUpdatedet).State = EntityState.Modified;
               }
               else
               {
                   var _PartnersDetails = new PartnersDetail
                   {
                       PartnerID = partnerUpdate.PartnerID,
                       LanguageID = partener.LanguageID,
                       CEOFirstName = partener.CEOFirstName,
                       CEOLastName = partener.CEOLastName,
                       GeneralDescriptionCoreBusiness = partener.GeneralDescriptionCoreBusiness,
                       PossibleAreaOfCooperation = partener.PossibleAreaOfCooperation
                   };
                   Context.PartnersDetails.Add(_PartnersDetails);
               }
           }
           
           return Context.SaveChanges();
       }

       public Dictionary<string, object> Review(long id, int langId = 1)
       {
           using (var db = new LMISEntities())
           {
               return db.Partners
                   .Include(r => r.PartnersDetails)
                   .AsNoTracking()
                   .Where(r => r.PartnerID == id && r.IsDeleted == null)
                   .ToList()
                   .Select(a => new Dictionary<string, object>
                    {
                        { "Id", id },
                        { "CEOFirstName", a.PartnersDetails.First().CEOFirstName },
                        { "CEOLastName", a.PartnersDetails.First().CEOLastName },
                        { "CEOEmail", a.CEOEmail },
                        { "Year", a.YearFounded },
                        { "Business", a.PartnersDetails.First().GeneralDescriptionCoreBusiness },
                        { "AOC", a.PartnersDetails.First().PossibleAreaOfCooperation },
                        { "Approval", a.Is_Approved },
                        { "RejectReason", "" }
                    })
                   .SingleOrDefault();
           }
       }

       public void Approve(string adminId, long reqKey, long partnerId, bool approved, string reason)
       {
           using (var db = new LMISEntities())
           using (var transaction = db.Database.BeginTransaction())
           {
               try
               {
                   var log = db.RequestLogs
                       .Where(r => r.ID == reqKey && r.RequestID == partnerId
                           && r.RequestType == "02900003" && r.Is_Approved == 1)
                       .ToList().Single();

                   log.AdminID = adminId;
                   log.Is_Approved = approved ? (byte)Approval.Approved : (byte)Approval.Rejected;

                   var tr = db.Partners
                       .Where(r => r.PartnerID == partnerId)
                       .ToList().Single();

                   if (approved)
                       tr.Is_Approved = (byte)Approval.Approved;
                   else
                   {
                       tr.Is_Approved = (byte)Approval.Rejected;
                   }

                   db.SaveChanges();
                   transaction.Commit();
               }
               catch (Exception ex)
               {
                   transaction.Rollback();
                   ExceptionDispatchInfo.Capture(ex).Throw();
               }
           }
       }

    }
}
