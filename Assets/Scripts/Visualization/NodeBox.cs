using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TheRuinsBeneath.Visualization {
    public class NodeBox : MonoBehaviour {
        [SerializeField]
        NodeVisualizer visualizer = default;
        [SerializeField]
        TextMeshProUGUI contentTextMesh = default;
        [SerializeField]
        Button buttonChoiceA = default;
        [SerializeField]
        Button buttonChoiceB = default;
        [Header("Debug Info")]
        [SerializeField]
        Node nodeInfo = default;


        protected void OnValidate() {
            if (!contentTextMesh) {
                TryGetComponent(out contentTextMesh);
            }
        }
        public void SetContent(Node node) {
            nodeInfo = node;
            contentTextMesh.text = node.content;
            if (buttonChoiceA) {
                buttonChoiceA.GetComponentInChildren<TextMeshProUGUI>().text = node.outcomesNames[0];
            }
            if (buttonChoiceB) {
                buttonChoiceB.GetComponentInChildren<TextMeshProUGUI>().text = node.outcomesNames[1];
            }
        }

        public void ChooseOutcomeA() {
            visualizer.ReceiveOutcome(nodeInfo.outcomes[0]);
        }
        public void ChooseOutcomeB() {
            visualizer.ReceiveOutcome(nodeInfo.outcomes[1]);
        }

        public void SetVisualizer(NodeVisualizer visualizer) {
            this.visualizer = visualizer;
        }
    }
}