using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    public GameObject pausePanel;
    bool isActive = false;


    private void Start()
    {
        Time.timeScale =1;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
    }

    public void RestartGame()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
        Time.timeScale = 1;
        
    }

    public void NextLevel()
    {
       
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Time.timeScale = 1;

    }

    public void GoBackToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void PauseResumeGame()
    {
        //print("Clicked i swear");
        if (isActive == false)
        {
            pausePanel.SetActive(true);
            Time.timeScale = 0;
            isActive = true;
        }
        else
        {
            pausePanel.SetActive(false);
            Time.timeScale = 1;
            isActive = false;
        }
    }


}
