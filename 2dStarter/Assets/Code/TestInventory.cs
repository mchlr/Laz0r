using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestInventory : MonoBehaviour
{

    private Dictionary<GameObject, int> inventory;

    public GameObject InventoryPanel;

    // TODO: Remove this
    public TilemapPrefab debugPrefab;



    public TestInventory()
    {
    }

    void Awake()
    {
        Debug.Log("[TESTING_INVENTORY].Start() hit!");

        inventory = new Dictionary<GameObject, int>();

        Debug.Log("[INVENTORY] - Loading Prefabs...");

        GameObject basic = Instantiate(Resources.Load<GameObject>("Prefabs/TestReflector"));
        inventory.Add(basic, 10);

        // Move the smol instance out of the cameras view;
        basic.transform.position = new Vector3(1, 255, 0);

        GameObject split = Instantiate(Resources.Load<GameObject>("Prefabs/Split"));
        inventory.Add(split, 4);

        // Move the smol instance out of the cameras view;
        split.transform.position = new Vector3(1, 255, 0);

        // Add available blocks into the InventoryPanel in the UI;
        foreach (KeyValuePair<GameObject, int> e in inventory)
        {
            try
            {
                //Instantiate(new GameObject());
                GameObject btn = (GameObject)Instantiate(Resources.Load("Prefabs/UIButton"));

                btn.transform.SetParent(InventoryPanel.transform);

                // TODO: Change this to use the available quantity of a given block instead (=> e.value);
                btn.transform.GetChild(0).GetComponent<Text>().text = e.Value.ToString();

                // Set the buttons apperance to the image/texture of the corresponding GameObject/ReflectorType;
                btn.GetComponent<Button>().GetComponent<RawImage>().texture = e.Key.GetComponent<MeshRenderer>().materials[0].mainTexture;
                btn.GetComponent<Button>().GetComponent<RawImage>().SizeToParent();

                // TODO: Create a "generic" decrement()/delete()-method that sticks to decrementing e.value;

                btn.GetComponent<Button>().onClick.AddListener(delegate { setHoverObject(e.Key); });
            }
            catch (Exception ex)
            {
                Debug.Log("[TESTING_INVENTORY] Ran into an error while creating a new Button!");
                Debug.Log(ex.Message);
                Debug.Log(ex.StackTrace);

            }
            finally
            {
                Debug.Log("Inventory-Constructor done!");
            }

        }
    }

    void setHoverObject(GameObject target)
    {
        Debug.Log("[TESTING_INVENSTORY] setHoverObject() - Registered Click!");


        Debug.Log("setHoverObject() - @Button");
        Debug.Log("Got GameObject");
        Debug.Log(target);

        if (canCreate(target))
        {
            GameSystem.me.setHoverObject(target);
        }
    }

    public Dictionary<GameObject, int> toDisplay()
    {
        return inventory;
    }

    /// <summary>
    /// Function to check, if the player is allowed to create an object of the given type;
    /// </summary>
    /// <param name="tar">The GameObject/Reflector, that will be used to query the inventory</param>
    /// <returns>True/False if the value for the given key is greater than 0</returns>
    public bool canCreate(GameObject tar)
    {
        foreach(GameObject entity in inventory.Keys)
        {
            if (entity.GetComponent<MeshRenderer>().materials[0].mainTexture.Equals(tar.GetComponent<MeshRenderer>().materials[0].mainTexture))
            {
                return inventory[entity] > 0;
            }
        }
        return false;
    }

    public void decrement(GameObject tar)
    {
        foreach (GameObject entity in inventory.Keys)
        {
            if(entity.GetComponent<MeshRenderer>().materials[0].mainTexture.Equals(tar.GetComponent<MeshRenderer>().materials[0].mainTexture))
            {
                Debug.Log("[INVENTORY] Decrementing for " + entity.ToString() + ". Old Value: " + inventory[entity]);
                inventory[entity]--;
                decrementButtonValue(entity);
                break;
            }
        }
    }

    public void increment(GameObject tar)
    {
        foreach (GameObject entity in inventory.Keys)
        {
            if (entity.GetComponent<MeshRenderer>().materials[0].mainTexture.Equals(tar.GetComponent<MeshRenderer>().materials[0].mainTexture))
            {
                Debug.Log("[INVENTORY] Incrementing for " + entity.ToString() + ". Old Value: " + inventory[entity]);
                inventory[entity]++;
                decrementButtonValue(entity);
                break;
            }
        }
    }

    public void delete(TilemapObject tar)
    {
        Debug.Log("[INVENTORY] - Incrementing(DELETE) count for " + tar.name);
        Texture tarTxt = tar.GetComponent<Renderer>().material.mainTexture;

        // TODO: Fix this to use sprites (if possible);
        /*if (inventory.ContainsKey(tarTxt))
        {
            inventory[tarTxt]++;
        }*/
    }

    TilemapPrefab debugGet()
    {
        return debugPrefab;
    }

    private void decrementButtonValue(GameObject tarEntity)
    {
        Debug.Log("decrementButtonValue()");

        Button[] buttonZ = InventoryPanel.GetComponentsInChildren<Button>();
        foreach (Button but in buttonZ)
        {
            if (but.GetComponent<RawImage>().texture.Equals(tarEntity.GetComponent<MeshRenderer>().materials[0].mainTexture))
            {
                but.transform.GetChild(0).GetComponent<Text>().text = inventory[tarEntity].ToString();
            }
        }
    }
}
