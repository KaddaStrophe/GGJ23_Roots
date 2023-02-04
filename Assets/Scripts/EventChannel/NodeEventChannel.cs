using UnityEngine;

namespace TheRuinsBeneath.EventChannel {
    [CreateAssetMenu(fileName = "SO_NodeEventChannel_New", menuName = "EventChannel/NodeEventChannel")]
    public class NodeEventChannel : ScriptableObject {
        public delegate void NodeCallback(Node node, GameObject gameObject);
        public NodeCallback OnNodeChange;

        public void RaiseOnNodeChange(Node node, GameObject gameObject) {
            OnNodeChange?.Invoke(node, gameObject);
        }
    }
}