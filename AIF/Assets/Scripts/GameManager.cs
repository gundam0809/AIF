using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    //referencing text
    public HighScore highScore;
    public GameObject canvasGameObject; 
    public TextMeshProUGUI messageText;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI scoreText;
    public MenuManager menuManager;

    public TargetHealth[] targets;
    public GameObject player;
    public Camera worldCamera;
    public TextMeshProUGUI healthText;
    public float health = 10;
    public float startTimerAmount = 3;
    private float startTimer;

    public float targetActivateTimerAmount = 1;
    private float targetActivateTimer;

    public float gameTimerAmount = 60;
    private float gameTimer;

    private int score = 0;

    public enum GameState
    {
        Start,
        Playing,
        GameOver
    };

    public GameState gameState;
    public GameState State { get { return gameState; } }
    public void Awake()
    {
        gameState = GameState.GameOver;
    }
    public void Start()
    {
        player.SetActive(false);
        worldCamera.gameObject.SetActive(true);
        messageText.text = "Press Enter to Start";
        timerText.text = "";
        scoreText.text = "";
        healthText.text = "";
        for (int i = 0; i < targets.Length; i++)
        {
            targets[i].GameManager = this;
            targets[i].gameObject.SetActive(false);
        }
        startTimer = startTimerAmount;

    }
    public void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Application.Quit();
        }

        switch (gameState)
        {
            case GameState.Start:
                GameStateStart();
                break;
            case GameState.Playing:
                GameStatePlaying();
                break;
            case GameState.GameOver:
                GameStateGameOver();
                break;
        }
    }
    public void OnNewGame()
    {
        gameState = GameState.Start;
    }
    // 1 reference
    public void GameStateStart()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        startTimer -= Time.deltaTime;
        canvasGameObject.SetActive(false);
        messageText.text = "Get Ready " + (int)(startTimer + 1);
        if (startTimer <= 0)
        {
            messageText.text = "";
            gameState = GameState.Playing;
            gameTimer = gameTimerAmount;
            startTimer = startTimerAmount;
            score = 0;

            player.SetActive(true);

            worldCamera.gameObject.SetActive(false);
            healthText.gameObject.SetActive(false);
        }
    }

    // 1 reference
    public void GameStatePlaying()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        canvasGameObject.SetActive(true);
        healthText.gameObject.SetActive(true);
        healthText.text = "Health: " + health;
        gameTimer -= Time.deltaTime;
        int seconds = Mathf.RoundToInt(gameTimer);
        timerText.text = string.Format("Time: {0:D2}:{1:D2}",
                                        (seconds / 60), (seconds % 60));
        scoreText.text = ("Score: " + score);
        if (gameTimer <= 0 || health <= 0)
        {
            Debug.Log("Game Over Score: " + score);
            messageText.text = "Game Over! Score: " + score;
            gameState = GameState.GameOver;
            player.SetActive(false);
            worldCamera.gameObject.SetActive(true);
            for (int i = 0; i < targets.Length; i++)
            {
                targets[i].gameObject.SetActive(false);
            }
            highScore.AddScore(score);
            highScore.SaveScoresToFile();

            SceneManager.LoadScene(3);
            Cursor.lockState = CursorLockMode.None;
        }

        // Timer before activating Target.
        targetActivateTimer -= Time.deltaTime;
        if (targetActivateTimer <= 0)
        {
            //We dont use this because all our 'targets' are awake on start
            //ActivateRandomTarget();
            targetActivateTimer = targetActivateTimerAmount;
        }

    }

    // 1 reference
    public void GameStateGameOver()
    {
      if (Input.GetKeyUp(KeyCode.Return))
      {
        gameState = GameState.Start;
        timerText.text = "";
        scoreText.text = "";
      }
    }
/*    // Randomly activates a target.
    private void ActivateRandomTarget()
    {
        int randomIndex = Random.Range(0, targets.Length);
        if (targets[randomIndex] != null) 
            targets[randomIndex].gameObject.SetActive(true);
    }*/

    public void AddScore(int points)
    {
        score += points;
        scoreText.text = "Score " + score;
    }
    public void MinusHealth()
    {
        health = health -1;
        scoreText.text = "Health " + health;
    }
    public void OnHighScores()
    {
        messageText.text = "";

        string text = "";
        for (int i = 0; i < highScore.scores.Length; i++)
        {
            text += highScore.scores[i] + "\n";
        }
    }
}

