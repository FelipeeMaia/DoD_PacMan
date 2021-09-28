using UnityEngine;
using UnityEngine.UI;

// Manager responsible for the score and to show it in the UI
public class ScoreManager : MonoBehaviour
{
    #region SINGLETON PATTERN

    private static ScoreManager _instance;

    public static ScoreManager instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<ScoreManager>();
                if (_instance == null)
                {
                    _instance = new GameObject().AddComponent<ScoreManager>();
                }
            }

            return _instance;
        }
    }

    #endregion

    private Text scoreTxt, highscoreTxt;
    public int score, highscore;

    private void Awake()
    {
        highscoreTxt = GameObject.Find("Text High Score Value").GetComponent<Text>();
        scoreTxt = GameObject.Find("Text Score Value").GetComponent<Text>();
    }

    void Start()
    {
        ResetState();
    }

    public void ResetState()
    {
        score = 0;
        scoreTxt.text = "" + score;

        highscore = PlayerPrefs.GetInt("Highscore", 0);
        highscoreTxt.text = "" + highscore;

        SaveScore();
    }

    public void OnScored(int value)
    {
        score += value;
        scoreTxt.text = "" + score;

        if (score > highscore)
        {
            highscore += value;
            highscoreTxt.text = "" + highscore;
        }
    }

    public void SaveScore()
    {
        if(score >= highscore)
        {
            PlayerPrefs.SetInt("Highscore", score);
        }
    }
}
