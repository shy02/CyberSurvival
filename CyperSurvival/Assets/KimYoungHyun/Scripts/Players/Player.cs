using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Animator MyAnimator;

    public float moveSpeed = 5f;
    private bool isLeft = false;
    private bool isRolling = false;

    public GameObject Muzzle;
    public GameObject WeaponHand;
    public GameObject CrossHair;
    public GameObject[] MyBullets;
    public GameObject[] MyUltimates;
    public Transform LauncherPos;
    public Vector3 fireDirection;
    public Vector3 mousePosition;
    public float uValue = 0;

    public GameObject AfterImagePrefab;
    public float afterImageInterval = 0.1f;
    private float afterImageTimer;

    void Start()
    {
        MyAnimator = GetComponent<Animator>();
        Muzzle.SetActive(false);

        InvokeRepeating("TakeDamageTest", 0f, 1f); // Test
    }

    void Update()
    {
        if (!GameManager.Instance.IsGameRunning)
        {
            return;
        }

        HandleUserInput();
        HandleMousePosition();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

    }

    private void HandleUserInput()
    {
        float moveX = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        float moveY = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        Vector3 newPosition = transform.position + new Vector3(moveX, moveY, 0);
        transform.position = newPosition;
        WalkingAnimation(moveX, moveY);

        if (moveX != 0 || moveY != 0)
        {
            afterImageTimer -= Time.deltaTime;
            if (afterImageTimer <= 0)
            {
                CreateAfterImage();
                afterImageTimer = afterImageInterval;
            }
        }

        if (Input.GetMouseButtonDown(0) && !isRolling)
        {
            FireBullet();
        }
        else if (Input.GetMouseButton(1) && !isRolling && GameManager.Instance.isBossGroggy)
        {
            FireUltimate();
        }
        else
        {
            uValue -= Time.deltaTime;
            uValue = (uValue <= 0.1f) ? 0.1f : uValue;
        }

        if (Input.GetKeyDown(KeyCode.LeftControl) && !isRolling)
        {
            Avoide();
        }
    }

    private void FireBullet()
    {
        GameObject bulletInstance = Instantiate(MyBullets[GameManager.Instance.PlayerPower], LauncherPos.position, Quaternion.identity);
        PlayerBullet bulletScript = bulletInstance.GetComponent<PlayerBullet>();
        bulletScript.fireDirection = fireDirection;
        bulletScript.playwerPower = GameManager.Instance.PlayerPower;
        StartCoroutine(ShowMuzzle());
    }

    private void FireUltimate()
    {
        uValue += Time.deltaTime;
        if (uValue >= 0.1)
        {
            GameObject ultimate = Instantiate(MyUltimates[GameManager.Instance.PlayerPower], mousePosition, Quaternion.identity);
            Destroy(ultimate, 0.15f);
            uValue = 0;
        }
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

    private void Avoide()
    {
        StartCoroutine(RollAnimation());
    }

    //public void TakeDamage(int monsterAttack)
    //{
    //    if (!isRolling)
    //    {
    //        int damage = monsterAttack * (100 - defence) / 100;
    //        hp -= damage;
    //        HitAnimation();
    //        print($"Player hp : {hp} (-{damage})");
    //        GameManager.Instance.SetHp(hp);
    //    }
    //}

    public void TakeDamage(int damage)
    {
        int finalDamage = damage * (100 - GameManager.Instance.PlayerDefence) / 100;
        GameManager.Instance.SetHp(-finalDamage);
    }

    public void GainHpItem()
    {
        if (GameManager.Instance.PlayerHp >= GameManager.MAX_HP)
        {
            return;
        }
        GameManager.Instance.SetHp(100);
    }

    public void GainPowerItem()
    {
        GameManager.Instance.AddGainedPowerItem();
    }

    public void GainDefenceItem()
    {
        GameManager.Instance.AddGainedDefenceItem();
    }

    private void WalkingAnimation(float moveX, float moveY)
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

    private void HitAnimation()
    {
        MyAnimator.SetTrigger(Strings.Hit);
    }

    IEnumerator ShowMuzzle()
    {
        Muzzle.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        Muzzle.SetActive(false);
    }

    private void CreateAfterImage()
    {
        GameObject afterimage = Instantiate(AfterImagePrefab);
        afterimage.GetComponent<AfterImage>().Initialize(
        GetComponent<SpriteRenderer>().sprite, transform.position);

        Vector3 scale = afterimage.transform.localScale;
        scale.x = isLeft ? -Mathf.Abs(scale.x) : Mathf.Abs(scale.x);
        afterimage.transform.localScale = scale;
    }

    private void TakeDamageTest() // Test
    {
        if (!isRolling)
        {
            int damage = 50 * (100 - GameManager.Instance.PlayerDefence) / 100;
            GameManager.Instance.SetHp(-damage);
            HitAnimation();

            if (GameManager.Instance.PlayerHp <= 0)
            {
                MyAnimator.SetTrigger(Strings.Dead);
                WeaponHand.SetActive(false);
                GameManager.Instance.FinishGame();
            }
        }
    }
}