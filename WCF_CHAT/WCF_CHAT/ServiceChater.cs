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
using System.Text.RegularExpressions;

namespace WCF_CHAT
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени класса "ServiceChater" в коде и файле конфигурации.
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class ServiceChater : IServiceChater
    {
        List<ServerUser> _users = new List<ServerUser>();
        int _nextId = 1;
        object locker = new object();

        public ServiceChater()
        {
            GetUsersFromLog();
            PrintLog($"[WARN] HOST running on { Dns.GetHostAddresses(Dns.GetHostName())[0].ToString()}.");
        }
        void GetUsersFromLog()
        {
            
            var reg = new Regex(@"\[([0-9\:\-\.]+)]\ \[INFO\] User ([\d+]):([A-zА-яёЁ\#\№\$\*\^\d]+) send message: ''([\s\WA-zА-я0-9ёЁ\^$]+)''");
            var lines = File.ReadAllLines("log.txt");
            foreach (var line in lines)
            {
                var match = reg.Match(line);
                if (match.Groups.Count > 2)
                {
                    if (_users.FindAll(i => i.ID == int.Parse(match.Groups[2].Value)).Count == 0){
                        var user = new ServerUser()
                        {
                            ID= int.Parse(match.Groups[2].Value),
                            Name = match.Groups[3].Value,
                            OperationContext = null
                        };
                        if (user.ID >= _nextId)
                            _nextId = user.ID + 1;
                        _users.Add(user);
                        SetHistoryByID(user.ID);
                    }
                }
            }
        }

        public int Connect(string name)
        {
            if (name == "Anonimus")
                name += "#" + (new Random()).Next(1, 256);
            var cur_name_user = _users.FirstOrDefault(i => i.Name == name);
            var user = (ServerUser)null;
            if (cur_name_user == null)
            {
                user = new ServerUser()
                {
                    ID = _nextId++,
                    Name = name,
                    OperationContext = OperationContext.Current,
                    Connected = true
                };
                _users.Add(user);
            }
            else 
            {
                if (cur_name_user.Connected)
                {
                    PrintLog($"[WARN] reject connection for {name}. Connection already exist.");
                    return -1;
                }
                cur_name_user.Connected = true;
                cur_name_user.OperationContext = OperationContext.Current;
                user = cur_name_user;
            }
            (new Thread(() => //иначе превышает время ожидания
            {
                Thread.Sleep(200);
                SendMessage($"{name} join the chat.");
                PrintLog($"[INFO] {name} join the chat.");
            })).Start();
            return user.ID;
        }

        public void Disconnect(int id)
        {
            var user =_users.FirstOrDefault(i=>i.ID==id);
            if (user != null)
            {
                user.Connected = false;
                //_users.Remove(user);
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
                if(user.Connected)
                    user.OperationContext.GetCallbackChannel<IServerChaterCallBack>().MessageCallBack(answer);
            }
            PrintLog($"[INFO] {(current_user!=null?($"User {current_user.ID}:{current_user.Name}"):"Server")} send message: ''{mes}''");
        }

        void PrintLog(string mes)
        {
            Console.WriteLine($"[{DateTime.Now.ToShortTimeString()}-{DateTime.Now.ToShortDateString()}] {mes}");
            lock (locker)
                File.AppendAllLines("log.txt",new string[] { ($"[{DateTime.Now.ToShortTimeString()}-{DateTime.Now.ToShortDateString()}] {mes}") });
        }

        public void GetHistory(int id)
        {
            var current_user = _users.FirstOrDefault(i => i.ID == id);
            if (current_user == null)
                return;
            foreach(var mes in current_user.MesHitory)
                current_user.OperationContext.GetCallbackChannel<IServerChaterCallBack>().MessageCallBack(mes);
        }

        void SetHistoryByID(int id)
        {
            var current_user = _users.FirstOrDefault(i => i.ID == id);
            if (current_user == null || current_user.Name == "Anonimus")
                return;
            var st = false;
            var result = new List<string>();
            var user_con_d = new Regex(@"\[([0-9\:\-\.]+)]\ \[INFO\] ([A-zА-яёЁ\#\№\$\*\^\d]+) (join|left) the chat\.");
            var reg = new Regex(@"\[([0-9\:\-\.]+)]\ \[INFO\] User ([\d+]):([A-zА-яёЁ\#\№\$\*\^\d]+) send message: ''([\s\WA-zА-я0-9ёЁ\^$]+)''");
            var lines = new string[0];
            lock(locker)
                lines = File.ReadAllLines("log.txt");
            foreach (var line in lines)
            {
                var match = user_con_d.Match(line);
                if (match.Groups.Count > 1 && current_user.Name == match.Groups[2].Value)
                {

                    st = match.Groups[3].Value == "join";
                    result.Add($"[{match.Groups[1].Value}] {match.Groups[2].Value} {match.Groups[3].Value} the chat.");
                    continue;
                }
                if (st)
                {
                    var mes = reg.Match(line);
                    if(mes.Groups.Count>1)
                        result.Add($"[{mes.Groups[1].Value}] {mes.Groups[3].Value}: {mes.Groups[3].Value}");
                }
            }
            current_user.MesHitory= result;
        }

        ~ServiceChater()
        {
            PrintLog("[WARN] HOST end running.");
        }

    }
}
