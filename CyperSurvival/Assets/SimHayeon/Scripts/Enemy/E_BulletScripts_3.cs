using UnityEditor.Search;
using UnityEngine;

public class E_BulletScripts_3 : MonoBehaviour
{
    Transform target = null;

    [SerializeField] float Speed = 5f;

    Vector3 dir = default;
    void Start()
    {
        target = StageManager_3.instance.player;
        dir = target.position - transform.position;
    }
    private void Update()
    {
        transform.Translate(dir.normalized * Speed * Time.deltaTime);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
