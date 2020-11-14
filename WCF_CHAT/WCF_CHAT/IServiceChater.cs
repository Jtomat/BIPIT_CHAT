using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WCF_CHAT
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени интерфейса "IServiceChater" в коде и файле конфигурации.
    [ServiceContract(CallbackContract = typeof(IServerChaterCallBack))]
    public interface IServiceChater
    {

        [OperationContract] 
        int Connect(string name);

        [OperationContract]
        void Disconnect(int id);


        [OperationContract(IsOneWay = true)]
        void SendMessage(string mes, int id);
    }


    public interface IServerChaterCallBack
    {
        [OperationContract(IsOneWay = true)]
        void MessageCallBack(string mes);
    }
}
