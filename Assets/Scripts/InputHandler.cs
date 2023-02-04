using TheRuinsBeneath.Visualization;
using UnityEngine;

namespace TheRuinsBeneath {
    public class InputHandler : MonoBehaviour {

        [SerializeField]
        NodeVisualizer visualizer = default;

        protected void OnValidate() {
            if (!visualizer) {
                visualizer = FindObjectOfType<NodeVisualizer>();
            }
        }

        void OnSubmit() {
            visualizer.SpawnNextNode();
        }
    }
}