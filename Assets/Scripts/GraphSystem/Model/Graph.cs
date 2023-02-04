using UnityEngine;

[CreateAssetMenu(menuName = "Textadventure/Graph")]
public class Graph : ScriptableObject
{
    public Node startNode;

    [HideInInspector]
    public Node currentNode;

    public void Init() {

        currentNode = startNode;
        startNode.Init();
    }
}
