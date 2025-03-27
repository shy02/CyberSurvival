using UnityEngine;

public class OpenOption : MonoBehaviour
{
    [SerializeField] GameObject Option;
    bool option_open = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            option_open = !option_open;
            Option.SetActive(option_open);
        }
    }
}
