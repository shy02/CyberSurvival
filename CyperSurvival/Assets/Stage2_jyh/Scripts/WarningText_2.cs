using UnityEngine;
using UnityEngine.UI;

public class WarningText_2 : MonoBehaviour
{
    Text text;
    float delta = 0f;


    private void Awake()
    {
        text = GetComponent<Text>();
        text.color = Color.red;
    }

    void Start()
    {
        
    }

    void Update()
    {
        delta += Time.deltaTime;
        TextWarning();
    }

    private void OnEnable()
    {
        delta = 0f;
        text.text = "Warning!\nWarning!";
    }

    //깜빡거리게 
    void TextWarning()
    {
        //2초이하일때 워닝
        if (delta < 2f)
            text.text = "Warning!\nWarning!";
        //소환까지 시간알려주기
        else
            text.text = (5-delta).ToString("N0") + "초 뒤\n보스가 소환됩니다.";

        //0.5초 마다 투명하게 만들기
        if(delta % 0.5f < 0.1f)
        {
            text.color = new Color(1, 0, 0, 0);
        }
        else
        {
            text.color = new Color(1, 0, 0, 1);
        }

    }

}
