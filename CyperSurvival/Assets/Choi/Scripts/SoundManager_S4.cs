using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager_S4 : MonoBehaviour
{
    public static SoundManager_S4 instace;

    private void Awake()
    {
        if (instace == null) { instace = this; }
        else { Destroy(gameObject); }
    }

    [SerializeField] List<GameObject> sounds = new List<GameObject>();

    private void Update()
    {
        if (GameManager.Instance.nowGameOver || GameManager.Instance.nowNextStage)
        {
            transform.GetChild(0).GetComponent<AudioSource>().Stop();
            sounds[0].GetComponent<AudioSource>().Stop();
            sounds[3].GetComponent<AudioSource>().Stop();
            while (transform.childCount <= 0)
            {
                Destroy(transform.GetChild(0).gameObject);
            }
        }
    }

    public void Dash() { PlaySound(sounds[0], 1.1f); }

    public void land() { PlaySound(sounds[1], 1.3f); }

    public void fire() { PlaySound(sounds[2], 1f); }

    private void PlaySound(GameObject sound_prefab, float destroyTime)
    {
        GameObject sound = Instantiate(sound_prefab, transform);
        sound.GetComponent<AudioSource>().Play();
        Destroy(sound, destroyTime);
    }
}
