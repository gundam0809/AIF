using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI greentext;
    public TextMeshProUGUI purpletext;
    public float green = 0;
    public float purple = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        greentext.text = ("Green: " + green);
        purpletext.text = ("Purple: " + purple);
    }
    public void AddGreen()
    {
        green = green + 1;
    }
}
