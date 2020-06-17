using UnityEngine;
using UnityEngine.UI;

public class HighScore : MonoBehaviour
{
    private Text highScoreText;

    private void OnEnable()
    {
        highScoreText = GetComponent<Text>();
        highScoreText.text = "HIGHSCORE : " + PlayerPrefs.GetInt("HighScore", 0);
    }
}
