using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

    public void PlayGame()
    {
        SceneManager.LoadScene(sceneName: "Level1");
    }

    public void QuitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }
    public void MainMenu()
    {
        SceneManager.LoadScene(sceneName: "Menu");
    }
}
