using UnityEngine;

public class RushDirObject : MonoBehaviour
{
    [SerializeField] float lineSize = 5f;
    public bool Cango = true;
    public bool startdraw = false;

    public void StartDrawRushArea()
    {
        Cango = true;
        startdraw = true;

    }
    void Update()
    {
        if (startdraw)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, lineSize, LayerMask.GetMask("Wall"));
            Debug.DrawRay(transform.position, transform.right * lineSize, Color.red);
            if (hit.collider != null)
            {
                Cango = false;
            }
        }
    }
}
