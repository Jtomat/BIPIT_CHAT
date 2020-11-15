using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace WCF_CHAT
{
    public class ServerUser
    {
        private OperationContext _operationContext;

        public int ID { get; set; }
        public string Name { get; set; }
        public bool Connected=false;
        public OperationContext OperationContext { get => _operationContext; set => _operationContext = value; }

        public List<string> MesHitory = new List<string>();
    }
}
