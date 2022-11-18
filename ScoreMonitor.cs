using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class ScoreMonitor : MonoBehaviour
{
    public TextMeshProUGUI scoreOfGame;
    
    
    // Start is called before the first frame update
    void Start()
    {
        scoreOfGame.text = "Your score is: 0";
    }

    // Update is called once per frame
    void Update()
    {
        Process process = CameraPointer.process;
        int currentScore = process.GetScore();
        scoreOfGame.text = "Your score is: " + currentScore;
    }
}
