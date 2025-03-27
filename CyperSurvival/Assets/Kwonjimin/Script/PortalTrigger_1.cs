using UnityEngine;

public class PortalTrigger_1 : MonoBehaviour
{
    public AudioClip portalSound; // ��Ż ȿ����
    public float triggerDistance = 5f; // ȿ������ �鸮�� �ִ� �Ÿ�
    private Transform player;
    private AudioSource audioSource;

    void Start()
    {
        player = GameObject.FindWithTag("Player")?.transform;

        // AudioSource ����
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = portalSound;
        audioSource.spatialBlend = 1f; // 3D ȿ�� ����
        audioSource.rolloffMode = AudioRolloffMode.Logarithmic; // �Ÿ� ���� ����� Logarithmic�� ���� (�Ҹ��� ���������� �۾���)

        audioSource.minDistance = triggerDistance / 2f; // �Ҹ��� 100% �鸮�� ����
        audioSource.maxDistance = triggerDistance * 2f; // �Ҹ��� ���� �پ��� �ִ� �Ÿ� (�� �� �ָ� ����)
        audioSource.playOnAwake = false;

        // �⺻ ������ �� Ű��� (0.0f ~ 1.0f)
        audioSource.volume = 1.0f; // �ִ� ���� ����
    }

    void Update()
    {
        if (player != null)
        {
            float distance = Vector2.Distance(transform.position, player.position);

            // ��Ż�� ��������� �Ҹ��� ����ǵ���
            if (distance <= audioSource.minDistance && !audioSource.isPlaying)
            {
                audioSource.Play();
            }
            // �ʹ� �־����� �Ҹ��� ���ߵ���
            else if (distance > audioSource.maxDistance && audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }
    }
}
