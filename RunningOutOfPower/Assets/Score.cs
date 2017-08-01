using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Score : MonoBehaviour
{
    public Text ScoreBox;
    public Text WinLoseBox;

    void Start()
    {
        ScoreBox.text = "YOUR SCORE: " + SubManager.mGameScore;

        if(SubManager.mGameScore > SubManager.HighScore)
        {
            WinLoseBox.text = "CONGRATULATIONS YOU BEAT THE HIGH SCORE";
        }
        else
        {
            WinLoseBox.text = "Too bad... You'll probably never beat the high score.";
        }
    }


    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Space))
        {
            SceneManager.LoadScene("Main");
        }
    }
}
