using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TimeControl : MonoBehaviour {

    // Source: https://github.com/PointyPigheadGames/Highscore-and-Timer-System

    private int time = 0;
    public Text timer;
    public Text highscore;
    private bool newHighscore;

	void Start () 
    {
        if (PlayerPrefs.HasKey("Highscore_" + SceneManager.GetActiveScene().name))
        {
            Debug.Log("Found Highscore!");
            Debug.Log(PlayerPrefs.GetInt("Highscore_" + SceneManager.GetActiveScene().name).ToString());

            string score = PlayerPrefs.GetInt("Highscore_" + SceneManager.GetActiveScene().name).ToString();
            highscore.text = score == "9999" ? "-" : score; 
        }
        else
        {
            highscore.text = "-";
            PlayerPrefs.SetInt("Highscore_" + SceneManager.GetActiveScene().name, 9999);

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
        if (PlayerPrefs.GetInt("Highscore_" + SceneManager.GetActiveScene().name) >= time) {
            SetHighscore();
            newHighscore = true;
        }

	}

    public void SetHighscore () 
    {
        var name = "Highscore_" + SceneManager.GetActiveScene().name;

        Debug.Log("Setting Highscore for: " + name + " to: " + time);


        PlayerPrefs.SetInt("Highscore_" + SceneManager.GetActiveScene().name, time);
        highscore.text = PlayerPrefs.GetInt("Highscore_" + SceneManager.GetActiveScene().name).ToString();

    }

    public void ClearHighscores () 
    {
        PlayerPrefs.DeleteKey("Highscore_" + SceneManager.GetActiveScene().name);
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
