using LMIS.Infrastructure.Data.DTOs;

using System.Collections.Generic;

namespace LMIS.Infrastructure.Data.Entities
{
    public class OfficeVm
    {
        public long OfficeId { get; set; }
        public GlobalString Title { get; set; } //100
        public GlobalString Address { get; set; } //200
        public GlobalString District { get; set; } //50
        public string Telephone { get; set; } //20
        public string Mobile { get; set; } //20
        public string Fax { get; set; } //20
        public string Hotline { get; set; } //20
        public Dictionary<string, object> Extras { get; set; }
    }
}