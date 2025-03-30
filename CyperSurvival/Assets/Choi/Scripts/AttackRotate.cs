using UnityEngine;

public class RotateTowardsPlayer : MonoBehaviour
{
    public float orbitRadius = 2f;
    private Transform player; 
    private Transform parentTransform;
    private void OnEnable()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        parentTransform = transform.parent;

        RotatePosition();
        FaceParent();
    }

    private void RotatePosition()
    {
        Vector3 directionToPlayer = (player.position - parentTransform.position).normalized;

        Vector3 newPosition = parentTransform.position + directionToPlayer * orbitRadius;

        transform.position = newPosition;
    }
    private void FaceParent()
    {
        Vector3 directionToParent = parentTransform.position - transform.position;

        Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, -directionToParent);
        transform.rotation = targetRotation * Quaternion.Euler(0, 0, -90); ;
    }
}
