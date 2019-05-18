using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UIScript : MonoBehaviour
{
    public void updateHoverState(bool show)
    {
        GameObject laser;
        laser = GameObject.FindWithTag("Respawn");


        Debug.Log("Found Lazor!");
        Debug.Log(laser);

        Laser lazorScript = laser.GetComponent<Laser>();

        lazorScript.setShowOnHover(show);

        Debug.Log("Set showOnHover to: " + lazorScript.showOnHover);
    }
}
