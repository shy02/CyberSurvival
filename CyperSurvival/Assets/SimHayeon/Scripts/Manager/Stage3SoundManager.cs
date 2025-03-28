using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class Stage3SoundManager : MonoBehaviour
{
    //��� : �Ҹ����� ������Ʈ�� ������ �ִ� ������Ʈ�� ������ȭ�Ѵ��� �ش� �Ҹ��� ���ö� �ش� �������� ������
    //       �ѹ��� �������� �Ҹ��� �������� ���� ���


    public static Stage3SoundManager instace;//�ܺ� � ��ũ��Ʈ�� ��������

    private void Awake()
    {
        //�̱���
        if(instace == null) { instace = this; }
        else { Destroy(gameObject); }
        
    }//�������� ���� ����

    [SerializeField] List<GameObject> sounds = new List<GameObject>();//�Ҹ��� ����ִ� ������Ʈ�� ������ȭ ���Ѽ� ���⿡ �߰�

    private void Update()
    {
        if (GameManager.Instance.nowGameOver || GameManager.Instance.nowNextStage)
        {
            LoopSoundStop();
            DestroyAllChildren();
        }
    }

    #region ����Ŭ���� Ȥ�� ���� ������ ���� ���� ����

    private void LoopSoundStop()
    {
        transform.GetChild(0).GetComponent<AudioSource>().Stop();
        sounds[0].GetComponent<AudioSource>().Stop();
        sounds[6].GetComponent<AudioSource>().Stop();
    }
    private void DestroyAllChildren()
    {
        while (transform.childCount <= 0)
        {
            Destroy(transform.GetChild(0).gameObject);
        }
    }
    #endregion

    #region �ܺο��� �����ϴ� ���� �÷��� �Լ���
    //��������
    public void PlayWalkSound() { sounds[0].GetComponent<AudioSource>().Play(); }
    public void StopWalkSound() { sounds[0].GetComponent<AudioSource>().Stop(); }

    //������
    public void PlayBeforeRush() { PlaySound(sounds[1], 3f); }

    public void PlayRushing(){ PlaySound(sounds[2], 4f); }

    public void PlayRushBoom() { PlaySound(sounds[3], 4f); }

    //�������� ��ź
    public void FallingBomb() { PlaySound(sounds[4], 2f); }

    public void Bomb_Boom() { PlaySound(sounds[5], 7f); }

    //������ ���� ��εεεε�
    public void PlayArtShot() { sounds[6].GetComponent<AudioSource>().Play(); }
    public void StopArtShot() { sounds[6].GetComponent<AudioSource>().Stop(); }
    
    //�������� ���ư��� �Ѿ� �߻� �Ҹ�
    public void CircleShot() { PlaySound(sounds[7], 1f); }

    //�׷α� �ɷ����� ������ ������
    public void electiric() { PlaySound(sounds[8], 1.3f); }

    #endregion
    private void PlaySound(GameObject sound_prefab, float destroyTime)//���带 �÷��̽�Ű�� �޼���
    {
        AudioSource audio = sound_prefab.GetComponent<AudioSource>();
        if (audio != null)
        {
            GameObject sound = Instantiate(sound_prefab, transform);
            sound.GetComponent<AudioSource>().Play();
            Destroy(sound, destroyTime);
        }
        else { Debug.LogError(sound_prefab.name + "�� �ҽ��� ������� �ʽ��ϴ�!"); }
    }
}
