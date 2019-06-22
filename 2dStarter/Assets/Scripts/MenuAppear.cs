using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuAppear : MonoBehaviour
{
    // Source: https://answers.unity.com/questions/850220/how-can-i-get-a-ui-canvas-to-hideappear-on-esc-but.html

    public GameObject menu; // Assign in inspector
    public Text text;
    private bool isShowing;

    void Update()
    {
        menu.SetActive(isShowing);

    }

    public void showCongratsCanvas()
    {
        isShowing = true;
        menu.SetActive(isShowing);

    }

    public void showHighscoreText()
    {
        text.text = "Yeah, cool, ein neuer Highscore!";
    }
}