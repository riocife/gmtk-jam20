using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public GameObject mainMenuScreen;
    public GameObject creditsScreen;

    private void Start()
    {
        Cursor.visible = true;
    }

    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Credits()
    {
        mainMenuScreen.SetActive(false);
        creditsScreen.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(1);
    }

    public void Menu()
    {
        SceneManager.LoadScene(0);
    }

    public void ReturnToMainMenu()
    {
        mainMenuScreen.SetActive(true);
        creditsScreen.SetActive(false);
    }
}
