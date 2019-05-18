using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Mirror : ScriptableObject //Für Destroy und find Objects
{

    private static int nr = 1;
    private GameObject mir;
    private Tilemap map;

    public Mirror(Tilemap map)
    {

        Material[] mat;

        this.map = map;
        
        //Für Platierungshilfe schon erstellen
        mir = GameObject.CreatePrimitive(PrimitiveType.Cube);
        Destroy(mir.GetComponent<BoxCollider>());
        mir.gameObject.name = "Mirror(onMouse) ";
        mir.transform.SetParent(map.transform);

        // Make the mirror stand straight;
        mir.transform.localScale = new Vector3(1f, 1f, 1);
        //mir.transform.Rotate(0, 0, 45);
        //mir.transform.localScale = new Vector3(0.7f, 0.7f, 1);


        Material reflMat = Resources.Load<Material>("Materials/ReflectorMaterial");
        mir.GetComponent<MeshRenderer>().material = reflMat;
        mir.GetComponent<MeshRenderer>().material.SetColor("_Color", new Color(0.4f, 0.4f, 0.4f, 0.7f));

        // Original
        //mir.GetComponent<MeshRenderer>().material.shader = Shader.Find("Sprites/Default");

        setPosOnGrid();

    }


    public bool addMirror() //vlt bool return
    {

        setPosOnGrid();

        if (!isOtherMirror())
        {

            mir.gameObject.name = "Mirror " + nr;
            mir.AddComponent<BoxCollider>();
            mir.GetComponent<MeshRenderer>().material.color = new Color(1f, 1f, 1f, 1f);

            Debug.Log("New Mirror " + nr);
            Debug.Log("on Position: " + mir.transform.position);

            nr++;

            return true;

        }
        
        Debug.Log("Mirror not created");

        return false;        

    }

    
    public void setPosOnGrid()
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

        mir.transform.position = pos;

    }


    public bool isOtherMirror()
    {

        GameObject[] otherMir = FindObjectsOfType<GameObject>(); ;

        for (int x = 0; x < otherMir.Length; x++) 
        {

            if (otherMir[x].transform.position == mir.transform.position && !otherMir[x].Equals(mir))
            {
                return true;

            }

        }
        
        return false;
    }


}
