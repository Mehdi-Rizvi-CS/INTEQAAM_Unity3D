using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu1 : MonoBehaviour
{
    // This function will be called when the PLAY button is clicked
    public void PlayGame1()
    {
        // Replace "YourSceneName" with the exact name of the scene you want to load
        // Alternatively, use SceneManager.LoadScene(1) if it's the next scene in the Build Settings
        SceneManager.LoadScene("v0.3"); 
    }

    // This function will be called when the QUIT button is clicked
    public void QuitGame1()
    {
        Debug.Log("Game Exited"); 

        // Stops Play mode in the Unity Editor
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif

        // Quits the compiled application
        Application.Quit();
    }
}