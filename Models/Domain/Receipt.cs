using System.Collections.Generic;

namespace Models
{
    public class Receipt
    {
        public List<ReceiptNode> Nodes;
        public Receipt()
        {
            Nodes = new List<ReceiptNode>();
        }

        public void AddNode(ReceiptNode node)
        {
            Nodes.Add(node);
        }
    }
}

