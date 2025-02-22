using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadSceneByName(string _name) {
        SceneManager.LoadScene(_name);
    }

    public void LoadNextScene() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ExitGame() {
        Application.Quit();
    }
    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            Application.Quit();
            Debug.Log("Quiteando");

        }
    }
   

}
