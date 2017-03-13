using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP2
{
    public class CustomCollection
    {
        protected NodeClass structure_Head;
        protected int structure_Count;

        protected CustomCollection()
        {
            structure_Head = null;
            structure_Count = 0;
        }
        protected void Add(object Data)
        {
            NodeClass Money = new NodeClass(Data, structure_Head);
            structure_Head = Money;
            structure_Count++;
        }
        public class NodeClass
        {
            private object node_Data;
            public NodeClass node_NextNode;
            public NodeClass(object Data, NodeClass Next)
            {
                node_Data = Data;
                node_NextNode = Next;
            }
            public object GetData()
            {
                return node_Data;
            }
        }
    }
}
