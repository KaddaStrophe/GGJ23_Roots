using Assets.Scripts.GraphSystem;
using UnityEngine;

namespace TheRuinsBeneath {
    public class GameManager : MonoBehaviour {

        [SerializeField]
        GraphCommander graphCommander = default;

        protected void OnValidate() {
            if (!graphCommander) {
                graphCommander = FindObjectOfType<GraphCommander>();
            }
        }
    }
}