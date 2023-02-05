using UnityEngine;

[CreateAssetMenu(menuName = "Textadventure/Graph")]
public class Graph : ScriptableObject {

    [SerializeField]
    public Node startNode;

    public void Init() {
        startNode.Init();
    }
}
