using UnityEngine;

public class EnemySlash : MonoBehaviour
{
    [SerializeField]
    private int damage = 1;

    private void OnEnable()
    {
        Invoke("DisableObject", 0.5f);
    }

    private void DisableObject()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Player_2>().GetDamage(damage);
        }
    }
}
