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
        NodeBox nodeBoxDecision = default;

        [SerializeField, Range(100f, 1000f)]
        float dialogeXDistance = 200f;
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

        Transform currentLeftNodeTransform;
        Transform currentRightNodeTransform;
        Transform currentTransform;
        Node currentNode;
        bool inDialogue = false;
        bool leftNodeToUse = true;

        protected void OnValidate() {
            Assert.IsTrue(nodeEventChannel);
            if (!graphCommander) {
                graphCommander = FindObjectOfType<GraphCommander>();
            }
        }

        protected void Start() {
            var root = graphCommander.ProvideStart();
            SpawnNode(root);
        }

        // DEBUG METHOD
        void SpawnNextNode() {
            if (!currentNode.IsDecision()) {
                if (currentNode.outcomes.Count == 0) {
                    // TODO: Start Over Logic/Visuals
                    var nextNode = graphCommander.ProvideStart();
                    SpawnNode(nextNode);
                } else {
                    var nextNode = graphCommander.OutcomeUpdateNoDecision();
                    SpawnNode(nextNode);
                }
            }
        }

        public void ReceiveOutcome(Outcome outcome) {
            var nextNode = graphCommander.OutcomeUpdate(outcome);
            SpawnNode(nextNode);
        }

        void SpawnNode(Node node) {
            // Set Node Values
            var prefab = nodeBox;
            if (node.IsDecision()) {
                prefab = nodeBoxDecision;
            }
            prefab.SetVisualizer(this);
            prefab.SetAnimationDelay(Mathf.Abs(GetFloatDepth(node.depth)) / 500);

            // Spawn Box
            if (node.isStartOfScene) {
                SpawnAsNewScene(node, prefab);
            } else {
                SpawnAsDialog(node, prefab);
            }
        }

        void SpawnAsDialog(Node node, NodeBox prefab) {
            // current node is left or right node?
            if (!inDialogue) {
                inDialogue = true;
                leftNodeToUse = false;
            }
            // new node is other node
            Transform nodeToUse;
            if (!leftNodeToUse) {
                if (currentRightNodeTransform) {
                    currentRightNodeTransform.gameObject.SetActive(false);
                }
                currentLeftNodeTransform.GetComponent<NodeBox>().GreyOutContent();
                currentRightNodeTransform = CreateNodeBox(prefab, new Vector3(dialogeXDistance, 0f, 0f));
                nodeToUse = currentRightNodeTransform;
                currentTransform = currentRightNodeTransform;
                leftNodeToUse = true;
            } else {
                currentRightNodeTransform.GetComponent<NodeBox>().GreyOutContent();
                currentLeftNodeTransform.gameObject.SetActive(false);
                currentLeftNodeTransform = CreateNodeBox(prefab, Vector3.zero);
                nodeToUse = currentLeftNodeTransform;
                currentTransform = currentLeftNodeTransform;
                leftNodeToUse = false;
            }

            nodeToUse.GetComponent<NodeBox>().SetContent(node);
            currentNode = node;
        }

        Transform CreateNodeBox(NodeBox prefab, Vector3 posDelta) {
            var instance = Instantiate(prefab.transform);
            instance.SetParent(nodeCanvas.transform);
            var currentPos = Vector3.zero;
            if (currentLeftNodeTransform) {
                currentPos = currentLeftNodeTransform.position;
            }
            var newPosVector = new Vector3(currentPos.x + posDelta.x, currentPos.y + posDelta.y, currentPos.z);
            instance.SetPositionAndRotation(newPosVector, Quaternion.identity);
            return instance;
        }

        void SpawnAsNewScene(Node node, NodeBox prefab) {
            inDialogue = false;
            prefab.SetContent(node);
            var deltaVector = new Vector3(0f, GetFloatDepth(node.depth), 0f);
            var instance = CreateNodeBox(prefab, deltaVector);

            // Camera Info Call ring ring
            nodeEventChannel.RaiseOnNodeChange(node, instance.gameObject);

            currentLeftNodeTransform = instance;
            currentTransform = currentLeftNodeTransform;
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

        public void Advance() {
            // If animation is already finished
            if (!currentTransform.GetComponent<NodeBox>().AdvanceAnimation()) {
                SpawnNextNode();
            }
        }
    }
}