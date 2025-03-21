using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy2_Shot_3 : MonoBehaviour
{
    [SerializeField] GameObject bullet;

    private void Start()
    {
        StartCoroutine(shot());
    }
    IEnumerator shot()
    {
        yield return new WaitForSeconds(2f);
        Instantiate(bullet, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0.5f);
        Instantiate(bullet, transform.position, Quaternion.identity);
        StartCoroutine(shot());
    }
}
