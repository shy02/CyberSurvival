using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Animator MyAnimator;
    public float moveSpeed = 5f;
    public int hp = 100;
    public int power = 10;
    private bool isLeft = false;
    private bool isRolling = false;

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
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
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

        if (Input.GetMouseButtonDown(0) && !isRolling)
        {
            FireBullet();
        }

        if (Input.GetKeyDown(KeyCode.LeftControl) && !isRolling)
        {
            Avoide();
        }
    }

    private void FireBullet()
    {
        GameObject bulletInstance = Instantiate(MyBullet, LauncherPos.position, Quaternion.identity);
        PlayerBullet bulletScript = bulletInstance.GetComponent<PlayerBullet>();
        if (bulletScript != null)
        {
            bulletScript.fireDirection = fireDirection;
        }
    }

    private void Avoide()
    {
        StartCoroutine(RollAnimation());
    }

    private void HandleMousePosition()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;
        isLeft = mousePosition.x < transform.position.x;

        // 플레이어의 방향 업데이트
        if (isLeft)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }

        // 마우스 방향 계산
        fireDirection = (mousePosition - WeaponHand.transform.position).normalized;
        float angle = Mathf.Atan2(fireDirection.y, fireDirection.x) * Mathf.Rad2Deg;

        // WeaponHand 회전 업데이트
        if (isLeft)
        {
            WeaponHand.transform.rotation = Quaternion.Euler(0f, 0f, angle + 180f);
        }
        else
        {
            WeaponHand.transform.rotation = Quaternion.Euler(0f, 0f, angle);
        }

        // LauncherPos 위치 보정
        if (isLeft)
        {
            // 플레이어가 뒤집혔을 때 LauncherPos의 위치를 보정
            LauncherPos.localPosition = new Vector3(-Mathf.Abs(LauncherPos.localPosition.x), LauncherPos.localPosition.y, LauncherPos.localPosition.z);
        }
        else
        {
            // 플레이어가 원래 방향일 때 LauncherPos의 위치를 보정
            LauncherPos.localPosition = new Vector3(Mathf.Abs(LauncherPos.localPosition.x), LauncherPos.localPosition.y, LauncherPos.localPosition.z);
        }

        // CrossHair 위치 업데이트
        CrossHair.transform.position = mousePosition;
    }

    public void Damage(int attack)
    {
        hp -= attack;
    }

    private void SetWalkingAnimation(float moveX, float moveY)
    {
        if (moveX != 0 || moveY != 0)
        {
            MyAnimator.SetBool(Strings.isWalking, true);
        }
        else
        {
            MyAnimator.SetBool(Strings.isWalking, false);
        }
    }

    IEnumerator RollAnimation()
    {
        isRolling = true;
        MyAnimator.SetBool(Strings.isRolling, true);
        WeaponHand.SetActive(false);

        float animationLength = MyAnimator.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(animationLength);

        isRolling = false;
        MyAnimator.SetBool(Strings.isRolling, false);
        WeaponHand.SetActive(true);
    }

}