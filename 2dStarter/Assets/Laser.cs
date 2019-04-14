using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    private LineRenderer lineRenderer;
    public Transform LaserHit;

    private int maxReflectionCount = 10;
    private float maxDistance = 1000000;

    private List<Vector3> pointz = new List<Vector3>();


    //private List<Vector2> asds = new List<Vector2>();
 
    // Start is called before the first frame update
    void Start()
    {
        // Hole den LineRenderer, den wir vorhin als Component in Unity geaddet haben
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = true; // constantly firering
        lineRenderer.useWorldSpace = true; // origin world points

        // Erzeuge die Linie
        //drawReflection(transform.position, transform.up, 0, maxReflectionCount);
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.Space))
        {
            lineRenderer.enabled = true;
        }
        else
        {
            lineRenderer.enabled = false;
        }

        if(Input.GetKey(KeyCode.Escape) || Input.GetKey(KeyCode.Backspace))
        {
            Debug.Log("Resetting and recalculating reflection!");

            // Reset points;
            pointz = new List<Vector3>();
            

            // Calculate new reflection;

            pointz.Add(transform.position);
            Debug.Log("Now Reflecting!");

            pointz = drawReflection(transform.position, transform.up, 0, maxReflectionCount, pointz);

            lineRenderer.positionCount = pointz.Count;

            Debug.Log(string.Format("Got {0} reflection points", pointz.Count));

            for (int i = 0; i < pointz.Count; i++)
            {
                lineRenderer.SetPosition(i, pointz[i]);
                Debug.Log(pointz[i]);
            }
            Debug.Log("Jobs done!");
        }

        

        //// [DEBUG-CODE]
        //if (asds.Count == 0)
        //{
        //    Vector2 pos;
        //    Vector2 dir;
        //    Debug.Log("Now running test code!");

        //    dir = transform.up;
        //    pos = transform.position;

        //    Debug.Log(string.Format("Initial Pos {0} Dir {1}", pos, dir));

        //    RaycastHit2D hit = Physics2D.Raycast(pos, dir);

        //    asds.Add(pos);
        //    pos = hit.point;
        //    dir = Vector2.Reflect(dir, hit.normal);

        //    Debug.Log(string.Format("[DEBUG-CODE] First Reflection hit at {0} going out with angle {1}", pos, dir));

        //    for (int x = 0; x < maxReflectionCount; x++)
        //    {
        //        asds.Add(pos);
        //        hit = Physics2D.Raycast(pos, dir);
        //        if (hit)
        //        {
        //            pos = hit.point;
        //            dir = Vector2.Reflect(dir, hit.normal);

        //            Debug.Log(string.Format("[DEBUG-CODE] Reflecting from {0} with angle {1} --- Reflection-Count = {2}", pos, dir, x));
        //        }
        //        else
        //        {
        //            Debug.Log("[DEBUG-CODE] Kein hit! :(");
        //        }
        //    }
        //}

        //for (int i = 0; i < asds.Count; i++)
        //{
        //    Debug.DrawLine(asds[i == 0 ? 0 : i - 1], asds[i == asds.Count - 1 ? i : i + 1]);
        //}
    }

    private List<Vector3> drawReflection(Vector3 position, Vector3 direction, int bounceCount, int refRemaining, List<Vector3> poss)
    {
        Debug.Log(string.Format("----- Reflection #{0} ----- From: {1} - with angle: {2}", bounceCount, position, direction));

        // Genug reflektiert;
        if (refRemaining == 0)
        {

            Debug.Log("schüss");
            return poss;
        }

        // Weiter reflektieren;

        if(Physics.Raycast(position, direction, out RaycastHit hit, Mathf.Infinity, 0))
        {
            //Laser trifft irgendwas;
            direction = Vector3.Reflect(direction, hit.normal);
            position = hit.point;
        }
        else
        {
            //Laser geht ins nix;
            position += direction * maxDistance;
            poss.Add(position);
            return poss;
        }

        poss.Add(position);

        // Recursive call;
        return drawReflection(position, direction, bounceCount + 2, refRemaining--, poss);
    }
}
