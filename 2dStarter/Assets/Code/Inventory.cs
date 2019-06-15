//using System;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;

//public class Inventory : MonoBehaviour
//{

//    private Dictionary<GameObject, int> inventory;

//    public GameObject InventoryPanel;

//    public Inventory()
//    {
//        // TODO: Check, if this can get removed;
//    }

//    void Start()
//    {
//        Debug.Log("Inventory.Start() hit!");

//        inventory = new Dictionary<GameObject, int>();


//        //How to get a texture;
//        // basic.GetComponent<Material>().mainTexture;


//        // TODO: Load Blocks from File (or something else...);
//        //GameObject basic = Instantiate(Resources.Load<GameObject>("Prefabs/Reflector"));
//        //basic.SetActive(false);
//        //inventory.Add(basic, 5);

//        //GameObject split = Instantiate(Resources.Load<GameObject>("Prefabs/Split"));
//        //split.SetActive(false);
//        //inventory.Add(split, 4);

//        TilemapObject basic = Instantiate(Resources.Load<TilemapObject>("Prefabs/TestReflector"));


//        // Add available blocks into the InventoryPanel in the UI;
//        foreach (KeyValuePair<GameObject, int> e in inventory)
//        {
//            try
//            {
//                //Instantiate(new GameObject());
//                GameObject btn = (GameObject)Instantiate(Resources.Load("Prefabs/UIButton"));

//                btn.transform.SetParent(InventoryPanel.transform);

//                // TODO: Change this to use the available quantity of a given block instead (=> e.value);
//                btn.transform.GetChild(0).GetComponent<Text>().text = e.Value.ToString();

//                // Set the buttons apperance to the image/texture of the corresponding GameObject/ReflectorType;
//                btn.GetComponent<Button>().GetComponent<RawImage>().texture = e.Key.GetComponent<MeshRenderer>().materials[0].mainTexture;

//                // TODO: Create a "generic" decrement()/delete()-method that sticks to decrementing e.value;

//                btn.GetComponent<Button>().onClick.AddListener(delegate { setHoverObject(e.Key); });
//            }
//            catch (Exception ex)
//            {
//                Debug.Log("Ran into an error while creating a new Button!");
//                Debug.Log(ex.Message);
//                Debug.Log(ex.StackTrace);

//            }
//            finally
//            {
//                Debug.Log("Inventory-Constructor done!");
//            }

//        }
//    }

//    void setHoverObject(GameObject target)
//    {
//        Debug.Log("setHoverObject() - Registered Click!");

//        //Texture txt = GetComponent<RawImage>().texture;

//        //GameObject dbg = GetComponent<GameObject>();

//        Debug.Log("setHoverObject() - @Button");
//        Debug.Log("Got GameObject");
//        Debug.Log(target);

//        if (canCreate(target))
//        {
//            GameSystem.me.setHoverObject(target);
//        }
//    }

//    public Dictionary<GameObject, int> toDisplay()
//    {
//        return inventory;
//    }

//    /// <summary>
//    /// Function to check, if the player is allowed to create an object of the given type;
//    /// </summary>
//    /// <param name="tar">The GameObject/Reflector, that will be used to query the inventory</param>
//    /// <returns>True/False if the value for the given key is greater than 0</returns>
//    public bool canCreate(GameObject tar)
//    {
//        return (inventory.ContainsKey(tar) && inventory[tar] > 0);
//    }

//    public void decrement(GameObject tar)
//    {
//        Debug.Log("[INVENTORY] - Decrementing(ADD) count for " + tar.name);
//        //Texture tarTxt = tar.GetComponent<Renderer>().material.mainTexture;

//        if (inventory.ContainsKey(tar))
//        {
//            inventory[tar]--;
//        }
//    }

//    public void delete(GameObject tar)
//    {
//        Debug.Log("[INVENTORY] - Incrementing(DELETE) count for " + tar.name);
//        Texture tarTxt = tar.GetComponent<Renderer>().material.mainTexture;

//        // TODO: Fix this to use sprites (if possible);
//        /*if (inventory.ContainsKey(tarTxt))
//        {
//            inventory[tarTxt]++;
//        }*/
//    }
//}
