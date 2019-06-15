using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reflector : TilemapObject, Placeable_if
{
    public string debugName;


    private LineRenderer lineRenderer;

    private Vector3 renderStart;
    private Vector3 renderStop;

    private bool isHit;
    private Vector3 hitDir; // Hit direction;
    private RaycastHit hit; // Position of the hit collider;

    // Start is called before the first frame update
    void Start()
    {
        // Setup the LineRenderer;
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = false; // constantly firering
        lineRenderer.useWorldSpace = true; // origin world points

        isHit = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.DrawLine(renderStart, renderStop);

        if (isHit)
        {
            lineRenderer.enabled = true;

            lineRenderer.SetPosition(0, this.renderStart);
            lineRenderer.SetPosition(1, this.renderStop);

            
        }
        else if (lineRenderer.enabled)
        {
            lineRenderer.enabled = false;
        }
    }

    public void detectHit(bool foo, Vector3 inDir, RaycastHit hit)
    {
        this.isHit = foo;
        this.hitDir = inDir;
        this.hit = hit;

        if (this.isHit)
        {
            // Debug

            //Debug.Log(string.Format("{0} got hit from {1}", this.debugName, inDir.ToString()));

            //lineRenderer.material.SetColor("_Color", new Color((int)Random.value * 255, (int)Random.value * 255, (int)Random.value * 255));

            // Debug

            lineRenderer.enabled = true;
            renderStart = this.hit.point;


            lineRenderer.SetPosition(0, this.hit.point);

            Vector3 refl = Vector3.Reflect(this.hitDir, this.hit.normal);

            // Laser hits something (reflective);
            if (Physics.Raycast(this.hit.point, refl, out RaycastHit hitPoint))
            {
                Reflector hitScript = hitPoint.collider.gameObject.GetComponent<Reflector>();

                //Debug.Log("Got hit!");
                //Debug.Log(hitScript);

                lineRenderer.SetPosition(1, hitPoint.point);
                renderStop = hit.point;

                Debug.DrawLine(this.hit.point, hitPoint.point);

                if (hitScript != null)
                {
                    //Debug.Log(string.Format("{0} hit {1}", this.debugName, hitScript.debugName));
                    

                    hitScript.detectHit(true, refl, hitPoint);
                }
            }
            // Laser goes into nothing or hits something non-reflective;
            else
            {
                lineRenderer.SetPosition(1, refl);
                Debug.DrawLine(this.hit.point, refl);

                return;
            }
        }
    }

    public void hover(bool b)
    {
        throw new System.NotImplementedException();
    }

    public bool add()
    {
        throw new System.NotImplementedException();
    }

    public void del()
    {
        throw new System.NotImplementedException();
    }

    public void setPos()
    {
        throw new System.NotImplementedException();
    }

    public Vector3 getPos()
    {
        throw new System.NotImplementedException();
    }

    public bool isObjMarked(Placeable_if otherObj)
    {
        throw new System.NotImplementedException();
    }
}
