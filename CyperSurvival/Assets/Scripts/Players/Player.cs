using UnityEngine;

public class Player : MonoBehaviour
{
    public Animator MyAnimator;
    public float moveSpeed = 5f;
    public int hp = 100;
    public int power = 10;
    private bool isLeft = false;

    public GameObject WeaponHand;
    public GameObject CrossHair;
    public GameObject MyBullet;
    public Transform LauncherPos;
    public Vector3 fireDirection;
    public Vector3 mousePosition;

    void Start()
    {
        MyAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        HandleKeyboard();
        HandleMousePosition();
        FireBullet();
    }

    private void HandleKeyboard()
    {
        float moveX = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        float moveY = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        Vector3 newPosition = transform.position + new Vector3(moveX, moveY, 0);
        transform.position = newPosition;
        SetWalkingAnimation(moveX, moveY);

        if (moveX < 0)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if (moveX > 0)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }

    private void FireBullet()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CreateBullet();
        }
    }

    private void CreateBullet()
    {
        GameObject bulletInstance = Instantiate(MyBullet, LauncherPos.position, Quaternion.identity);
        PlayerBullet bulletScript = bulletInstance.GetComponent<PlayerBullet>();
        if (bulletScript != null)
        {
            bulletScript.fireDirection = fireDirection;
        }
    }

    public void Damage(int attack)
    {
        hp -= attack;
    }

    private void HandleMousePosition()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;
        isLeft = mousePosition.x < transform.position.x;
        if (isLeft)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }

        fireDirection = (mousePosition - WeaponHand.transform.position).normalized;
        float angle = Mathf.Atan2(fireDirection.y, fireDirection.x) * Mathf.Rad2Deg;
        if (isLeft)
        {
            WeaponHand.transform.rotation = Quaternion.Euler(0f, 0f, angle + 180f);
        }
        else
        {
            WeaponHand.transform.rotation = Quaternion.Euler(0f, 0f, angle);
        }

        CrossHair.transform.position = mousePosition;
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