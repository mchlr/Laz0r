using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    private LineRenderer lineRenderer;
    public Transform LaserHit;

    private int maxReflectionCount = 10;
    private float maxDistance = 1000000;

    private List<Vector2> pointz = new List<Vector2>();


    private List<Vector2> asds = new List<Vector2>();

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

        pointz.Add(transform.position);

        // Erzeuge die Linie
        if(pointz.Count == 1)
        {
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


        // [DEBUG-CODE]
        if (asds.Count == 0)
        {
            Vector2 pos;
            Vector2 dir;
            Debug.Log("Now running test code!");

            dir = transform.up;
            pos = transform.position;

            Debug.Log(string.Format("Initial Pos {0} Dir {1}", pos, dir));

            RaycastHit2D hit = Physics2D.Raycast(pos, dir);

            asds.Add(pos);
            pos = hit.point;
            dir = Vector2.Reflect(dir, hit.normal);

            Debug.Log(string.Format("[DEBUG-CODE] First Reflection hit at {0} going out with angle {1}", pos, dir));

            for (int x = 0; x < maxReflectionCount; x++)
            {
                asds.Add(pos);
                hit = Physics2D.Raycast(pos, dir);
                if (hit)
                {
                    pos = hit.point;
                    dir = Vector2.Reflect(dir, hit.normal);

                    Debug.Log(string.Format("[DEBUG-CODE] Reflecting from {0} with angle {1} --- Reflection-Count = {2}", pos, dir, x));
                }
                else
                {
                    Debug.Log("[DEBUG-CODE] Kein hit! :(");
                }
            }
        }

        for (int i = 0; i < asds.Count; i++)
        {
            Debug.DrawLine(asds[i == 0 ? 0 : i - 1], asds[i == asds.Count - 1 ? i : i + 1]);
        }
    }

    private List<Vector2> drawReflection(Vector2 position, Vector2 direction, int bounceCount, int refRemaining, List<Vector2> poss)
    {
        //Debug.Log("Remaining " + refRemaining);

        // Genug reflektiert;
        if (refRemaining == 0)
        {

            Debug.Log("schüss");
            return poss;
        }

        // Weiter reflektieren;

        Vector2 start = position;
        RaycastHit2D hit = Physics2D.Raycast(position, direction, maxDistance);

        if (hit)
        {
            //Laser trifft irgendwas
            direction = Vector2.Reflect(direction, hit.normal);
            position = hit.point;

            //Debug.Log(string.Format("Laser hit at {0} out with {1} (Direction)", position, direction));
        }
        else
        {
            //Laser geht ins nix
            position += direction * maxDistance;
            poss.Add(position);
            return poss;
        }

        //Debug.DrawLine(start, position);
        poss.Add(position);

        LaserHit.position = hit.point;
        //lineRenderer.SetPosition(bounceCount, start);
        //lineRenderer.SetPosition(++bounceCount, LaserHit.position);

        // Recursive call;
        refRemaining -= 1;
        return drawReflection(position, direction, bounceCount + 2, refRemaining, poss);
    }
}
