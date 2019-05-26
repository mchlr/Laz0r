using UnityEngine;

public class Mirror : TilemapObject, Placeable_if
{

    private static int nr = 1;
    private GameObject mir;

    public void hover()
    {
        mir = GameObject.CreatePrimitive(PrimitiveType.Cube);
        Destroy(mir.GetComponent<BoxCollider>());

        mir.gameObject.name = "Mirror(onMouse) ";
        mir.transform.SetParent(map.transform);
        mir.transform.localScale = new Vector3(1f, 1f, 0);

        Material reflMat = Resources.Load<Material>("Materials/ReflectorMaterial");
        mir.GetComponent<MeshRenderer>().material = reflMat;
        mir.GetComponent<MeshRenderer>().material.SetColor("_Color", new Color(0.4f, 0.4f, 0.4f, 0.7f));

        setPos();

    }


    public bool add()
    {

        setPos();

        if (!isOtherObj(mir))
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

    public void del()
    {
        Destroy(mir);
    }

    public void move()
    {
        del();
        hover();
    }

    public void setPos()
    {
        mir.transform.position = mouseToTilePos();
    }

    public Vector3 getPos()
    {
        return mir.transform.position;
    }

}
