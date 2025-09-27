using TMPro;
using UnityEngine;

public class HighScoreTXT : MonoBehaviour
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
        string text = "";
        for (int i = 0; i < highScore.scores.Length; i++)
        {
            text += highScore.scores[i] + "\n";
        }
        HighScoreText.text = text;
    }
}
