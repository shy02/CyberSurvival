using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Animator MyAnimator;

    public static int DEAULT_POWER = 100;

    public float moveSpeed = 5f;
    public int hp = 1000;
    public int power = 1;
    public int defence = 0;
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
    }

    void Update()
    {
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
        SetWalkingAnimation(moveX, moveY);

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
        GameObject bulletInstance = Instantiate(MyBullets[power-1], LauncherPos.position, Quaternion.identity);
        PlayerBullet bulletScript = bulletInstance.GetComponent<PlayerBullet>();
        bulletScript.fireDirection = fireDirection;
        bulletScript.playwerPower = power;
        StartCoroutine(ShowMuzzle());
    }

    private void FireUltimate()
    {
        uValue += Time.deltaTime;
        if (uValue >= 0.1)
        {
            GameObject ultimate = Instantiate(MyUltimates[power-1], mousePosition, Quaternion.identity);
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
        //Debug.Log("CrossHair : " + CrossHair.transform.position);
    }

    private void Avoide()
    {
        StartCoroutine(RollAnimation());
    }

    public void TakeDamage(int monsterAttack)
    {
        if (!isRolling)
        {
            hp -= monsterAttack * (100 - defence);
            HitAnimation();
        }
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

    public void GainHpItem()
    {
        hp += 10;
        print($"player hp : {hp} (+10)");
    }

    public void GainPowerItem()
    {
        GameManager.Instance.AddGainedPowerItem();
        power++;
        print($"player power : {power} (+1)");
    }

    public void GainDefenceItem()
    {
        GameManager.Instance.AddGainedDefenceItem();
        defence += 10;
        print($"player defence : {defence} (+10)");
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

    IEnumerator HitAnimation()
    {
        MyAnimator.SetBool(Strings.isHit, true);

        float animationLength = MyAnimator.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(animationLength);

        MyAnimator.SetBool(Strings.isHit, false);
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

}