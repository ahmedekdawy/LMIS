using LMIS.Dal.Entity;
using LMIS.Infrastructure.Data.DTOs;
using LMIS.Infrastructure.Data.Entities;
using LMIS.Infrastructure.Enums;
using LMIS.Infrastructure.Interfaces.Repositories;

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Linq;
using System.Runtime.ExceptionServices;

namespace LMIS.Dal.Repositories
{
    public class TestimonialsRepository : ITestimonialsRepository
    {
        private LMISEntities Context = new LMISEntities();

        public List<TestimonialsVM> GetTestimonials(int languageID)
        {
            var result = (from testimonial in Context.Testimonials
                          join testimonialDetail in Context.TestimonialsDetails on testimonial.TestimonialID equals testimonialDetail.TestimonialID
                          join portalUsers in Context.PortalUsers on testimonial.PortalUserID equals portalUsers.PortalUsersID
                          where testimonialDetail.LanguageID == languageID && testimonial.IsApproved==2

                          select
                     new TestimonialsVM
                     {
                         PortalUserID = testimonial.PortalUserID,
                         SiteRating = testimonial.SiteRating,
                         Comments = testimonialDetail.Comments,
                         Name =portalUsers.UserCategory=="IND"? Context.IndividualDetailsDets
                         .Where(i=>i.LanguageID==languageID && i.PortalUsersID==portalUsers.PortalUsersID)
                         .Select(x => x.FirstName).FirstOrDefault() :
                         Context.OrganizationDetails_Det
                         .Where(i => i.LanguageID == languageID && i.PortalUsersID == portalUsers.PortalUsersID)
                         .Select(x => x.OrganizationName).FirstOrDefault()
                     }
                  );
        
            return result.ToList();
        }

        public int Insert(TestimonialsVM item)
        {
            Testimonial it = new Testimonial { PostDate = DateTime.Now, PortalUserID = item .PortalUserID,IsApproved= 1,SiteRating=item.SiteRating};
            it.TestimonialsDetails.Add(new TestimonialsDetail { Comments = item.Comments, LanguageID = 1 });
          

            Context.Testimonials.Add(it);

            return Context.SaveChanges();
        }

        public Dictionary<string, object> Review(long id, int langId = 1)
        {
            using (var db = new LMISEntities())
            {
                return db.Testimonials
                    .Include(r => r.TestimonialsDetails)
                    .AsNoTracking()
                    .Where(r => r.TestimonialID == id && r.IsDeleted == null)
                    .ToList()
                    .Select(a => new Dictionary<string, object>
                    {
                        { "Id", id },
                        { "Rating", a.SiteRating },
                        { "Comment", new GlobalString(a.TestimonialsDetails.Select(d => new LocalString(d.LanguageID, d.Comments)).ToList()) },
                        { "Approval", a.IsApproved },
                        { "RejectReason", "" }
                    })
                    .SingleOrDefault();
            }
        }

        public void Approve(string adminId, long reqKey, long testimonialId, bool approved, string reason)
        {
            using (var db = new LMISEntities())
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    var log = db.RequestLogs
                        .Where(r => r.ID == reqKey && r.RequestID == testimonialId
                            && r.RequestType == "02900009" && r.Is_Approved == 1)
                        .ToList().Single();

                    log.AdminID = adminId;
                    log.Is_Approved = approved ? (byte)Approval.Approved : (byte)Approval.Rejected;

                    var tr = db.Testimonials
                        .Where(r => r.TestimonialID == testimonialId)
                        .ToList().Single();

                    if (approved)
                        tr.IsApproved = (byte)Approval.Approved;
                    else
                    {
                        tr.IsApproved = (byte)Approval.Rejected;
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