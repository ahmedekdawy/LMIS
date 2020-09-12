using LMIS.Infrastructure.Data.DTOs;

using System.Collections.Generic;

namespace LMIS.Infrastructure.Data.Entities
{
    public class UnionVm
    {
        public class UnionCommittee
        {
            public CodeSet Gov { get; set; }
            public GlobalString Name { get; set; }
        }
        public long UnionId { get; set; }
        public GlobalString Name { get; set; } //200
        public GlobalString Address { get; set; } //1000
        public string Telephone { get; set; } //20, Nullable
        public string Fax { get; set; } //20, Nullable
        public string Website { get; set; } //150, Nullable
        public string Email { get; set; } //150, Nullable
        public string Logo { get; set; } //200, Nullable
        public List<GlobalString> Professions { get; set; } //1000
        public List<UnionCommittee> Committees { get; set; } //1000
        public Dictionary<string, object> Extras { get; set; }
    }
}