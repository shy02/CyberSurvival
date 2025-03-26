using UnityEngine;

public class EffectDelete_2 : MonoBehaviour
{
    Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        Destroy(gameObject, anim.GetCurrentAnimatorClipInfo(0).Length);
    }

    void Update()
    {
        
    }
}
