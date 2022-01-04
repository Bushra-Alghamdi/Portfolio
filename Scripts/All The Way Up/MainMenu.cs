using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public SoundManager soundManager;
    public GameObject CreditsPanel, howToPlayPanel;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
    public void PlayeGame()
    {
        FindObjectOfType<SoundManager>().PlaySFX("PlayButton");

        SceneManager.LoadScene("Raghad");
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Time.timeScale = 1;
        //AudioClip.PCMReaderCallback(play);
    }

    public void QuitGame()
    {
        FindObjectOfType<SoundManager>().PlaySFX("QuitButton");

        Application.Quit();
    }

    public void Credits()
    {
        FindObjectOfType<SoundManager>().PlaySFX("CreditButton");

        SceneManager.LoadScene("Credits");
        //CreditsPanel.SetActive(true);
    }

    public void CloseCredit()
    {
        //CreditsPanel.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);

    }
    public void HowToPlay()
    {
        FindObjectOfType<SoundManager>().PlaySFX("CreditButton");

        howToPlayPanel.SetActive(true);
    }

    public void CloseHowToplay()
    {
        howToPlayPanel.SetActive(false);

    }
    public void GoBackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
