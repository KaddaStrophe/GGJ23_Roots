using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.GraphSystem
{
    public class Outcome : ScriptableObject
    {
        [Multiline]
        public string content;

        public Node nextNode;

        public Outcome(string content, Node nextNode)
        {
            this.content  = content;
            this.nextNode = nextNode;
        }
    }
}
