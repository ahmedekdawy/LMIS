using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMIS.Infrastructure.Data.Entities.Individual
{
    public class IndividualProfileEducationVM
    {
        public decimal IndividualEducationlevelID { get; set; }
        public decimal PortalUsersID { get; set; }
        public string LevelOfEducation { get; set; }
        public string FacultyID { get; set; }
        public string CertificationTypeID { get; set; }
        public int GraduationYear { get; set; }
        public double? GradePrecentage { get; set; }
        public string Grade { get; set; }
        public double? GradeGPA { get; set; }
        public string Degree { get; set; }
        public string InstitutionType { get; set; }
        public string InstitutionName { get; set; }


        public string EducationName { get; set; }
        public string LevelOfEducationName { get; set; }

    }
}
