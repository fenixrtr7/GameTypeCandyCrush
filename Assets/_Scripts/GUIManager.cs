using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIManager : MonoBehaviour
{
    public static GUIManager sharedInstance;
    public Text movesText, scoreText, maxScoreText, maxScoreText2, factorMultiText;
    int saveMoveCounter, score, maxScore;
    public int moveCounter = 20;
    public int Score
    {
        get
        { return score; }
        set
        {
            score = value;
            scoreText.text = "Score: " + score;
        }
    }
    public int MoveCounter
    {
        get { return moveCounter; }
        set
        {
            moveCounter = value;
            movesText.text = "Moves: " + moveCounter;
            if (moveCounter <= 0)
            {
                moveCounter = 0;
                StartCoroutine(GameOver());
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (sharedInstance == null)
        {
            sharedInstance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        score = 0;
        saveMoveCounter = moveCounter;
        maxScore = PlayerPrefs.GetInt("MaxScore", 0);
        UpdateSoreMoves();
        UpdateMaxScore();
    }

    private IEnumerator GameOver()
    {
        if (score > maxScore)
        {
            PlayerPrefs.SetInt("MaxScore", score);
            maxScore = PlayerPrefs.GetInt("MaxScore");
            UpdateMaxScore();
        }

        yield return new WaitUntil(() => !BoardManager.sharedInstance.isShifting);
        yield return new WaitForSeconds(0.25f);

        MenuManager.sharedInstance.ShowGameOver();
    }
    public void Reset()
    {
        score = 0;
        moveCounter = saveMoveCounter;
        UpdateSoreMoves();
    }

    // Actualizar Max Score
    void UpdateMaxScore()
    {
        maxScoreText.text = "Max Score: " + maxScore;
        maxScoreText2.text = "Max Score: " + maxScore;
    }
    void UpdateSoreMoves()
    {
        movesText.text = "Moves: " + moveCounter;
        scoreText.text = "Score: " + score;
    }

    public void PintFactorMulti(int dato){
        factorMultiText.text = "x " + dato;
    }

}
