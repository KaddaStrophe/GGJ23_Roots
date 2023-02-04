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

        Transform currentNodeTransform;
        Node currentNode;

        protected void OnValidate() {
            Assert.IsTrue(nodeEventChannel);
            if(!graphCommander) {
                graphCommander = FindObjectOfType<GraphCommander>();            
            }
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
        public void SpawnNextNode() {
            if(!currentNode.IsDecision()) {
                var nextNode = graphCommander.OutcomeUpdateNoDecision();
                SpawnNode(nextNode);
            }
        }

        void SpawnNode(Node node) {
            var prefab = nodeBox;
            // Set Node Values
            if(node.IsDecision()) {
                prefab = nodeBoxDecision;
            }
            prefab.SetContent(node);
            prefab.SetVisualizer(this);
            prefab.SetAnimationDelay(Mathf.Abs(GetFloatDepth(node.depth))/500);
            // Spawn Box
            var instance = Instantiate(prefab.transform);
            instance.SetParent(nodeCanvas.transform);
            // TODO: In NodeBox?
            var currentPos = Vector3.zero;
            if(currentNodeTransform) {
                currentPos = currentNodeTransform.position;
            }
            var newPosVector = new Vector3(currentPos.x, currentPos.y + GetFloatDepth(node.depth), currentPos.z);
            instance.SetPositionAndRotation(newPosVector, Quaternion.identity);
            
            // Camera Info Call ring ring
            nodeEventChannel.RaiseOnNodeChange(node, instance.gameObject);
            currentNodeTransform = instance;
            currentNode = node;
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