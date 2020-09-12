using System;
using System.Collections.Generic;
using System.Threading;

namespace LMIS.Infrastructure.Data.DTOs
{
    public class UserInfo
    {
        //private readonly Guid _userId;          //from Aspnet_Users: not nullable
        //private readonly long _portalUserId;    //from PortalUsers: not nullable
        //private readonly long? _orgContactId;   //from OrganizationContact_Info: null if no record && PortalUsers.Category = Individual

        //public UserInfo(Guid uid, long puid, long? ocid = null)
        //{
        //    _userId = uid;
        //    _portalUserId = puid;
        //    _orgContactId = ocid;
        //}
        public string UserId { get; set; }// { get { return _userId; } }
        public decimal PortalUserId { get; set; }//{ get { return _portalUserId; } }
        public decimal? OrgContactId { get; set; }//{ get { return _orgContactId; } }
        public List<string> Roles { get; set; }
        public string UserName { get; set; }
        public bool IsIndividual { get; set; }
        public bool IsApproved { get; set; }
        public bool IsTrainingProvider { get; set; }
        public bool IsTrainingSeeker { get; set; }
        public bool IsEmployer { get; set; }
        public bool IsJobSeeker { get; set; }
        public bool IsResearcher { get; set; }
        public bool IsInternal { get; set; }
        public GlobalString FirstName { get; set; }
        public GlobalString LastName { get; set; }
        public string Email { get; set; }

        public string ConnectionId { get; set; }
        public int  UserGroup { get; set; }

         public bool Freeflag { get; set; }
    }
    public class UserInfoChat
    {
       
        public string UserId { get; set; }// { get { return _userId; } }
         public string UserName { get; set; }
        public GlobalString FirstName { get; set; }
        public GlobalString LastName { get; set; }
        public string Email { get; set; }
        public string ConnectionId { get; set; }
        public string UserGroup { get; set; }

        public bool Freeflag { get; set; }
        public bool IsAdmin { get; set; }
        public decimal   PortalUserId { get; set; } 
        

    }
}