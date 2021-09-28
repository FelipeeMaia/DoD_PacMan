using System.Collections;
using UnityEngine;

//Manager responsible for the win and lose conditions of the game
public class GameManager : MonoBehaviour
{

    #region SINGLETON PATTERN

    private static GameManager _instance;

    public static GameManager instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();
                if (_instance == null)
                {
                    _instance = new GameObject().AddComponent<GameManager>();
                }
            }

            return _instance;
        }
    }

    #endregion

    // Some events to easily manage other classes
    public delegate void EventAction();
    public event EventAction OnMildReset;
    public event EventAction OnHardReset;
    public event EventAction OnGameStart;

    public int dotsRemaining = 244;
    public int lifes = 3;

    void Start()
    {
        OnHardReset += OnMildReset;

        StartCoroutine(GameStart());
    }

    public void LoseLife()
    {
        lifes--;

        if (lifes > 0)
        {
            MildReset();
        }
        else
        {
            ScoreManager.instance.ResetState();
            HardReset();
        }
    }

    public void OnDotEated()
    {
        dotsRemaining--;

        if(dotsRemaining == 0)
        {
            //EndGame
            HardReset();
        }
    }

    private IEnumerator GameStart()
    {
        dotsRemaining = 244;
        lifes = 3;

        AudioManager.instance.PlayClip("Intro");

        yield return new WaitForSeconds(4.25f);


        AudioManager.instance.PlaySiren();

        OnGameStart();
    }

    private void MildReset()
    {
        OnMildReset();
    }

    private void HardReset()
    {
        OnHardReset();

        StartCoroutine(GameStart());
    }
}
