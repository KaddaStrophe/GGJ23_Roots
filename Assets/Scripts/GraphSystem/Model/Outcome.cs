using UnityEngine;

namespace Assets.Scripts.GraphSystem
{
    public class Outcome : ScriptableObject
    {
        [Multiline]
        public string content;
        public Node nextNode;
    }
}
