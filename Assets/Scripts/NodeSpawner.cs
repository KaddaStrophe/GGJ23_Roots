using UnityEngine;

public class NodeSpawner : MonoBehaviour {
    [SerializeField]
    NodeEventChannel nodeEventChannel = default;
    [SerializeField]
    Transform node = default;
    [SerializeField]
    Vector2 relativePos = default;
    [SerializeField]
    bool spawnNow = false;

    protected void OnValidate() {
        if (!node) {
            TryGetComponent(out node);
        }
        if (spawnNow) {
            spawnNow = false;
            SpawnNodeAtRelPos();
        }
    }

    void SpawnNodeAtRelPos() {
        var instance = Instantiate(node);
        var newPosVector = new Vector3(transform.position.x + relativePos.x, transform.position.y + relativePos.y, transform.position.z);
        instance.SetParent(transform.parent);
        instance.SetPositionAndRotation(newPosVector, Quaternion.identity);
        nodeEventChannel.RaiseOnNodeSpawn(instance.gameObject);
    }

    public void SpawnNode(Vector3 relativePos) {
        this.relativePos = relativePos;
        SpawnNodeAtRelPos();
    }
}
