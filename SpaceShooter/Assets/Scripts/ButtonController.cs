using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour {
    public static bool PausedGame = false;
    public GameObject PausePanel;
    public GameObject PauseButton;

	public void PlayGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void PauseGame()
    {
        if (!PausedGame)
        {
            PausedGame = true;
            Time.timeScale = 0;
            PausePanel.gameObject.SetActive(true);
            PauseButton.gameObject.SetActive(false);
        }
        else
        {
            PausePanel.gameObject.SetActive(false);
            PauseButton.gameObject.SetActive(true);
            PausedGame = false;
            Time.timeScale = 1;
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
