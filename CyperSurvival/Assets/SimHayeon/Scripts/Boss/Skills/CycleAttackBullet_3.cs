using UnityEngine;

public class CycleAttackBullet : MonoBehaviour
{

    public float Speed = 3f;
    Vector2 vec2 = Vector2.down;

    void Update()
    {
        transform.Translate(vec2 * Speed * Time.deltaTime);
    }

    public void Move(Vector2 vec)
    {
        vec2 = vec;
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);
            //collision.GetComponent<Player>().GetDamage();
        }
    }
}
