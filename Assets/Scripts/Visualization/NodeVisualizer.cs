using System;
using System.Collections;
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
        NodeBox nodeBoxDecision = default;

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
            Assert.IsTrue(nodeEventChannel);
            Assert.IsTrue(graphCommander);
            if (spawnNow) {
                spawnNow = false;
                SpawnNextNode();
            }
        }

        protected void Start() {
            var root = graphCommander.ProvideStart();
            SpawnNode(root);
        }

        // DEBUG METHOD
        void SpawnNextNode() {
            var nextNode = graphCommander.OutcomeUpdateNoDecision();
            SpawnNode(nextNode);
        }

        void SpawnNode(Node node) {
            var prefab = nodeBox;
            // Set Node Values
            if(node.IsDecision()) {
                prefab = nodeBoxDecision;
            }
            prefab.SetContent(node);
            prefab.SetVisualizer(this);
            // Spawn Box
            var instance = Instantiate(prefab.transform);
            currentNode = instance;

            instance.SetParent(nodeCanvas.transform);
            // TODO: In NodeBox?
            var newPosVector = new Vector3(currentNode.transform.position.x, currentNode.transform.position.y + GetFloatDepth(node.depth), currentNode.transform.position.z);
            instance.SetPositionAndRotation(newPosVector, Quaternion.identity);
            
            // Camera Info Call ring ring
            nodeEventChannel.RaiseOnNodeChange(node, instance.gameObject);
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

        public void ReceiveOutcome(Outcome outcome) {
            var nextNode = graphCommander.OutcomeUpdate(outcome);
            SpawnNode(nextNode);
        }
    }
}