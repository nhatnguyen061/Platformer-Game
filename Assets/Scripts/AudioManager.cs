using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioSource[] soundEffect;

    public AudioSource bgm, levelEndMusic;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlaySFX(int soundToPlay)
    {
        soundEffect[soundToPlay].Stop();

        soundEffect[soundToPlay].Play();
    }
    public void PlayLevelVictory()
    {
        bgm.Stop();
        levelEndMusic.Play();
    }
}
