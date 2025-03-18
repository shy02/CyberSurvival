using System.Collections;
using UnityEngine;

public class Boss4Attack : MonoBehaviour
{
    // ����1. 8�� ������ ��ä������� �����ð����� �߻�
    // ����2. �÷��̾ �ָ� �ִٸ� �÷��̾�� �Ÿ� + 1 ��ŭ ����, ��ο� �÷��̾ �ִٸ� ������
    // ����3. O�� ���� ������ �߻� ��ó���� �־��� ���� �ӵ��� ������ > ���ϱ� �����
    // ����4. �÷��̾��� ��ġ�� ���� ������ ����� ���� �ð����� �ұ���� ����

    //8�� ��� ����°� �Ѿ� �ǵ�� ��ä��� ����

    [SerializeField] GameObject EightBullet1;//8�ڸ��׸��鼭 ���ư� �Ѿ��� �ϳ�
    [SerializeField] GameObject EightBullet2;//8�ڸ��׸��鼭 ���ư� �Ѿ��� �ϳ�

    Transform Target;
    Transform EightBulletLuncher;


    private void Start()
    {
        EightBulletLuncher = transform.GetChild(0).GetChild(0);//���� 1 ��ó
        StartCoroutine(EightBulletPattern());
    }


    IEnumerator EightBulletPattern()
    {
        Target = GetComponent<EnemyMovement>().player;
        Vector3 dir = Target.position - transform.position;
        //�ϴ� E �ڸ�� ���� ������ �׽�Ʈ
        Instantiate(EightBullet1, EightBulletLuncher.position, Quaternion.LookRotation(dir));
        yield return new WaitForSeconds(1f);//�Ѿ� �߻� ����;
    }


}
