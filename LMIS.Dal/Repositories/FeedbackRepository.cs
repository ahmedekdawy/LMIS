using LMIS.Dal.Entity;
using LMIS.Infrastructure.Data.Entities;
using LMIS.Infrastructure.Enums;
using LMIS.Infrastructure.Interfaces.Repositories;

using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Runtime.ExceptionServices;

namespace LMIS.Dal.Repositories
{
    public class FeedbackRepository : IFeedbackRepository
    {
        private LMISEntities Context = new LMISEntities();

        public int Insert(FeedbackVM item)
        {
            List<Feedback> feed = new List<Feedback>();
            feed.Add(new Feedback { PostDate = DateTime.Now, PostUserID = item.PostUserID, PortalUserID = item.PortalUserID, FeedbackTypeId = item.FeedbackTypeId, Title = item.Title, Description = item.Description, IsReviewed = false, FeedbackLang = 1 });
            Context.Feedbacks.AddRange(feed);
            int result = 0;
            try
            {
                // Your code...
                // Could also be before try if you know the exception occurs in SaveChanges

              result=   Context.SaveChanges();

            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
            return result;

        }

        public Dictionary<string, object> Review(long id, int langId = 1)
        {
            using (var db = new LMISEntities())
            {
                return db.Feedbacks
                    .AsNoTracking()
                    .Where(r => r.FeedbackID == id && r.IsDeleted == null)
                    .Select(a => new
                    {
                        Type = SqlUdf.SubCodeName(a.FeedbackTypeId, 1),
                        Feedback = a
                    })
                    .ToList()
                    .Select(a => new Dictionary<string, object>
                    {
                        { "Id", id },
                        { "Type", a.Type },
                        { "Title", a.Feedback.Title },
                        { "Desc", a.Feedback.Description },
                        { "Lang", a.Feedback.FeedbackLang },
                        { "Approval", a.Feedback.IsReviewed ? (byte) Approval.Approved : (byte) Approval.Pending }
                    })
                    .SingleOrDefault();
            }
        }

        public void Approve(string adminId, long reqKey, long feedbackId, bool approved, string reason)
        {
            using (var db = new LMISEntities())
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    var log = db.RequestLogs
                        .Where(r => r.ID == reqKey && r.RequestID == feedbackId
                            && r.RequestType == "02900008" && r.Is_Approved == 1)
                        .ToList().Single();

                    log.AdminID = adminId;
                    log.Is_Approved = approved ? (byte)Approval.Approved : (byte)Approval.Rejected;

                    var tr = db.Feedbacks
                        .Where(r => r.FeedbackID == feedbackId)
                        .ToList().Single();

                    tr.IsReviewed = approved;

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