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
        [SerializeField]
        public float animationDelay = 0f;
        
        ITween[] tweens;

        protected void OnValidate() {
            if (!contentTextMesh) {
                TryGetComponent(out contentTextMesh);
            }
        }

        protected void OnEnable() {
            tweens = GetComponentsInChildren<ITween>();
        }
        public void SetContent(Node node) {
            nodeInfo = node;
            contentTextMesh.text = node.content;
            if (nodeInfo.IsDecision()) {
                if (buttonChoiceA) {
                    buttonChoiceA.GetComponentInChildren<TextMeshProUGUI>().text = node.outcomesNames[0];
                }
                if (buttonChoiceB) {
                    buttonChoiceB.GetComponentInChildren<TextMeshProUGUI>().text = node.outcomesNames[1];
                }
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

        public void SetAnimationDelay(float delay) {
            animationDelay = delay;
        }

        public void GreyOutContent() {
            foreach (var tween in tweens) {
                tween.GreyOut();
            }
            if(nodeInfo.IsDecision()) {
                buttonChoiceA.interactable = false;
                buttonChoiceA.gameObject.SetActive(false);
                buttonChoiceB.interactable = false;
                buttonChoiceB.gameObject.SetActive(false);
            }
        }
    }
}