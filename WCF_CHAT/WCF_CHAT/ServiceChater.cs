using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Configuration;
using System.Net;
using System.Threading;

namespace WCF_CHAT
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени класса "ServiceChater" в коде и файле конфигурации.
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class ServiceChater : IServiceChater
    {
        List<ServerUser> _users = new List<ServerUser>();
        int _nextId = 1;

        public ServiceChater()
        {
            PrintLog($"[WARN] HOST running on { Dns.GetHostAddresses(Dns.GetHostName())[0].ToString()}.");
        }
        

        public int Connect(string name)
        {
            _users.Add(new ServerUser()
            {
                ID = _nextId,
                Name = name,
                OperationContext = OperationContext.Current
            });
            (new Thread(() => //иначе превышает время ожидания
            {
                SendMessage($"{name} join the chat.");
                PrintLog($"[INFO] {name} join the chat.");
            })).Start();
            
            return _nextId++;
        }

        public void Disconnect(int id)
        {
            var user =_users.FirstOrDefault(i=>i.ID==id);
            if (user != null)
            {
                _users.Remove(user);
                SendMessage($"{user.Name} left the chat.");
                PrintLog($"[INFO] {user.Name} left the chat.");
            }
        }

        public void SendMessage(string mes, int id=0)
        {
            string answer = $"[{DateTime.Now.ToShortTimeString()}-{DateTime.Now.ToShortDateString()}] ";
            var current_user = _users.FirstOrDefault(i => i.ID == id);
            if (current_user != null)
                answer += $"{current_user.Name}: ";
            answer += mes;
            foreach (var user in _users)
            {
                user.OperationContext.GetCallbackChannel<IServerChaterCallBack>().MessageCallBack(answer);
            }
            PrintLog($"[INFO] {(current_user!=null?($"User {current_user.ID}:{current_user.Name}"):"Server")} send message: \"{mes}\"");
        }

        void PrintLog(string mes)
        {
            Console.WriteLine($"[{DateTime.Now.ToShortTimeString()}-{DateTime.Now.ToShortDateString()}] {mes}");
            File.AppendAllLines("log.txt",new string[] { ($"[{DateTime.Now.ToShortTimeString()}-{DateTime.Now.ToShortDateString()}] {mes}") });
        }
        ~ServiceChater()
        {
            PrintLog("[WARN] HOST end running.");
        }

    }
}
