using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy2_Shot : MonoBehaviour
{
    [SerializeField] GameObject bullet;

    private void Start()
    {
        StartCoroutine(shot());
    }
    IEnumerator shot()
    {
        yield return new WaitForSeconds(2f);
        Instantiate(bullet, transform.position, Quaternion.identity).GetComponent<E_BulletScripts>().target = GetComponent<EnemyMovement>().player;
        yield return new WaitForSeconds(0.5f);
        Instantiate(bullet, transform.position, Quaternion.identity).GetComponent<E_BulletScripts>().target = GetComponent<EnemyMovement>().player;
        StartCoroutine(shot());
    }
}
