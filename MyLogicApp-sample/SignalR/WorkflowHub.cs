
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.SignalR;
using MyLogicApp_sample.BLL.Account;
using System.Linq;

namespace MyLogicApp_sample.SignalR
{
    public class WorkflowHub : Hub
    {
        public void Notify()
        {
            Clients.All.receiveNotification();
        }

        public void NewTodoNotify(string userId)
        {
            var user = MembershipTools.NewUserManager().FindById(userId);
            Clients.User(user.UserName).increaseTodoCount();
        }
    }
}