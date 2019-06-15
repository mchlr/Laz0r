using UnityEngine;

public class Mirror : TilemapObject, Placeable_if
{

    private static int nr = 1;


    public void hover(bool b)
    {

        if (obj == null)
        {
            obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
            Destroy(obj.GetComponent<BoxCollider>());

            obj.gameObject.name = "Mirror(onMouse) ";
            obj.transform.SetParent(map.transform);
            obj.transform.localScale = new Vector3(1f, 1f, 0);

            Material reflMat = Resources.Load<Material>("Materials/ReflectorMaterial");
            obj.GetComponent<MeshRenderer>().material = reflMat;

        }

        if (b)
        {

            obj.GetComponent<MeshRenderer>().material.SetColor("_Color", new Color(1, 1, 1, 0.7f));

        }
        else
        {

            obj.GetComponent<MeshRenderer>().material.SetColor("_Color", new Color(1, 1, 1, 0.0f));

        }

        setPos();

    }


    public bool add()
    {

        setPos();

        if (!isOtherObj())
        {

            obj.gameObject.name = "Mirror " + nr;
            obj.AddComponent<BoxCollider>();
            obj.GetComponent<MeshRenderer>().material.color = new Color(1f, 1f, 1f, 1f);

            Debug.Log("New Mirror " + nr);
            Debug.Log("on Position: " + obj.transform.position);

            nr++;

            return true;

        }

        Debug.Log("Mirror not created");

        return false;

    }

    public void del()
    {
        Destroy(obj);
    }

    /* public void move()
     {
         del();
         hover(true);
     }*/

    public void setPos()
    {
        obj.transform.position = mouseToTilePos();
    }

    public Vector3 getPos()
    {
        return obj.transform.position;
    }


    public bool isObjMarked(Placeable_if otherObj)
    {

        if (obj.transform.position == otherObj.getPos())
        {
            return true;
        }

        return false;

    }

}
