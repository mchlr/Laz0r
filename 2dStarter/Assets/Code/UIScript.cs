using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UIScript : MonoBehaviour
{

    public GameObject InventoryPanel;

    public void setDeleteState(bool val)
    {
        GameSystem.me.setDeleteState(val);
    }
    public void setMoveState(bool val)
    {
        GameSystem.me.setMoveState(val);
    }



}
