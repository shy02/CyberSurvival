using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NextSceneAnimation : MonoBehaviour
{
    public TextMeshProUGUI nextText;
    public void StartAnime()
    {
        transform.GetComponent<AudioSource>().Play();
        transform.GetChild(0).GetComponent<Animator>().SetBool("start", true);
    }

    public void ApearText()
    {
        transform.GetChild(0).GetComponent<Animator>().SetBool("start", true);
        StartCoroutine(nextTimer());
    }

    IEnumerator nextTimer()
    {
        yield return new WaitForSeconds(1);
        int countTime = 5;
        nextText.gameObject.SetActive(true);
        while (countTime > 0)
        {
            nextText.text = "Next After " + countTime + "s";
            countTime--;
            yield return new WaitForSeconds(1);
        }
        GameManager.Instance.nowNextStage = false;
        nextText.gameObject.SetActive(false);

        ScenManager.instance.GoNextStage();
    }
}
