using System.Collections;
using UnityEngine;

public class MachineGunAttack : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float fireDuration = 6f;
    public float fireRate = 0.1f;

    private bool isFiring = false;
    private Animator animator;
    [SerializeField] Transform firePosition;
    private Rigidbody2D rb;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void StartFiring()
    {
        if (!isFiring)
        {
            StartCoroutine(FireMachineGun());
        }
    }

    private IEnumerator FireMachineGun()
    {
        animator.SetBool("isMachineGun", true);
        rb.bodyType = RigidbodyType2D.Kinematic;
        isFiring = true;
        float elapsedTime = 0f;

        while (elapsedTime < fireDuration)
        {
            FireBullet();
            elapsedTime += fireRate;
            yield return new WaitForSeconds(fireRate);
        }

        isFiring = false;
        animator.SetBool("isMachineGun", false);

        rb.bodyType = RigidbodyType2D.Dynamic;
    }

    private void FireBullet()
    {
        SoundManager_S4.instace.fire();
        GameObject bullet = Instantiate(bulletPrefab, firePosition.position, Quaternion.identity);
    }
}
