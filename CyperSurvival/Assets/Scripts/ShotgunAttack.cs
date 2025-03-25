using UnityEngine;

public class ShotgunAttack : MonoBehaviour
{
    public GameObject bulletPrefab;
    public int numberOfBullets = 9;
    public float spreadAngle = 20f;  

    private Transform player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    public void FireShotgun()
    {
        if (player == null) return;

        float angleStep = spreadAngle / (numberOfBullets - 1);
        float startAngle = -spreadAngle / 2f;

        
        for (int i = 0; i < numberOfBullets; i++)
        {
            float currentAngle = startAngle + i * angleStep;
            Vector2 bulletDirection = GetBulletDirection(currentAngle);
            ShootBullet(bulletDirection);
        }
    }

    private Vector2 GetBulletDirection(float angle)
    {
        
        float angleInRadians = angle * Mathf.Deg2Rad;
        float x = Mathf.Cos(angleInRadians);
        float y = Mathf.Sin(angleInRadians);
        return new Vector2(x, y).normalized; 
    }

    private void ShootBullet(Vector2 direction)
    {
        
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody2D>().linearVelocity = direction * bullet.GetComponent<EnemyBullet>().speed;
    }
}
