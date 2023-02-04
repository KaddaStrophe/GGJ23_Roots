using UnityEngine;

[CreateAssetMenu(menuName = "Textadventure/Graph")]
public class Graph : ScriptableObject
{
    public Node startNode;

    [HideInInspector]
    public Node currentNode;
    
    public Graph(Node startNode)
    {
        this.startNode = startNode;
    }
}
