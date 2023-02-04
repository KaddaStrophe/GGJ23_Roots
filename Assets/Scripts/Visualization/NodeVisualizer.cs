using System;
using Assets.Scripts.GraphSystem;
using TheRuinsBeneath.EventChannel;
using UnityEngine;
using UnityEngine.Assertions;

namespace TheRuinsBeneath.Visualization {
    public class NodeVisualizer : MonoBehaviour {
        [SerializeField]
        NodeEventChannel nodeEventChannel = default;
        [SerializeField]
        GraphCommander graphCommander = default;
        [SerializeField]
        Canvas nodeCanvas = default;
        [SerializeField]
        NodeBox nodeBox = default;

        [SerializeField]
        float depthSurfaceValue = default;
        [SerializeField]
        float depthHighValue = default;
        [SerializeField]
        float depthMidValue = default;
        [SerializeField]
        float depthLowValue = default;
        [SerializeField]
        float depthDeepValue = default;
        [SerializeField]
        float depthAbyssValue = default;

        [SerializeField]
        bool spawnNow = false;

        Transform currentNode;

        protected void OnValidate() {
            if (spawnNow) {
                spawnNow = false;
                SpawnNextNode();
            }
            Assert.IsTrue(nodeEventChannel);
            Assert.IsTrue(graphCommander);
        }
        void SpawnNextNode() {
            var nextNode = graphCommander.OutcomeUpdateNoDecision();
            SpawnSimpleNode(nextNode, currentNode.transform.position, nodeBox);
        }

        protected void Start() {
            var root = graphCommander.ProvideStart();
            //if (root.IsDecision()) {
            //    SpawnDecisionNode(root, transform.position, decisionBox);
            //} else {
            SpawnSimpleNode(root, transform.position, nodeBox);
            //}
        }

        void SpawnSimpleNode(Node node, Vector3 position, NodeBox prefab) {
            // Set Node Values
            prefab.SetSO(node);
            prefab.SetContent(node.content);
            // Spawn Box
            var instance = Instantiate(prefab.transform);
            currentNode = instance;
            var newPosVector = new Vector3(position.x, position.y + GetFloatDepth(node.depth), position.z);
            instance.SetParent(nodeCanvas.transform);
            instance.SetPositionAndRotation(newPosVector, Quaternion.identity);
            nodeEventChannel.RaiseOnNodeSpawn(instance.gameObject);
        }

        float GetFloatDepth(Depth depth) {
            return depth switch {
                Depth.DEPTH_SURFACE => depthSurfaceValue,
                Depth.DEPTH_HIGH => depthHighValue,
                Depth.DEPTH_MID => depthMidValue,
                Depth.DEPTH_LOW => depthLowValue,
                Depth.DEPTH_DEEP => depthDeepValue,
                Depth.DEPTH_ABYSS => depthAbyssValue,
                _ => throw new NotImplementedException(),
            };
        }
    }
}