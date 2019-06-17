using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Linq;

public class GameSystem : MonoBehaviour
{
    public static GameSystem me = null;

    //public Inventory PlayerInv;

    public TestInventory PlayerInv;
    public Tilemap GameMap;
    public Laser GameLaser;

    private List<TilemapPrefab> GameObjects;

    private TilemapPrefab currentHoveringObject;

    private bool doHover;
    private bool doDelete;
    private bool doMove;


    private GameObject hoverPrefab;


    // Note from Unity: Awake is always called before any Start functions;
    void Awake()
    {
        Debug.Log("[GameSystem] Awake() Hit!");

        if (me == null)
        {
            me = this;
        }

        GameObjects = new List<TilemapPrefab>();
        Debug.Log("[GameSystem] Finished initialization.");

        Debug.Log("Inventory:");
        Debug.Log(PlayerInv);

        Debug.Log("GameMap:");
        Debug.Log(GameMap);

        Debug.Log("GameObjects:");
        Debug.Log(GameObjects);

        doHover = false;

        Debug.Log("-------------------------------------");
    }

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {

        


        if(doHover)
        {
            currentHoveringObject.hover(true);
        }

        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("[GameSystem] - Click!");
            Vector3 mouse_pos = setPosOnGrid(Camera.main.ScreenToWorldPoint(Input.mousePosition));

            // Check if the player has clicked within the map;
            if (GameMap.HasTile(GameMap.WorldToCell(mouse_pos))) {
                if (doDelete)
                {
                    DeleteReflector(mouse_pos);
                }

                if (doMove)
                {
                    DeleteReflector(mouse_pos);

                    doHover = true;
                }

                //hoverObject.hover(false);
                if (doHover && currentHoveringObject.add(GameObjects.Count))
                {
                    if (PlayerInv.canCreate(hoverPrefab))
                    {
                        GameObjects.Add(currentHoveringObject);
                        currentHoveringObject = new TilemapPrefab(hoverPrefab);

                        // If an object has been moved;
                        if (doMove)
                        {
                            doMove = false;
                            doHover = false;
                        }
                    }
                    else
                    {
                        doHover = false;
                    }
                    GameLaser.setChanges(true);
                }
            }
        }
    }

    // Custom Methods();


    public void setHoverObject(GameObject tar)
    {
        currentHoveringObject = new TilemapPrefab(tar);
        hoverPrefab = tar;

        doDelete = false;
        doMove = false;
        doHover = true;

        Debug.Log("currentHoveringObject set to");
        Debug.Log(currentHoveringObject.obj);
    }

    public void setDeleteState(bool val)
    {
        doHover = false;
        doMove = false;
        doDelete = val;
    }
    public void setMoveState(bool val)
    {
        doHover = false;
        doDelete = false;
        doMove = val;
    }



    // TODO: Think about moving this to an external Util-Class;

    private Vector3 setPosOnGrid(Vector3 mouse_pos)
    {
        Debug.Log("Click at " + mouse_pos.ToString());

        Vector3 pos = new Vector3();


        if (mouse_pos[0] >= 0)
        {
            pos[0] = (int)mouse_pos[0] + GameMap.transform.localScale.x / 2;
        }
        else
        {
            pos[0] = (int)mouse_pos[0] - GameMap.transform.localScale.x / 2;
        }


        if (pos[0] % GameMap.transform.localScale.x == 0)//Wenn auf Rahmen gesetzt
        {
            pos[0] = (int)mouse_pos[0];
        }


        if (mouse_pos[0] >= 0)
        {
            pos[1] = (int)mouse_pos[1] + GameMap.transform.localScale.y / 2;
        }
        else
        {
            pos[1] = (int)mouse_pos[1] - GameMap.transform.localScale.y / 2;
        }

        if (pos[1] % GameMap.transform.localScale.y == 0)
        {
            pos[1] = (int)mouse_pos[1];
        }

        pos[2] = 0; //z = 0

        Debug.Log("Transformed to " + pos.ToString());

        return pos;
    }

    private void DeleteReflector(Vector3 mouse_pos)
    {
        int remIdx = 0;
        foreach (TilemapPrefab place in GameObjects)
        {
            if (place.obj.transform.position.Equals(mouse_pos))
            {
                Debug.Log("--------------------");
                Debug.Log("Destroying: " + place.obj.name);
                Debug.Log(place);

                PlayerInv.increment(place.obj);
                place.del();

                if (doHover) { setHoverObject(place.obj); }

                GameLaser.setChanges(true);
                GameObjects.RemoveAt(remIdx);
                break;
            }
            remIdx++;
        }
    }
}
