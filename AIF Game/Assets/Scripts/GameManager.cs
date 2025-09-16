using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI greentext;
    public float score = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        greentext.text = ("Green: " + score);
    }
    public void AddScore()
    {
        score = score + 1;
    }
}
