using UnityEngine;
using UnityEngine.UI;

public class stage2_Spawner : MonoBehaviour
{
    public Image hpBar;
    int MaxHp = 100;
    int CurrentHp = 100;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {
        CurrentHp -= damage;
        Debug.Log("HP : " + CurrentHp);
        hpBar.fillAmount = (float)CurrentHp / MaxHp;
        if (CurrentHp <= 0)
        {
            Destroy(gameObject);
        }
    }
}
