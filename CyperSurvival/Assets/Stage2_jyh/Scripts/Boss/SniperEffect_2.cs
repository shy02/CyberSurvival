using UnityEngine;

public class SniperEffect_2 : MonoBehaviour
{
    [SerializeField] private int damage;
    Player player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player = collision.gameObject.GetComponent<Player>();

            if(player.isRolling == false)
            {
                player.TakeDamage(damage);
            }
        }
    }
}
