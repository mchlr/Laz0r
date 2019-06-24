using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeControl : MonoBehaviour {

    // Source: https://github.com/PointyPigheadGames/Highscore-and-Timer-System

    private int time = 0;
    public Text timer;
    public Text highscore;
    private bool newHighscore;

	void Start () 
    {
        if (PlayerPrefs.HasKey("Highscore") == true) {
            highscore.text = PlayerPrefs.GetInt("Highscore").ToString();
        }
        else {
            highscore.text = "-";
        }
        StartTimer();
	}
	
    public void StartTimer () 
    {
        time = 0;
        InvokeRepeating("IncrimentTime", 1, 1);
    }

	public void StopTimer()
	{
        CancelInvoke();
        if (PlayerPrefs.GetInt("Highscore") >= time) {
            SetHighscore();
            newHighscore = true;
        }

	}

    public void SetHighscore () 
    {
        PlayerPrefs.SetInt("Highscore", time);
        highscore.text = PlayerPrefs.GetInt("Highscore").ToString();

    }

    public void ClearHighscores () 
    {
        PlayerPrefs.DeleteKey("Highscore");
        highscore.text = "-";
    }

    void IncrimentTime () 
    {
        time += 1;
        timer.text = time.ToString();
    }

    public bool isNewHighscore()
    {
        return newHighscore;
    }
}
