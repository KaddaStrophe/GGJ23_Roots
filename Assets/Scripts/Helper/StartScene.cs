using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScene : MonoBehaviour {
    public void StartSceneWithName(string sceneName) {
        SceneManager.LoadScene(sceneName);
    }
}
