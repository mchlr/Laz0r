using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilemapPrefab : TilemapObject, Placeable_if
{
    private string test;
    private static int idx = 0;
    public string name;

    public TilemapPrefab() : base() { }

    public TilemapPrefab(GameObject tar) 
        : base(tar)
    {
        test = "ASDFGH " + idx;
        idx += 1;

        name = Guid.NewGuid().ToString();
        //var uid = new Guid();
    }


    public bool add(int idx)
    {
        setPos();

        Debug.Log("isOtherObj? -> " + isOtherObj());
        Debug.Log("canCreate? -> " + GameSystem.me.PlayerInv.canCreate(obj));
        Debug.Log("Expected false, true");

        if (!isOtherObj() && GameSystem.me.PlayerInv.canCreate(obj))
        {
            GameSystem.me.PlayerInv.decrement(obj);

            obj.gameObject.name = "Object " + idx;
            obj.AddComponent<BoxCollider>();

            return true;
        }
        return false;
    }

    public bool add()
    {
        

        if (!isOtherObj() && GameSystem.me.PlayerInv.canCreate(obj))
        {
            GameSystem.me.PlayerInv.decrement(obj);

            obj.gameObject.name = "Object X";
            obj.AddComponent<BoxCollider>();

            return true;
        }
        return false;
    }

    public void del()
    {
        Destroy(obj);
    }

    public Vector3 getPos()
    {
        throw new System.NotImplementedException();
    }

    public void hover(bool b)
    {
        


        /*if (obj == null)
        {
            Destroy(obj.GetComponent<BoxCollider>());

            obj.gameObject.name = "Prefab(onMouse)";

            Debug.Log("[Prefab] - Scale:");
            Debug.Log("obj.gameObject.transform.localScale");
            Debug.Log(obj.gameObject.transform.localScale);

            //obj.gameObject.transform.localScale = new Vector3(2f, 2f, 1);

            Debug.Log("obj.transform.localScale");
            Debug.Log(obj.transform.localScale);



            //obj.transform.SetParent(map.transform);
            //obj.transform.localScale = new Vector3(2f, 2f, 0);



            //Material reflMat = Resources.Load<Material>("Materials/ReflectorMaterial");
            //obj.GetComponent<MeshRenderer>().material = reflMat;

        }*/


        // Check, if the GameObject hasn't been sacled yet;
        if(obj.gameObject.transform.localScale.x != 2f)
        {
            obj.gameObject.transform.localScale = new Vector3(2f, 2f, 1);
        }
        if (b)
        {

            obj.GetComponent<MeshRenderer>().material.SetColor("_Color", new Color(1, 1, 1, 1));

        }
        else
        {

            obj.GetComponent<MeshRenderer>().material.SetColor("_Color", new Color(0, 1, 0, 1));

        }

        setPos();

    }

    public bool isObjMarked(Placeable_if otherObj)
    {
        throw new System.NotImplementedException();
    }

    public void setPos()
    {
        obj.transform.position = mouseToTilePos();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public string ToString()
    {
        return test;
    }
    
}
