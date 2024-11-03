using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ProgressScene : MonoBehaviour
{
    public Sprite[] backgrounds = new Sprite[7];
    private int backgroundNumber = 0;

    public SpriteRenderer background;

    public AudioSource audioSource;
    private int musicNumber = 0;
    public AudioClip[] songs = new AudioClip[10];


    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.Space))
        {
            NextScene();
        }
        if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            ChangeBackground();
        }
        if(Input.GetKeyUp(KeyCode.M))
        {
            ChangeMusic();
        }
    }

    public void NextScene()
    {
        SceneManager.LoadScene(1);
    }

    public void ChangeBackground()
    {
        backgroundNumber++;
        background.sprite = backgrounds[backgroundNumber];
        if(backgroundNumber == 7)
        {
            backgroundNumber = 0;
        }
    }
    public void ChangeMusic()
    {
        musicNumber++;
        audioSource.clip = songs[musicNumber];
        audioSource.Play();
        if (musicNumber == 7)
        {
            musicNumber = 0;
        }
    }
}
