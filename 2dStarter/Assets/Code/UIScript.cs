using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UIScript : MonoBehaviour
{

    public GameObject InventoryPanel;

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
    public void setDeleteState(bool val)
    {
        GameSystem.me.setDeleteState(val);
    }
    public void setMoveState(bool val)
    {
        GameSystem.me.setMoveState(val);
    }



}
