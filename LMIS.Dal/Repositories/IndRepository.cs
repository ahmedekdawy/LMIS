using LMIS.Dal.Entity;
using LMIS.Infrastructure.Data.DTOs;
using LMIS.Infrastructure.Enums;
using LMIS.Infrastructure.Interfaces.Repositories;

using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Linq;

namespace LMIS.Dal.Repositories
{
    public class IndRepository : IIndRepository
    {
        public List<Dictionary<string, object>> Search(long jobOfferId, int langId = 1)
        {
            using (var db = new LMISEntities())
            {
                var filter = jobOfferId > 0;
                var reqSkills = new List<string>();
                var reqEdLevels = new List<string>();
                var ret = new List<Dictionary<string, object>>();

                if (filter)
                {
                    var offer = db.JobOffers.AsNoTracking()
                        .Where(a => a.JobOfferID == jobOfferId)
                        .Select(a => new
                        {
                            expFrom = a.ExpYearFrom,
                            expTo = a.ExpYearTo,
                            skills = a.jobOfferSkillsDetails,
                            medConds = a.jobOfferMedicalDetails,
                            edLevels = a.JobOfferEducationLevels
                        })
                        .ToList()
                        .Select( a => new
                        {
                            a.expFrom,
                            a.expTo,
                            skills = a.skills.Select(dr => dr.SkillID).Distinct().ToList(),
                            medConds = a.medConds.Select(dr => dr.MedicalID).ToList(),
                            edLevels = a.edLevels.Select(dr => dr.EducationLevelID).Distinct().ToList()
                        })
                        .Single();

                    reqSkills = offer.skills;
                    reqEdLevels = offer.edLevels;

                    ret.Add(new Dictionary<string, object>() {{
                        "Filters", new
                        {
                            ExpFrom = offer.expFrom,
                            ExpTo = offer.expTo,
                            Skills = offer.skills,
                            MedConds = offer.medConds,
                            EdLevels = offer.edLevels
                        }
                    }});
                }

                ret.AddRange(db.IndividualDetails
                    .AsNoTracking()
                    .Where(r => r.Is_Approved == (byte)Approval.Approved)
                    .Select(a => new
                    {
                        Candidate = a,
                        Details = a.IndividualDetailsDets,
                        Experience = a.IndividualExperienceDetails.Where(dr => dr.IsDeleted == null && (dr.CurrentEmploymentStatus == 1 || dr.EmploymentEndDate.HasValue))
                            .Select(dr => new
                            {
                                days = SqlFunctions.DateDiff("DAY", dr.EmploymentStartDate, dr.CurrentEmploymentStatus == 1 ? DateTime.Today : dr.EmploymentEndDate) ?? 0,
                                job = dr.EmploymentJobTitle
                            }),
                        Skills = a.IndividualSkillsDetails.Where(dr => dr.IsDeleted == null)
                            .Select(dr => new
                            {
                                id = dr.SkillID,
                                desc = SqlUdf.SubCodeName(dr.SkillID, langId)
                            }),
                        CountryDesc = SqlUdf.SubCodeName(a.CountryID, langId),
                        CityDesc = SqlUdf.SubCodeName(a.CityID, langId),
                        GenderDesc = SqlUdf.SubCodeName(a.GenderId, langId),
                        MedCondDesc = SqlUdf.SubCodeName(a.IndividualMedicalID, langId),
                        EdLevels = a.IndividualEducationlevels.Where(dr => dr.IsDeleted == null)
                            .Select(dr => new
                            {
                                id = dr.LevelOfEducation,
                                desc = SqlUdf.SubCodeName(dr.LevelOfEducation, langId)
                            })
                    })
                    .ToList()
                    .Select(a => new Dictionary<string, object>
                    {
                        { "Id", a.Candidate.PortalUsersID },
                        { "FirstName", (new GlobalString(a.Details.Select(d => new LocalString(d.LanguageID, d.FirstName)).ToList())).ToLocalString((Language) langId, true).T },
                        { "LastName", (new GlobalString(a.Details.Select(d => new LocalString(d.LanguageID, d.LastName)).ToList())).ToLocalString((Language) langId, true).T },
                        { "Experience", a.Experience.Select(e => e.days).Sum() },
                        { "Jobs", a.Experience.Select(e => e.job).Distinct().ToList() },
                        { "SkillRating", reqSkills.Any() ? a.Skills.Select(dr => reqSkills.Contains(dr.id)).Distinct().Count() / reqSkills.Count() : -1 },
                        { "Skills", a.Skills.Select(s => s.id).Distinct().ToList() },
                        { "Country", new CodeSet { id = a.Candidate.CountryID, desc = a.CountryDesc } },
                        { "City", new CodeSet { id = a.Candidate.CityID, desc = a.CityDesc } },
                        { "Gender", new CodeSet { id = a.Candidate.GenderId, desc = a.GenderDesc } },
                        { "MedCond", new CodeSet { id = a.Candidate.IndividualMedicalID, desc = a.MedCondDesc } },
                        { "EdLevels", a.EdLevels.Select(s => s.id).Distinct().ToList() },
                        { "EdlevelMatching", !reqEdLevels.Any() || a.EdLevels.Any(dr => reqEdLevels.Contains(dr.id)) }
                    })
                    .ToList());

                return ret;
            }
        }
    }
}