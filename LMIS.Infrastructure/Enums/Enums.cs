namespace LMIS.Infrastructure.Enums
{
    public static class Tables
    {
        public static string GeneralCode = "GeneralCode";
        public static string SubCode = "SubCode";
        public static string DimThemes = "DimThemes";
    }

    public enum ResponseStatus
    {
        Exception = -1,
        Success = 0,
        ValidationError = 1,
        AuthenticationError = 2,
        AuthorizationError = 500,
        InRelation=-2,
        Exist=-3
    }

    public enum UserCat
    {
        Individual,
        Organization,
        Unknown
    }

    public enum UserClass
    {
        OrgAdmin,
        OrgContact,
        Individual,
        Unknown
    }

    public enum GeneralCode
    {
        Themes = 001,
        Gender = 002,
        Governorate = 003,
        Year = 004,
        MaritalStatus = 005,
        EducationLevel = 006,
        Age = 007,
        Sector = 008,
        Country = 009,
        Month = 010,
        Nationailty = 011,
        Militarystatus = 012,
        Type = 013,
        Medicalconditions = 014,
        EventType = 015,
        SchoolType = 016,
        InstitutionDegree = 088,
        InstitutionGrade = 089,
        Universities=038,
        UniversityCertificate = 090,
        UniversityGrade = 091,
        Faculties=039
    }

    public enum Approval
    {
        Pending = 1,
        Approved = 2,
        Rejected = 3
    }

    public enum Language
    {
        English = 1,
        French = 2,
        Arabic = 3
    }
}