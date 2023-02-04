using UnityEngine;

[CreateAssetMenu(fileName = "SO_NodeInformation_New", menuName = "Nodes/NodeInformation")]
public class NodeInformation : ScriptableObject {
    [SerializeField]
    string content = default;
    [SerializeField]
    Speaker speaker = default;
    [SerializeField]
    Depth depth = default;
    [SerializeField]
    bool isStartOfScene = false;
    [SerializeField]
    bool isDecision = false;
    [SerializeField]
    string[] answers = default;
    [SerializeField]
    string[] backgroundFlavourText = default;
    [SerializeField]
    NodeInformation[] children = default;
}
