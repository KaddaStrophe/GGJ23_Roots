using UnityEngine;

namespace Assets.Scripts.GraphSystem
{
    public class Outcome : ScriptableObject
    {
        [Multiline]
        public string answer;
        public Node nextNode;
    }
}
