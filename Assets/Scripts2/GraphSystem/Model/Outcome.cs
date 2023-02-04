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
        public string text;

        public Node nextNode;

        public Outcome(string text, Node nextNode)
        {
            this.text = text;
            this.nextNode = nextNode;
        }
    }
}
