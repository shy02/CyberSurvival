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

    //�����Ÿ��� 
    void TextWarning()
    {
        //2�������϶� ����
        if (delta < 2f)
            text.text = "Warning!\nWarning!";
        //��ȯ���� �ð��˷��ֱ�
        else
            text.text = (5-delta).ToString("N0") + "�� ��\n������ ��ȯ�˴ϴ�.";

        //0.5�� ���� �����ϰ� �����
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
