using UnityEngine;

public class Player : MonoBehaviour
{
    float x;
    float y;
    Vector3 moveDir = Vector3.zero;
    public float moveSpeed = 3;
    int HP = 100;

     void Start()
    {
        
    }

    void Update()
    {
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");

        Vector3 vec = new Vector3(x, y, 0);
        moveDir = vec.normalized;

        transform.Translate(moveDir * moveSpeed * Time.deltaTime);

    }

    public void GetDamage(int damage)
    {
        HP -= damage;
        Debug.Log("HP : " + HP);
    }

}
