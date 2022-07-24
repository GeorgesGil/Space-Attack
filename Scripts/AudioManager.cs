using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioSource levelMusic, bossMusic, victoryMusic, loseMusic;
    public AudioSource [] audioSFX;
    private void Awake()
    {
        //if we dont have audio manager in scene then transfer the audio manager to next scene
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else//but if we have one in scene just destroy it to avoid have 2 audio manager in scene
        {
            Destroy(gameObject);
        }
        
    }
    // Start is called before the first frame update
    void Start()
    {
        levelMusic.Play();
    }

    //Stop all music to avoid playing musics at the same time
    void StopAllMusic()
    {
        levelMusic.Stop();
        bossMusic.Stop();
        victoryMusic.Stop();
        loseMusic.Stop();
    }

    public void LevelMusic()
    {
        StopAllMusic();
        levelMusic.Play();
    }

    public void VictoryMusic()
    {
        StopAllMusic();
        victoryMusic.Play();
    }

    public void LoseMusic()
    {
        StopAllMusic();
        loseMusic.Play();
    }

    public void BossMusic()
    {
        StopAllMusic();
        bossMusic.Play();
    }

    public void PlaySFX(int sfx)
    {
        audioSFX[sfx].Stop();
        audioSFX[sfx].Play();
    }
}
