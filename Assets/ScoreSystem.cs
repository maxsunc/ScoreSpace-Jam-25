using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreSystem : MonoBehaviour
{
    [SerializeField]
    private long score;
    private Text scoreText;
    
    void Start()
    {
        scoreText = GameObject.Find("Score").GetComponent<Text>();
        scoreText.text = "Score: " + score;
    }

    public long getScore()
    {
        return score;
    }
    public void addScore(int add)
    {
        score += add;
        scoreText.text = "Score: " + score;
    }
}
