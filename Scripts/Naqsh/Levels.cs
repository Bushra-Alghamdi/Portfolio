using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Levels : MonoBehaviour
{

    [SerializeField] public AudioSource _source;
    [SerializeField] public AudioClip _start, _quit, _levels;

    public void Level1()
    {
        SceneManager.LoadScene("Level-1");

    }


    public void Level2()
    {
        SceneManager.LoadScene("Level-2");

    }

    public void BackButton()
    {
        SceneManager.LoadScene("StartScreen");

    }

}
