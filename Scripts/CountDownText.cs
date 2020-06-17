using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CountDownText : MonoBehaviour
{
    public delegate void CountDownFinished();
    public static event CountDownFinished OnCountDownFinished;
    
    private Text countDown;

    private void OnEnable()
    {
        countDown = GetComponent<Text>();
        countDown.text = "3";
        StartCoroutine("CountDown");
    }

    IEnumerator CountDown()
    {
        int count = 3;
        for (int i = 0; i < count; i++)
        {
            countDown.text = (count - i).ToString();
            yield return new WaitForSeconds(1);
        }

        OnCountDownFinished(); //event sent to GameManager
    }
}
