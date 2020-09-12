using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Linq;
using LMIS.Dal.Entity;
using LMIS.Infrastructure.Data.DTOs;
using LMIS.Infrastructure.Data.Entities;
using LMIS.Infrastructure.Interfaces.Repositories;

namespace LMIS.Dal.Repositories
{
    public class SubCodeRepository : ISubCodeRepository
    {
        private readonly LMISEntities _context = new LMISEntities();

        private readonly Func<List<LocalString>, SubCode, List<LocalString>> _f1 = (a, b) =>
        {
            a.Add(new LocalString(b.LanguageID, b.Name));
            return a;
        };

        private readonly Func<List<GlobalString>, GlobalString, List<GlobalString>> _f2 = (a, b) =>
        {
            a.Add(b);
            return a;
        };

        public List<SubCodeVm> GetAllSubCode(string generalId, int languageId)
        {
            var result = (from subCode in _context.SubCodes
                          join generalCode in _context.GeneralCodes on subCode.GeneralID equals generalCode.GeneralID
                          join f in _context.SubCodes on subCode.ParentSubCodeID equals f.SubID into fg
                          from fgi in fg.Where(f => f.LanguageID == languageId).DefaultIfEmpty()
                          where subCode.GeneralID == generalId && subCode.LanguageID == languageId
                         orderby subCode.Name 
                         
                         select 
                    new SubCodeVm
                    {
                       
                        SubID = subCode.SubID,
                        GeneralID = subCode.GeneralID,
                        LanguageID = subCode.LanguageID,
                        Name = subCode.Name,
                        ParentSubCodeID = subCode.ParentSubCodeID,
                        ParentSubCodeName = fgi.Name,
                      //  GeneralName = generalCode.Name,
                       
                    }
                    ).Distinct();
            return result.ToList();


        }
        public List<SubCodeVm> GetSubCode(string subId)
        {
            var result = (from subCode in _context.SubCodes
                          join generalCode in _context.GeneralCodes on subCode.GeneralID equals generalCode.GeneralID
                             where subCode.SubID == subId && subCode.LanguageID == generalCode.LanguageID
                          orderby subCode.Name
                          
                          select
                     new SubCodeVm
                     {
                         SubID = subCode.SubID,
                         GeneralID = subCode.GeneralID,
                         LanguageID = subCode.LanguageID,
                         Name = subCode.Name,
                         ParentSubCodeID = subCode.ParentSubCodeID,
                         GeneralName = generalCode.Name
                     }
                    ).Distinct();
            return result.ToList();


        }
        public List<SubCodeVm> GetAllSubCode(string generalId, int languageId, string yearFrom, string YearTo)
        {
            var result = (from subCode in _context.SubCodes
                          join generalCode in _context.GeneralCodes on subCode.GeneralID equals generalCode.GeneralID
                          where subCode.GeneralID == generalId && subCode.LanguageID == languageId && generalCode.LanguageID == languageId
                                      && subCode.Name.CompareTo((from subfrom in _context.SubCodes where subfrom.LanguageID==languageId&& subfrom.SubID == yearFrom select subfrom.Name).FirstOrDefault()) >= 0
                                       && subCode.Name.CompareTo((from subto in _context.SubCodes where subto.LanguageID == languageId && subto.SubID == YearTo select subto.Name).FirstOrDefault()) <= 0
                          orderby subCode.Name
                          select
                     new SubCodeVm
                     {
                         SubID = subCode.SubID,
                         GeneralID = subCode.GeneralID,
                         LanguageID = subCode.LanguageID,
                         Name = subCode.Name,
                         ParentSubCodeID = subCode.ParentSubCodeID,
                         GeneralName = generalCode.Name
                     }
                   );
            return result.ToList();
        }

