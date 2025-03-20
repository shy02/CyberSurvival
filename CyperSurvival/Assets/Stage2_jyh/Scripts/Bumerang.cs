using UnityEngine;

public class Bumerang : MonoBehaviour
{
    public float speed = 5f;
    public float rotationSpeed = 200f;
    private Vector3 initialPosition;
    private bool returning = false;

    void Start()
    {
        initialPosition = transform.position;
    }

    void Update()
    {
        if (!returning)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, initialPosition) > 10f)
            {
                returning = true;
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, initialPosition, speed * Time.deltaTime);
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, initialPosition) < 0.1f)
            {
                Destroy(gameObject);
            }
        }
    }
}
