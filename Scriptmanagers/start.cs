using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreenButton : MonoBehaviour
{
    public string sceneToLoad = "BE2SampleScene";

    public void StartGame()
    {
       SceneManager.LoadScene(1);
    }
}