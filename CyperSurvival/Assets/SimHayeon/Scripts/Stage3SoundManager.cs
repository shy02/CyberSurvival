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
    public void PlayBeforeRush() { PlaySound(sounds[1], 3f); }

    public void PlayRushing(){ PlaySound(sounds[2], 4f); }

    public void PlayRushBoom() { PlaySound(sounds[3], 4f); }

    //¶³¾îÁö´Â ÆøÅº
    public void FallingBomb() { PlaySound(sounds[4], 2f); }

    public void Bomb_Boom() { PlaySound(sounds[5], 7f); }

    public void PlayArtShot() { sounds[6].GetComponent<AudioSource>().Play(); }
    public void StopArtShot() { sounds[6].GetComponent<AudioSource>().Stop(); }
    public void CircleShot() { PlaySound(sounds[7], 1f); }

    private void PlaySound(GameObject sound_prefab, float destroyTime)
    {
        GameObject sound = Instantiate(sound_prefab, transform);
        sound.GetComponent<AudioSource>().Play();
        Destroy(sound, destroyTime);
    }
}