        public List<GlobalString> List(string generalId)
        {
            using (var db = new LMISEntities())
            {
                return db.SubCodes
                    .Where(r => r.GeneralID == generalId)
                    .GroupBy(r => r.SubID)
                    .ToList()
                    .Select(g => new GlobalString(g.Aggregate(new List<LocalString>(), (a, b) => _f1(a, b)), g.Key))
                    .ToList();
            }
        }
        public List<GlobalString> List(string generalId, string execludedIds)
        {
            using (var db = new LMISEntities())
            {
                return db.SubCodes
                    .Where(r => r.GeneralID == generalId && !execludedIds.Contains( r.SubID))  
                    .GroupBy(r => r.SubID)
                    .ToList()
                    .Select(g => new GlobalString(g.Aggregate(new List<LocalString>(), (a, b) => _f1(a, b)), g.Key))
                    .ToList();
            }
        }
       
        
        public List<CodeSuperset> Group(string generalId)
        {
            using (var db = new LMISEntities())
            {
                return db.SubCodes
                    .Include(r => r.GeneralCode)
                    .Where(r => r.GeneralCode.ParentGeneralcodeID == generalId)
                    .GroupBy(r => new { r.SubID, r.GeneralID, r.GeneralCode.Name })
                    .ToList()
                    .Select(g1 => new
                    {
                        generalId = g1.Key.GeneralID,
                        generalName = g1.Key.Name,
                        subCode = new GlobalString(g1.Aggregate(new List<LocalString>(), (a, b) => _f1(a, b)), g1.Key.SubID)
                    })
                    .GroupBy(r => new { r.generalId, r.generalName })
                    .Select(g2 => new CodeSuperset(g2.Key.generalId, g2.Key.generalName,
                        g2.Aggregate(new List<GlobalString>(), (a, b) => _f2(a, b.subCode))))
                    .ToList();
            }
        }

        public List<GlobalString> FilterByParentSubCodeId(string generalId, string parentSubCodeId)
        {
            using (var db = new LMISEntities())
            {
                return db.SubCodes
                    .Where(r => r.GeneralID == generalId && r.ParentSubCodeID == parentSubCodeId)
                    .GroupBy(r => r.SubID)
                    .ToList()
                    .Select(g => new GlobalString(g.Aggregate(new List<LocalString>(), (a, b) => _f1(a, b)), g.Key))
                    .ToList();
            }
        }

        public string GetSubCode(string subId, int languageId)
        {
            var result = (from subCode in _context.SubCodes
                          where subCode.SubID == subId && subCode.LanguageID == languageId

                          select subCode.Name).FirstOrDefault();
            return result;
        }
     
        public SubCodeVm GetSubCodeModel(string subId, int languageId)
        {
            var result = (from subCode in _context.SubCodes
                          join generalCode in _context.GeneralCodes on subCode.GeneralID equals generalCode.GeneralID
                          where subCode.SubID == subId && subCode.LanguageID == languageId && generalCode.LanguageID == languageId
                          orderby subCode.Name
                          select
                     new SubCodeVm
                     {
                         SubID = subCode.SubID,
                         GeneralID = subCode.GeneralID,
                         LanguageID = subCode.LanguageID,
                         Name = subCode.Name,
                         ParentSubCodeID = subCode.ParentSubCodeID,
                         GeneralName = generalCode.Name
                     }
                    );
            return result.FirstOrDefault();


        }
        public int Save(SubCodeVm subCode)
        {
            SubCode item = new SubCode()
            {
                GeneralID = subCode.GeneralID,
                LanguageID = subCode.LanguageID,
                Name = subCode.Name,
                SubID = subCode.SubID,
                ParentSubCodeID = subCode.ParentSubCodeID
                

            };

            int affectedRows = _context.SubCodes.Where(s => s.Name == item.Name && s.LanguageID == item.LanguageID && s.GeneralID == item.GeneralID).Count() > 0 ? -1 : 1;
            if (affectedRows == 1)
            {
                _context.SubCodes.Add(item);
                affectedRows = _context.SaveChanges();
            }
            return affectedRows;
        }

