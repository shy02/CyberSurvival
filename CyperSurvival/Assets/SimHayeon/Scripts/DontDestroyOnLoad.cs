using Unity.VisualScripting;
using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{
    private static DontDestroyOnLoad instance;

    private void Awake()
    {
        if(instance == null) { instance = this; }
        else { Destroy(gameObject); }
    }
}
