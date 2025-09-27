using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuManager : MonoBehaviour
{
    public HighScore highScore;
    public TextMeshProUGUI HighScoreText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void LoadGameScene()
    {
        SceneManager.LoadScene(1);
    }
    public void LoadMenu()
    {
        SceneManager.LoadScene(0);
        Cursor.lockState = CursorLockMode.None;

    }
    public void LoadScores()
    {
        SceneManager.LoadScene(2);
        Cursor.lockState = CursorLockMode.None;
    }
    public void gameOver()
    {
        SceneManager.LoadScene(3);
        Cursor.lockState = CursorLockMode.None;
    }
}