        public int Save(List<SubCodeVm> subCode)
        {
            List<SubCode> Litem = new List<SubCode>();
            ;
            SubCode item = new SubCode()
            {
                GeneralID = subCode[0].GeneralID,
                LanguageID = subCode[0].LanguageID,
                Name = subCode[0].Name,
                SubID = subCode[0].SubID,
                ParentSubCodeID = subCode[0].ParentSubCodeID


            };
            Litem.Add(item);
            item = new SubCode()
            {
                GeneralID = subCode[1].GeneralID,
                LanguageID = subCode[1].LanguageID,
                Name = subCode[1].Name,
                SubID = subCode[1].SubID,
                ParentSubCodeID = subCode[1].ParentSubCodeID


            };
            Litem.Add(item);
            item = new SubCode()
            {
                GeneralID = subCode[2].GeneralID,
                LanguageID = subCode[2].LanguageID,
                Name = subCode[2].Name,
                SubID = subCode[2].SubID,
                ParentSubCodeID = subCode[2].ParentSubCodeID


            };
            Litem.Add(item);
            int LanguageID;
            LanguageID = Litem[0].LanguageID;
            string GeneralID = Litem[0].GeneralID;
            string Name = Litem[0].Name;
            var affectedRows = _context.SubCodes.Count(s => s.Name == Name && s.LanguageID == LanguageID && s.GeneralID == GeneralID);
            if (affectedRows == 0)
            {
                 LanguageID = Litem[1].LanguageID;
                 GeneralID = Litem[1].GeneralID;
                 Name = Litem[1].Name;
                 affectedRows = _context.SubCodes.Count(s => s.Name == Name && s.LanguageID == LanguageID && s.GeneralID == GeneralID);
            }
            if (affectedRows == 0)
            {
                LanguageID = Litem[2].LanguageID;
                GeneralID = Litem[2].GeneralID;
                Name = Litem[2].Name;
                affectedRows = _context.SubCodes.Count(s => s.Name == Name && s.LanguageID == LanguageID && s.GeneralID == GeneralID);
            }
            if (affectedRows == 0)
            {
              
                _context.SubCodes.AddRange(Litem);
                affectedRows = _context.SaveChanges();
            }
            else
            {
                affectedRows = 0;
            }
            return affectedRows;
        }

