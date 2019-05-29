using UnityEngine;
using UnityEngine.Tilemaps;

public abstract class TilemapObject : ScriptableObject
{

    public Tilemap map;
    public GameObject obj;

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

    public bool isOtherObj()
    {

        GameObject[] otherObj = FindObjectsOfType<GameObject>(); ;

        for (int x = 0; x < otherObj.Length; x++)
        {

            if (otherObj[x].transform.position == obj.transform.position && !otherObj[x].Equals(obj))
            {
                return true;

            }

        }

        return false;

    }


}
