using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorPlacing : MonoBehaviour
{

    private int i = 1;
    private GameObject box;
    private Grid[] grid;
    private Vector3 mouse_pos, tile_pos;

    // Start is called before the first frame update
    void Start()
    {

        grid = FindObjectsOfType<Grid>();

        //Debug.Log(grid[0]);  //grid[0] == Hex-Grid


    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {

            mouse_pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouse_pos[2] = 0;
            /*tile_pos = grid[0].WorldToCell(mouse_pos);
            tile_pos[1] -= 4; 
            tile_pos[2] = 0;   Klappt alles noch nicht*/

            //Debug.Log(mouse_pos);
            //Debug.Log(tile_pos);

            box = GameObject.CreatePrimitive(PrimitiveType.Cube);
            box.gameObject.name = "Spiegel " + i;

            Destroy(box.GetComponent<BoxCollider>());
            box.AddComponent<MeshCollider>();

            box.transform.SetParent(grid[0].transform);
            box.transform.Rotate(0, 0, 45);
            box.transform.position = mouse_pos;
            //box.transform.position = tile_pos;
            box.transform.localScale = new Vector3(1, 1, 1);

            i++;

        }

    }
}
