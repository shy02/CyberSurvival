using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class Stage3SoundManager : MonoBehaviour
{
    public static Stage3SoundManager instace;

    private void Awake()
    {
        if(instace == null) { instace = this; }
        else { Destroy(gameObject); }
    }

    [SerializeField] List<GameObject> sounds = new List<GameObject>();

    //°ÉÀ½°ü·Ã
    public void PlayWalkSound() { sounds[0].GetComponent<AudioSource>().Play(); }
    public void StopWalkSound() { sounds[0].GetComponent<AudioSource>().Stop(); }

    //µ¹Áø±â
    public void PlayBeforeRush()
    {
        GameObject sound = Instantiate(sounds[1], transform);
        sound.GetComponent<AudioSource>().Play();
        Destroy(sound, 5f);
    }

    public void PlayRushing()
    {
        GameObject sound = Instantiate(sounds[2], transform);
        sound.GetComponent<AudioSource>().Play();
        Destroy(sound, 4f);
    } 

    public void PlayRushBoom()
    {
        GameObject sound = Instantiate(sounds[3], transform);
        sound.GetComponent<AudioSource>().Play();
        Destroy(sound, 4f);

    }

    //¶³¾îÁö´Â ÆøÅº
    public void FallingBomb()
    {
        GameObject sound = Instantiate(sounds[4], transform);
        sound.GetComponent<AudioSource>().Play();
        Destroy(sound, 2f);

    }

    public void Bomb_Boom()
    {
        GameObject sound = Instantiate(sounds[5], transform);
        sound.GetComponent<AudioSource>().Play();
        Destroy(sound, 7f);
    }
}
