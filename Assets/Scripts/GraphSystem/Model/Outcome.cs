using UnityEngine;

namespace Assets.Scripts.GraphSystem
{
    public class Outcome : ScriptableObject
    {
        [SerializeField, Multiline]
        public string answer;
        [SerializeField]
        public Node nextNode;
    }
}
