using UnityEngine;

public enum Effect
{
    HP,
    POWER,
    DEFENCE
}

public class Item : MonoBehaviour
{
    public Effect effect;

    void Start()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(Strings.tagPlayer))
        {
            Player player = collision.GetComponent<Player>();

            switch (effect)
            {
                case Effect.HP:
                    player.GainHpItem();
                    break;
                case Effect.POWER:
                    player.GainPowerItem();
                    break;
                case Effect.DEFENCE:
                    player.GainDefenceItem();
                    break;
            }

            Destroy(gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
