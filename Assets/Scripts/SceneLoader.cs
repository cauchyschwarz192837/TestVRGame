using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadScene() {
        SceneManager.LoadScene("Scenes/Battleground");
    }

    /*
    public void LoadSceneInBuild() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    */
}
