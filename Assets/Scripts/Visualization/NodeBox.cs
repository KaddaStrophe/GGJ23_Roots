using TMPro;
using UnityEngine;

public class NodeBox : MonoBehaviour {
    [SerializeField]
    TextMeshProUGUI contentTextMesh = default;
    [SerializeField]
    Node node = default;

    protected void OnValidate() {
        if (!contentTextMesh) {
            TryGetComponent(out contentTextMesh);
        }
    }
    public void SetContent(string content) {
        contentTextMesh.text = content;
    }

    public void SetSO(Node node) {
        this.node = node;
    }
}
