using UnityEngine;

namespace TheRuinsBeneath.EventChannel {
    [CreateAssetMenu(fileName = "SO_NodeEventChannel_New", menuName = "EventChannel/NodeEventChannel")]
    public class NodeEventChannel : ScriptableObject {
        public delegate void NodeSpawnCallback(GameObject gameObject);
        public NodeSpawnCallback OnNodeSpawn;

        public void RaiseOnNodeSpawn(GameObject gameObject) {
            OnNodeSpawn?.Invoke(gameObject);
        }
    }
}