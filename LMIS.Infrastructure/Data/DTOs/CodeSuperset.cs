using System.Collections.Generic;

namespace LMIS.Infrastructure.Data.DTOs
{
    public class CodeSuperset
    {
        public string Id;
        public string Desc;
        public List<GlobalString> Subset;
        public CodeSuperset(string id, string desc, List<GlobalString> subset)
        {
            Id = id;
            Desc = desc;
            Subset = subset;
        }
    }
}