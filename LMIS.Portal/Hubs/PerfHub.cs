using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure.MappingViews;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using LMIS.Infrastructure.Data.DTOs;
using LMIS.Infrastructure.Data.Entities;
using LMIS.Portal.Helpers;
using Microsoft.AspNet.Identity;
using WebGrease.Css.Extensions;

namespace LMIS.Portal.Hubs
{
    public class PerfHub : Hub
    {

        static List<UserInfoChat> UsersList = new List<UserInfoChat>();
        static List<MessageInfoVM> MessageList = new List<MessageInfoVM>();
        private string groupid=string.Empty ;
        private bool _freeFlag = true ;
        public void Send(string message)
        {


            
            groupid  =UsersList.Where(u => u.UserName == Context.User.Identity.Name  ).Select(s => s.UserGroup).FirstOrDefault();
            if (!string.IsNullOrEmpty(groupid))
            {
                var _PortalUserID =UsersList.Where(u => u.UserGroup == groupid &&u.IsAdmin==false  ).Select(s => s.PortalUserId ).FirstOrDefault();
                 var _userid =UsersList.Where(u => u.UserGroup == groupid &&u.IsAdmin ).Select(s => s.UserId).FirstOrDefault();
                if (_PortalUserID > 0 && !string.IsNullOrEmpty(_userid))
                {
                    MessageList.Add(new MessageInfoVM()
                    {
                        UserName = Context.User.Identity.Name,
                        MsgDate = DateTime.Now,
                        UserGroup = groupid,
                        Message = message,
                        UserId = _userid,
                        PortalUserID = _PortalUserID
                    });
                    Clients.Group(groupid).newMessage(
                        DateTime.Now.ToShortTimeString() + " : <strong>" + Context.User.Identity.Name +
                        " says:</strong> " + message
                        );
                }
            }
            else
            {
                Clients.Client(Context.ConnectionId).newMessage(
               "<strong>Sory All Administrators are busy, please be patient and try again</strong> " 
               );
            }

            groupid = string.Empty;
        }
        public override System.Threading.Tasks.Task OnConnected()
        {
            var _LoggedUser = BllFactory.Singleton.AspNetUsersManager.GetUserInfo(Context.User.Identity.Name);


           
            if (Utils.CheckPermission(1, 14, _LoggedUser.Roles) > 0) //isadmin
            {
                if (UsersList.Where(u => u.UserName == Context.User.Identity.Name).FirstOrDefault() != null)
                {
                    groupid =
                       UsersList.Where(u => u.UserName == Context.User.Identity.Name)
                           .Select(s => s.UserGroup)
                           .FirstOrDefault();
                }
                else
                {
                    groupid = (UsersList.Count + 1).ToString();
                }

                if (UsersList.Where(u => u.UserName == Context.User.Identity.Name).FirstOrDefault() == null)
                {
                    var firstFreeuser = UsersList.Where(u => u.Freeflag && u.IsAdmin == false).FirstOrDefault();
                    if (firstFreeuser != null)
                    {
                        _freeFlag = false;
                        firstFreeuser.UserGroup = groupid;
                        firstFreeuser.Freeflag = _freeFlag;
                        Groups.Add(firstFreeuser.ConnectionId, groupid);
                    }
                    Groups.Add(Context.ConnectionId, groupid);
                    UsersList.Add(new UserInfoChat() { UserName = Context.User.Identity.Name,UserId=Context.User.Identity.GetUserId(), ConnectionId = Context.ConnectionId, Freeflag = _freeFlag, UserGroup = groupid, IsAdmin = true });
                }
            }
            else
            {

                if (UsersList.Where(u => u.UserName == Context.User.Identity.Name).FirstOrDefault() == null)
                {
                    var firstFreeAdmin = UsersList.Where(u => u.Freeflag && u.IsAdmin).FirstOrDefault();
                    if (firstFreeAdmin != null)
                    {
                        _freeFlag = false;
                        groupid = firstFreeAdmin.UserGroup;
                        firstFreeAdmin.Freeflag = _freeFlag;
                        Groups.Add(Context.ConnectionId, groupid);
                    }



                    UsersList.Add(new UserInfoChat() { UserName = Context.User.Identity.Name, ConnectionId = Context.ConnectionId, Freeflag = _freeFlag, UserGroup = groupid, IsAdmin = false, PortalUserId = _LoggedUser.PortalUserId });

                }


            }

            return base.OnConnected();
        }

        public override System.Threading.Tasks.Task OnDisconnected(bool stopCalled)
        {

            var item = UsersList.FirstOrDefault(x => x.ConnectionId == Context.ConnectionId);
            if (item != null)
            {

                //save conversation to dat abase
                var groupid = UsersList.Where(u => u.UserName == Context.User.Identity.Name).Select(s => s.UserGroup).FirstOrDefault();
                var conversion = MessageList.Where(w => w.UserGroup == groupid).ToList();
                if (conversion.Count > 1)
                {

                    BllFactory.Singleton.ChatLog.Insert(conversion);
              
                }
                UsersList.Remove(item);

             

                    //user logged off == user
                    try
                    {
                        var stradmin = (from s in UsersList where (s.UserGroup == item.UserGroup)  select s).First();
                        //become free
                        stradmin.Freeflag  = true ;
                        stradmin.UserGroup = string.Empty;
                        Groups.Remove(Context.ConnectionId, item.UserGroup);
                    }
                    catch
                    {
                        //***** Return to Client *****
                        Clients.Caller.NoExistAdmin();
                    }

                    var firstFreeuser = UsersList.Where(u => u.Freeflag && u.IsAdmin == false).FirstOrDefault();
                    var firstFreeAdmin = UsersList.Where(u => u.Freeflag && u.IsAdmin).FirstOrDefault();
                    if (firstFreeuser != null &&firstFreeAdmin != null)
                    {
                        _freeFlag = false;
                        groupid = firstFreeAdmin.UserGroup;
                        firstFreeuser.Freeflag = _freeFlag;
                        firstFreeAdmin.Freeflag = _freeFlag;
                        firstFreeuser.UserGroup = groupid;
                        Groups.Add(firstFreeuser.ConnectionId, groupid);
                        Groups.Add(firstFreeAdmin.ConnectionId, groupid);
                    }
                    
                   
                    
                    



            }

            return base.OnDisconnected(stopCalled);
        }
      

        public void sendEmail( )
        {
            var  groupid  =UsersList.Where(u => u.UserName == Context.User.Identity.Name  ).Select(s => s.UserGroup).FirstOrDefault();
            var conversion = string.Join(" ", MessageList.Where(w => w.UserGroup == groupid).Select(s=> "<p>"+s.MsgDate +" : "+s.UserName+" says: " +s.Message+"<p>" ));
            if (!string.IsNullOrEmpty(conversion))
            {


                Utils.SendEmailAttachments(new List<string>() {Context.User.Identity.Name}, "Chat Conversion",conversion, null);
                Clients.Client(Context.ConnectionId).newMessage("<strong>Chat Conversion was Sent Check your Email</strong> ");
            }
        }

    }

  
}