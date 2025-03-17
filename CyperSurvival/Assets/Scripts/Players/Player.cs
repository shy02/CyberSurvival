using UnityEngine;

public class Player : MonoBehaviour
{
    public Animator MyAnimator;
    public float moveSpeed = 5f;
    public float hp = 100;
    public float attack = 10;

    public GameObject WeaponBack;
    public GameObject WeaponHand;

    void Start()
    {
        MyAnimator = GetComponent<Animator>();
        WeaponBack.SetActive(true);
        WeaponHand.SetActive(false);
    }

    void Update()
    {
        MovePlayer();
        FireBullet();
    }

    private void MovePlayer()
    {
        float moveX = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        float moveY = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        Vector3 newPosition = transform.position + new Vector3(moveX, moveY, 0);
        transform.position = newPosition;
        SetWalkingAnimation(moveX, moveY);
    }

    private void FireBullet()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            WeaponBack.SetActive(false);
            WeaponHand.SetActive(true);
        }
        else
        {
            WeaponBack.SetActive(true);
            WeaponHand.SetActive(false);
        }
    }

    public void Damage(int attack)
    {
        hp -= attack;
    }

    private void SetWalkingAnimation(float moveX, float moveY)
    {
        if (moveX != 0 || moveY != 0)
        {
            MyAnimator.SetBool("isWalking", true);
        }
        else
        {
            MyAnimator.SetBool("isWalking", false);
        }
    }
}
