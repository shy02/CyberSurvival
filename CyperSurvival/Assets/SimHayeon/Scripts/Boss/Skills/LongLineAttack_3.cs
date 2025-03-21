using System.Collections;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class LongLineAttack : MonoBehaviour
{
    Vector3 center;
    float StartTime;
    //float Upsize = 0;
    void Start()
    {
        center = transform.position;
        StartTime = Time.time;
    }

    /*void Update()
    {
        pos.x = (Mathf.Cos((Time.time - StartTime) * 360 * Mathf.Deg2Rad)  - Upsize);
        pos.y = (Mathf.Sin((Time.time - StartTime) * 360 * Mathf.Deg2Rad));

        transform.position = pos;
        Upsize+= 0.01f;
    }*/

    public void StartAttack(float Mx, float My, float UX, float UY, bool flip, Transform boss)
    {
        center = boss.position;
        StartCoroutine(Attack8(Mx,My,UX,UY,flip));
    }

    IEnumerator Attack8(float Minusx, float Minusy, float UpsizeX, float UpsizeY, bool flip)
    {
        float Upsize = 0;
        while (true)
        {
            float x = (UpsizeX != 0 ) ? Minusx * (Mathf.Cos((Time.time - StartTime) * 360 * Mathf.Deg2Rad) - Upsize) : Minusx * (Mathf.Cos((Time.time - StartTime) * 360 * Mathf.Deg2Rad));
            float y = (UpsizeY != 0 ) ? Minusy * (Mathf.Sin((Time.time - StartTime) * 360 * Mathf.Deg2Rad) - Upsize) : Minusy * (Mathf.Sin((Time.time - StartTime) * 360 * Mathf.Deg2Rad));

            Vector3 pos = center + new Vector3(x, y, 0);
            transform.position = (flip) ? -pos : pos;
            Upsize += 0.07f;
            yield return new WaitForSeconds(0.01f);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
