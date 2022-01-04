using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class StartScreen : MonoBehaviour
{

    [SerializeField] public AudioSource _source;
    [SerializeField] public AudioClip _start, _quit, _levels;

    
    public void PlayGame()
    {
        _source.PlayOneShot(_start);
        //Load the next level in the queue

        SceneManager.LoadScene("Level-1");
        // Update is called once per frame


    }
    public void Levels()
    {
        _source.PlayOneShot(_levels);
        SceneManager.LoadScene("Levels");
    }

    public void QuitGame()
    {
        _source.PlayOneShot(_quit);
        Application.Quit();
    }

    

}
