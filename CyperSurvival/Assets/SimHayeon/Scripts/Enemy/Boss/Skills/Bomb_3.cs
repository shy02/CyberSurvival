using UnityEngine;

public class Bomb_3 : MonoBehaviour
{
    //��ġ�� ���� ī��Ʈ �ٿ� ����
    //������ ��!!!!
    //�ϰ� �����

    [SerializeField] float CountDown = 1f;//������ ������ �ð�
    [SerializeField] float DestroyTime = 2f;//������Ʈ ������� �ð�
    [SerializeField] GameObject Bomb_effect;//������ ȿ�� ������Ʈ

    private void Start()
    {
        Invoke("StartBomb",CountDown);
        Stage3SoundManager.instace.FallingBomb();
    }

    private void StartBomb()
    {
        Stage3SoundManager.instace.Bomb_Boom();
        Instantiate(Bomb_effect, transform.position, Quaternion.identity);

        //���⼭ �÷��̾� ������
        Destroy(gameObject, DestroyTime);
    }

}
