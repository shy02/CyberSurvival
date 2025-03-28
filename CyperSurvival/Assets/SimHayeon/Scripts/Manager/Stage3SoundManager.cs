using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class Stage3SoundManager : MonoBehaviour
{
    //방식 : 소리관련 컴포넌트를 가지고 있는 오브젝트를 프리팹화한다음 해당 소리가 나올때 해당 프리팹을 생성함
    //       한번에 여러개의 소리를 내기위해 만든 방식


    public static Stage3SoundManager instace;//외부 어떤 스크립트든 참조가능

    private void Awake()
    {
        //싱글톤
        if(instace == null) { instace = this; }
        else { Destroy(gameObject); }
        
    }//시작하자 마자 실행

    [SerializeField] List<GameObject> sounds = new List<GameObject>();//소리가 들어있는 오브젝트를 프리팹화 시켜서 여기에 추가

    private void Update()
    {
        if (GameManager.Instance.nowGameOver || GameManager.Instance.nowNextStage)
        {
            LoopSoundStop();
            DestroyAllChildren();
        }
    }

    #region 게임클리어 혹은 게임 오버시 사운드 삭제 관련

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

    #region 외부에서 참조하는 사운드 플레이 함수들
    //걸음관련
    public void PlayWalkSound() { sounds[0].GetComponent<AudioSource>().Play(); }
    public void StopWalkSound() { sounds[0].GetComponent<AudioSource>().Stop(); }

    //돌진기
    public void PlayBeforeRush() { PlaySound(sounds[1], 3f); }

    public void PlayRushing(){ PlaySound(sounds[2], 4f); }

    public void PlayRushBoom() { PlaySound(sounds[3], 4f); }

    //떨어지는 폭탄
    public void FallingBomb() { PlaySound(sounds[4], 2f); }

    public void Bomb_Boom() { PlaySound(sounds[5], 7f); }

    //지렁이 패턴 우두두두두두
    public void PlayArtShot() { sounds[6].GetComponent<AudioSource>().Play(); }
    public void StopArtShot() { sounds[6].GetComponent<AudioSource>().Stop(); }
    
    //원형으로 날아가는 총알 발사 소리
    public void CircleShot() { PlaySound(sounds[7], 1f); }

    //그로기 걸렸을때 나오는 지지직
    public void electiric() { PlaySound(sounds[8], 1.3f); }

    #endregion
    private void PlaySound(GameObject sound_prefab, float destroyTime)//사운드를 플레이시키는 메서드
    {
        AudioSource audio = sound_prefab.GetComponent<AudioSource>();
        if (audio != null)
        {
            GameObject sound = Instantiate(sound_prefab, transform);
            sound.GetComponent<AudioSource>().Play();
            Destroy(sound, destroyTime);
        }
        else { Debug.LogError(sound_prefab.name + "에 소스가 들어있지 않습니다!"); }
    }
}
