using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float bulletSpeed = 10.0f;
    public Vector3 fireDirection;

    void Start()
    {
        
    }

    void Update()
    {
        transform.position += fireDirection * bulletSpeed * Time.deltaTime;
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
