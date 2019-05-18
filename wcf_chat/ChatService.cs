using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace wcf_chat
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class ChatService : IChatService
    {
        List<ServerUser> users = new List<ServerUser>();
        int nextid = 1;

        public int Connect(string name)
        {
            ServerUser user = new ServerUser()
            {
                ID = nextid,
                Name = name,
                OperCont = OperationContext.Current
            };
            nextid++;
            SendMsg(user.Name + " подключился!",0);
            users.Add(user);
            return user.ID;
        }

        public void Disconnect(int id)
        {
            var user = users.FirstOrDefault(i => i.ID == id);
            if (user != null)
            {
                users.Remove(user);
                SendMsg(user.Name + " отключился!",0);
            }
        }

        public void SendMsg(string msg, int id)
        {
            foreach (var item in users)
            {
                String answer = "[" + DateTime.Now.ToShortTimeString() + "]";

                var user = users.FirstOrDefault(i => i.ID == id);
                if (user != null)
                {
                    answer += user.Name + ": ";
                }
                answer += msg;

                item.OperCont.GetCallbackChannel<IChatSeviceCallback>().MsgCallback(answer);
            }
        }
    }
}
