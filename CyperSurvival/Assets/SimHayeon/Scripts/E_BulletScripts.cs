using UnityEditor.Search;
using UnityEngine;

public class E_BulletScripts : MonoBehaviour
{
    public Transform target = null;

    [SerializeField] float Speed = 5f;

    Vector3 dir = default;
    void Start()
    {
       //target = GameObject.FindGameObjectWithTag("Player").transform;

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
