using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy2_Shot_3 : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    public Transform parent;

    private void Start()
    {
        StartCoroutine(shot());
    }
    IEnumerator shot()
    {
        if (!GameManager.Instance.nowNextStage)
        {
            yield return new WaitForSeconds(2f);
            Instantiate(bullet, transform.position, Quaternion.identity, parent);
            yield return new WaitForSeconds(0.5f);
            Instantiate(bullet, transform.position, Quaternion.identity, parent);
            StartCoroutine(shot());
        }
    }
}
