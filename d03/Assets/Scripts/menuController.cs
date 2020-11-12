using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class menuController : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("ex01");
    }
    public void QuitGame()
    {
        if (Application.isPlaying)
            EditorApplication.ExecuteMenuItem("Edit/Play");
        else
            Application.Quit();
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("ex00");
    }
}