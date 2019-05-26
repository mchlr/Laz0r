using UnityEngine;
using UnityEngine.Tilemaps;

public abstract class TilemapObject : ScriptableObject
{

    public Tilemap map;

    public TilemapObject()
    {
        map = FindObjectOfType<Tilemap>();
    }

    public Vector3 mouseToTilePos()
    {

        Vector3 mouse_pos, pos;

        pos = new Vector3();
        mouse_pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (mouse_pos[0] >= 0)
        {
            pos[0] = (int)mouse_pos[0] + map.transform.localScale.x / 2;
        }
        else
        {
            pos[0] = (int)mouse_pos[0] - map.transform.localScale.x / 2;
        }


        if (pos[0] % map.transform.localScale.x == 0)//Wenn auf Rahmen gesetzt
        {
            pos[0] = (int)mouse_pos[0];
        }


        if (mouse_pos[0] >= 0)
        {
            pos[1] = (int)mouse_pos[1] + map.transform.localScale.y / 2;
        }
        else
        {
            pos[1] = (int)mouse_pos[1] - map.transform.localScale.y / 2;
        }

        if (pos[1] % map.transform.localScale.y == 0)
        {
            pos[1] = (int)mouse_pos[1];
        }

        pos[2] = 0; //z = 0

        return pos;

    }

    public bool isOtherObj(GameObject thisObj)
    {

        GameObject[] obj = FindObjectsOfType<GameObject>(); ;

        for (int x = 0; x < obj.Length; x++)
        {

            if (obj[x].transform.position == thisObj.transform.position && !obj[x].Equals(thisObj))
            {
                return true;

            }

        }

        return false;

    }


}
