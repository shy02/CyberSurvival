using Unity.Cinemachine;
using UnityEngine;

public class FollowCamera3 : MonoBehaviour
{
    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        GetComponent<CinemachineCamera>().Follow = player.transform;
    }

}
