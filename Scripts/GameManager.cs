using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    #region Delegates And Events

    public delegate void GameDelegate();
    public static event GameDelegate OnGameStarted;
    public static event GameDelegate OnGameOverConfirmed;
    public static event GameDelegate DestroyAll;

    #endregion
    
    #region GameObjects

    public GameObject startPage;
    public GameObject gameOverPage;
    public GameObject countDownPage;
    public GameObject helpPage;
    public GameObject infoPage;
    public GameObject InGameScoreText;
    public Text CoinScoreText;
    public GameObject Bird;

    #endregion

    #region Variables

    private int coinTotal = 0;
    private int score = 0;
    public static bool gameOver = true;
    private Animator BirdAnimator;

    #endregion
    
    enum PageState
    {
        None,
        Start,
        GameOver,
        CountDown
    }
    
    private void OnEnable()
    {
        CountDownText.OnCountDownFinished += OnCountDownFinished;
        BirdMove.OnPlayerDied += OnPlayerDied;
        BirdMove.OnPlayerScored += OnPlayerScored;
        BirdMove.OnCoinCollect += OnCoinCollect;
    }

    private void OnDisable()
    {
        CountDownText.OnCountDownFinished -= OnCountDownFinished;
        BirdMove.OnPlayerDied -= OnPlayerDied;
        BirdMove.OnPlayerScored -= OnPlayerScored;
        BirdMove.OnCoinCollect -= OnCoinCollect;
    }

    void OnCountDownFinished() //event came from CountDownText
    {
        SetPageState(PageState.None);
        gameOver = false;
        OnGameStarted(); //event sent to BirdMove
        score = 0;
        gameOver = false;
    }
    
    void OnPlayerDied() //event came from BirdMove
    {
        gameOver = true;
        GameObject.Find("BottomGorund").GetComponent<BackGroundMov>().enabled = false; //disabling the scripts of BottomGround
        GameObject.Find("BackGround").GetComponent<BackGroundMov>().enabled = false; //disabling the scripts of BackGround
        BirdAnimator = Bird.GetComponent<Animator>(); 
        BirdAnimator.enabled = false; //disabling the animator on the bird
        int savedScore = PlayerPrefs.GetInt("HighScore",0);
        if (score > savedScore)
        {
            PlayerPrefs.SetInt("HighScore",score);
        }
        SetPageState(PageState.GameOver);
    }

    void OnPlayerScored() //event came from BirdMove
    {
        score++;
        InGameScoreText.GetComponent<Text>().text = score.ToString();
    }

    void OnCoinCollect() //event came from BirdMove
    {
        coinTotal++;
        CoinScoreText.text = "Coins : " + coinTotal.ToString();
    }
    
    void SetPageState(PageState state)
    {
        switch (state)
        {
            case PageState.None:
                startPage.SetActive(false);
                gameOverPage.SetActive(false);
                countDownPage.SetActive(false);
                InGameScoreText.SetActive(true);
                break;
            case PageState.Start:
                startPage.SetActive(true);
                gameOverPage.SetActive(false);
                countDownPage.SetActive(false);
                InGameScoreText.SetActive(false);
                break;
            case PageState.GameOver:
                startPage.SetActive(false);
                gameOverPage.SetActive(true);
                countDownPage.SetActive(false);
                InGameScoreText.SetActive(true);
                break;
            case PageState.CountDown:
                startPage.SetActive(false);
                gameOverPage.SetActive(false);
                countDownPage.SetActive(true);
                InGameScoreText.SetActive(false);
                break;
        }
    }
    
    public void ReplayButton()
    {
        //activated when replay button is hit
        BirdAnimator.enabled = true;
        GameObject.Find("BottomGorund").GetComponent<BackGroundMov>().enabled = true;
        GameObject.Find("BackGround").GetComponent<BackGroundMov>().enabled = true;
        OnGameOverConfirmed(); //event sent to BirdMove;
        DestroyAll(); //event sent to ObjectDestroyer;
        InGameScoreText.GetComponent<Text>().text = "0";
        SetPageState(PageState.Start);
    }

    public void PlayButton()
    {
        //activates when play button is hit
        SetPageState(PageState.CountDown);
    }

    public void HelpButton()
    {
        //activates when help button is hit
        if (helpPage.activeInHierarchy)
        {
            helpPage.SetActive(false);
        }
        else
        {
            helpPage.SetActive(true);
        }
    }

    public void InfoButton()
    {
        //activates when Info button is hit
        if (infoPage.activeInHierarchy)
        {
            infoPage.SetActive(false);
        }
        else
        {
            infoPage.SetActive(true);
        }
    }
        
    public void ExitButton()
    {
        //activates when exit button is hit
        Debug.Log("Quit");
        Application.Quit();
    }
}
