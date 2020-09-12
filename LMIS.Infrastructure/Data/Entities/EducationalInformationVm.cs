using LMIS.Infrastructure.Data.DTOs;
using LMIS.Infrastructure.Enums;

using System;

namespace LMIS.Infrastructure.Data.Entities
{
    public class EducationalInformationVm
    {
        public GlobalString Grade { get; set; }
        public GlobalString Name { get; set; }
        public string EducationName { get; set; }
        public string EducationType { get; set; }
        public int graduationyear { get; set; }
        public string Degree { get; set; }
        public double Percentage { get; set; }
        public long IndividualEducationlevelID { get; set; }
        public string EducationalLevelId { get; set; }
        public string EducationalLevelName { get; set; }
        public long PortalUsersID { get; set; }
        public GlobalString InstitutionType { get; set; }
        public GlobalString CertificationType { get; set; }
        public GlobalString FacultyName { get; set; }
        public float GradeGPA { get; set; }        
    }
}
