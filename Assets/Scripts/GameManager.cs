using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject circleTransition;
    public GameObject circle;
    public TextMeshProUGUI scoreBoard;
    public GameObject ball;
    public GameObject gameOverScreen;
    public TextMeshProUGUI highScoreText;

    private int score;
    private GameObject ballClone;
    private float timeElapsed;

    private void Awake()
    {
        score = 0;
        timeElapsed = 0;
    }

    void Start()
    {
        LeanTween.moveY(circleTransition.GetComponent<RectTransform>(), 1400, 1.0f).setDestroyOnComplete(true);
        highScoreText.text = PlayerPrefs.GetInt("HighScore").ToString();
    }


    void Update()
    {
        timeElapsed += Time.deltaTime;

        if(timeElapsed > 1)
        {
            //Circle on touch
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                if(touch.phase == TouchPhase.Began)
                {
                    circle.SetActive(true);
                }

                if(touch.phase == TouchPhase.Ended)
                {
                    circle.SetActive(false);
                }
                
            }
            else if (Input.GetKeyUp(KeyCode.Space))
            {
                circle.SetActive(false);
            }

            //Update scoreboard
            scoreBoard.text = score.ToString();

            //Update high score
            if (score > PlayerPrefs.GetInt("HighScore"))
            {
                PlayerPrefs.SetInt("HighScore", score);
                highScoreText.text = PlayerPrefs.GetInt("HighScore").ToString();
            }

            if (ballClone == null)
            {
                ballClone = Instantiate(ball, gameObject.transform);
                ballClone.GetComponent<BallScript>().addScoreEvent += addScore;
                ballClone.GetComponent<BallScript>().gameOver += gameOverOverlay;
            }
        }
    }

    void addScore()
    {
        FindObjectOfType<AudioManager>().Play("Score");
        ballClone.GetComponent<BallScript>().addScoreEvent -= addScore;
        LeanTween.scale(ballClone, new Vector3(0, 0, 0), 0.2f);
        StartCoroutine(destroyBall());
        score += 1;
        
        
    }

    void gameOverOverlay()
    {
        FindObjectOfType<AudioManager>().Play("GameOver");
        Time.timeScale = 0f;
        ball.GetComponent<BallScript>().gameOver -= gameOverOverlay;
        gameOverScreen.SetActive(true);
        

    }

    IEnumerator destroyBall()
    {
        yield return new WaitForSeconds(0.2f);
        Destroy(ballClone);
    }

    public void restartGame()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1f;
    }

    private void OnApplicationQuit()
    {
        //PlayerPrefs.SetInt("HighScore", 0);
        PlayerPrefs.Save();
    }
}