        public int Update(SubCodeVm subCode)
        {
            int affectedRows = 0;
            var _subCode = (from sc in _context.SubCodes
                            where  sc.SubID == subCode.SubID && sc.LanguageID == subCode.LanguageID

                            select sc).FirstOrDefault();
            if (_subCode == null)
            {
               var  item = new SubCode()
                {
                    GeneralID = subCode.SubID.Substring(0,3),
                    LanguageID = subCode.LanguageID,
                    Name = subCode.Name,
                    SubID = subCode.SubID,
                    ParentSubCodeID = subCode.ParentSubCodeID

                };
                affectedRows = _context.SubCodes.Where(s => s.Name == item.Name && s.LanguageID == item.LanguageID && s.GeneralID == item.GeneralID).Count() ;
                if (affectedRows == 0)
                {
                    _context.SubCodes.Add(item);
                    _context.Entry(item).State = EntityState.Added;
                }
            }
            else
            {
                _subCode.Name = subCode.Name;
                affectedRows = _context.SubCodes.Where(s => s.Name == _subCode.Name && s.LanguageID == _subCode.LanguageID && s.GeneralID == _subCode.GeneralID && s.SubID != _subCode.SubID).Count() ;
                if (affectedRows == 0)
                {
                    _context.SubCodes.Attach(_subCode);
                    _context.Entry(_subCode).State = EntityState.Modified;
                }

               
            }
            if (affectedRows == 0)
            {
                affectedRows = _context.SaveChanges();
            }
            else
            {
                affectedRows = 0;
            }
            return affectedRows;
        }
        public int Delete(string  subId)
        {
            int affectedRows;
            affectedRows = _context.DimThemes.Count(c => c.ThemeType == subId);
            affectedRows += _context.ThemesVariables.Count(c => c.VariableID == subId);
            affectedRows += _context.Feedbacks.Count(c => c.FeedbackTypeId == subId);
            affectedRows += _context.EmployeerTemplates.Count(c => c.TemplateType == subId);
            affectedRows += _context.EmployerGuidancekits.Count(c => c.EmployerGuidanceType == subId);
            affectedRows += _context.EmployerGuidancekits.Count(c => c.EmployerGuidanceType == subId);
            affectedRows += _context.Events.Count(c => c.EventTypeID == subId);
            affectedRows += _context.FAQs.Count(c => c.FAQCategoryID == subId);
            affectedRows += _context.HelpfulLinks.Count(c => c.GroupID == subId);
            affectedRows += _context.IndividualDetails.Count(c => c.GenderId == subId || c.MaritalStatusId == subId || c.MilitaryStatus_Id == subId || c.NationalityId == subId || c.CountryID == subId || c.CityID == subId);
            affectedRows += _context.IndividualEducationlevels.Count(c => c.LevelOfEducation == subId || c.Degree == subId);
            affectedRows += _context.IndividualExperienceDetails.Count(c => c.EmploymentJobTitle == subId || c.TypeOfEmployment == subId);
            affectedRows += _context.IndividualOtherSkills.Count(c => c.IndustryId == subId || c.SkillLevelId == subId);
            affectedRows += _context.IndividualSkillsDetails.Count(c => c.SkillID == subId || c.SkillLevelID == subId || c.YearsOf_Experience == subId || c.IndustryID == subId );
            affectedRows += _context.IndividualSkillsDetails_Delete.Count(c => c.Skilllevel == subId || c.YearsofExperience == subId || c.SkillType == subId);
            affectedRows += _context.InformalSectorMethodDefinitions.Count(c => c.MethodID == subId);
            affectedRows += _context.InformalSectorMethodLiteratures.Count(c => c.MethodID == subId);
            affectedRows += _context.JobAppliedAdditionalDocs.Count(c => c.AdditionalDocTypeID == subId);
            affectedRows += _context.JobOffers.Count(c => c.JobTiltleID == subId || c.GenderID == subId || c.EmploymentTypeID == subId || c.CountryID == subId || c.CityID == subId || c.SalaryCurrencyID == subId);
            affectedRows += _context.JobOfferAdditionalDocs.Count(c => c.AdditionalDocTypeID == subId);
            affectedRows += _context.JobOfferEducationLevels.Count(c => c.EducationLevelID == subId);
            affectedRows += _context.JobOfferEducationLevelDetails.Count(c => c.EdLevelID == subId);
            affectedRows += _context.jobOfferMedicalDetails.Count(c => c.MedicalID == subId);
            affectedRows += _context.jobOfferSkillsDetails.Count(c => c.SkillID == subId || c.SkillLevelID == subId || c.YearsOf_Experience == subId || c.SkillTypeID == subId || c.IndustryID == subId);
            affectedRows += _context.JobOtherSkills.Count(c => c.IndustryId == subId || c.IndustryId == subId);
            affectedRows += _context.JobSeekersGuidancekits.Count(c => c.JobSeekerGuidanceType == subId);
            affectedRows += _context.JobseekerTemplates.Count(c => c.TemplateType == subId);
            affectedRows += _context.NewHires.Count(c => c.NewHireIdType == subId);
            affectedRows += _context.NewTrainees.Count(c => c.NewTraineeIdType == subId);
            affectedRows += _context.OrganizationContact_Info.Count(c => c.JobTitleID == subId);
            affectedRows += _context.OrganizationDetails.Count(c => c.OrganizationSize == subId || c.CountryID == subId || c.CityID == subId || c.EconomicActivity == subId || c.IndustryType == subId);
            affectedRows += _context.PolicesOfEgypts.Count(c => c.InfoLang == subId);
            affectedRows += _context.FAQs.Count(c => c.FAQCategoryID == subId);
            affectedRows += _context.Skills.Count(c => c.SkillID == subId || c.UnitID == subId || c.IndustryId == subId || c.SkillLevel_ID == subId);
            affectedRows += _context.TrainingOffers.Count(c => c.CourseNameID == subId || c.TimeZone == subId || c.CountryID == subId || c.CityID == subId);
            affectedRows += _context.TrainingOfferOccurrences.Count(c => c.OccurrenceID == subId);
            affectedRows += _context.TrainingOtherSkills.Count(c => c.IndustryId == subId || c.SkillLevelId == subId);
            affectedRows += _context.TrainingSkillDetails.Count(c => c.SkillID == subId || c.SkillLevelID == subId || c.IndustryID == subId || c.SkillTypeID == subId);
            if (affectedRows > 0)
            {
                return 0;
            }
            _context.SubCodes.RemoveRange(_context.SubCodes.Where(x => x.SubID == subId));
              affectedRows = _context.SaveChanges();
             return affectedRows;
        }

        public string GetMaxSubCode(string generalId, int languageId)
        {
            var result = _context.SubCodes.Where(subCode => subCode.GeneralID == generalId && subCode.LanguageID == languageId).Max(m => m.SubID); 
            return result;
        }
      
    }
}