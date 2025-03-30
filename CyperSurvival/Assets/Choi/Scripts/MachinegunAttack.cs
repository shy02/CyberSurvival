using System.Collections;
using UnityEngine;

public class MachineGunAttack : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float fireDuration = 6f;
    public float fireRate = 0.1f;

    private bool isFiring = false;

    public void StartFiring()
    {
        if (!isFiring)
        {
            StartCoroutine(FireMachineGun());
        }
    }

    private IEnumerator FireMachineGun()
    {
        isFiring = true;
        float elapsedTime = 0f;

        while (elapsedTime < fireDuration)
        {
            FireBullet();
            elapsedTime += fireRate;
            yield return new WaitForSeconds(fireRate);
        }

        isFiring = false;
    }

    private void FireBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
    }
}
