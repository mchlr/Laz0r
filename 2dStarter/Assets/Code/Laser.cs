using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class Laser : MonoBehaviour
{

    public class LaserTrace
    {
        public List<Vector3> tracePoints;
        public List<LaserTrace> nodes;

        public LaserTrace()
        {
            tracePoints = new List<Vector3>();
            nodes = new List<LaserTrace>();

            traceCount++;
        }
    }

    private LineRenderer lineRenderer;
    public Transform LaserHit;

    private int maxReflectionCount = 50;
    private static int traceCount = 0;
    private int renderCount = 1;

    private float maxDistance = 1000000;
    private LaserTrace pointz = new LaserTrace();
    private bool hasChanges;
    List<LineRenderer> rendererZ = new List<LineRenderer>();

    // UI variables;
    public bool showOnHover = true;


    // Davids Klasse;

    private Tilemap map;
    private List<Mirror> mirrors;
    private bool mirrorAdded = false;

    // David 


    //private List<Vector2> asds = new List<Vector2>();


    public void setShowOnHover(bool show)
    {
        Debug.Log("Changing ShowOnHover state to: " + show);
        showOnHover = show;
    }

    // Start is called before the first frame update
    void Start()
    {
        // Hole den LineRenderer, den wir vorhin als Component in Unity geaddet haben
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = true; // constantly firering
        lineRenderer.useWorldSpace = true; // origin world points

        hasChanges = true;

        // Mirrorplacing
        map = FindObjectOfType<Tilemap>();

        mirrors = new List<Mirror>();

        mirrors.Add(new Mirror(map));

        // Mirrorplacing
    }

    // Update is called once per frame
    public void Update()
    {
        if (showOnHover)
        {
            mirrors[mirrors.Count - 1].setPosOnGrid();//Der Spiegel der an der Maus hängt
        }

        if (Input.GetMouseButtonDown(0))
        {

            mirrorAdded = mirrors[mirrors.Count - 1].addMirror();

            if (mirrorAdded)
            {
                mirrors.Add(new Mirror(map));
            }

        }
        if (Input.GetMouseButtonUp(0) && mirrorAdded) //&& mirrorAdded damit nicht neu gerechnet wird ohne das ein Spiegel erstellt wurde
        {
            this.setChanges(true);
            Debug.Log("New Changes!");

            mirrorAdded = false;
        }

        if (hasChanges)
        {
            Debug.Log("Calculating new Reflection!");

            traceCount = 0;

            // Reset points;
            pointz = new LaserTrace();


            // Calculate new reflection;

            pointz.tracePoints.Add(transform.position);
            Debug.Log("Now Reflecting!");

            // Reset "Branch-Renderers"
            if (rendererZ.Count > 0)
            {
                foreach (var ren in rendererZ)
                {
                    Destroy(ren);
                }
                rendererZ = new List<LineRenderer>();
            }

            // Schieß in 45° Wingl
            pointz = drawReflection(transform.position, new Vector3(1, 1, 0), 0, maxReflectionCount, pointz);

            // Draw the in
            lineRenderer.positionCount = pointz.tracePoints.Count;
            for (int i = 0; i < pointz.tracePoints.Count; i++)
            {
                lineRenderer.SetPosition(i, pointz.tracePoints[i]);
            }

            if (rendererZ.Count == 0)
            {
                LineRenderer newL = (new GameObject("line" + renderCount++)).AddComponent<LineRenderer>();
                newL.startWidth = 0.1f;
                newL.endWidth = 0.1f;
                newL.enabled = true;
                rendererZ.Add(newL);

                LineRenderer newR = (new GameObject("line" + renderCount++)).AddComponent<LineRenderer>();
                newR.startWidth = 0.1f;
                newR.endWidth = 0.1f;
                newR.enabled = true;
                rendererZ.Add(newR);
            }

            for (int y = 0; y < pointz.nodes.Count; y++)
            {
                //LineRenderer newL = (new GameObject("line" + renderCount++)).AddComponent<LineRenderer>();

                Debug.Log("Rendering " + pointz.nodes[y].tracePoints.Count + " #Pointz for line" + y);

                LineRenderer rend = rendererZ[y];

                int n = pointz.nodes[y].tracePoints.Count;
                rend.positionCount = n;
                for (int j = 0; j < n; j++)
                {
                    rend.SetPosition(j, pointz.nodes[y].tracePoints[j]);
                    Debug.DrawLine(pointz.nodes[y].tracePoints[j], pointz.nodes[y].tracePoints[j] * 1.2f);
                }
            }

            Debug.Log("Jobs done!");


            hasChanges = false;
        }
    }

    public LaserTrace drawReflection(Vector3 position, Vector3 direction, int bounceCount, int refRemaining, LaserTrace poss)
    {
        Debug.Log(string.Format("----- Reflection #{0} ----- From: {1} - with angle: {2}", bounceCount, position, direction));


        // Genug reflektiert;
        if (refRemaining == 0 || traceCount >= 10)
        {

            Debug.Log("schüss");
            return poss;
        }

        // Weiter reflektieren;


        if (Physics.Raycast(position, direction, out RaycastHit hit, maxDistance))
        {
            //Laser trifft irgendwas;

            //Prüfe, welche Objektart getroffen wird;
            switch (hit.transform.gameObject.tag)
            {
                //Ziel erreicht: Beglückwünschen und Rekursion beenden;
                case "Target":
                    print("Congrats <3");

                    //Gib grünes Licht;
                    Renderer targetRenderer = hit.transform.gameObject.GetComponent<Renderer>();
                    targetRenderer.material.SetColor("_Color", Color.green);

                    //Direction und position aktualisieren auf Treffpunkt;
                    position = hit.point;
                    poss.tracePoints.Add(position);
                    return poss;
                //Nicht-reflektierender Widerstand;
                case "NonReflective":
                    //Direction und position aktualisieren auf Treffpunkt;
                    position = hit.point;
                    poss.tracePoints.Add(position);
                    return poss;
                case "PrismaCube":
                    Debug.Log(string.Format("Hit at: {0}", GetHitFace(hit)));
                    Debug.Log("Center: " + hit.transform.position);

                    var rechts = new Vector3(1, 1, 0);
                    var links = new Vector3(-1, 1, 0);

                    var calcPos = hit.transform.position;
                    position = hit.point;


                    // EXPERIMENTAL

                    if (true)
                    {
                        var tmp = calcPos;
                        tmp.x -= 1f;

                        LaserTrace left = new LaserTrace();
                        left.tracePoints.Add(tmp);
                        left = drawReflection(tmp, links, 0, maxReflectionCount, left);
                        poss.nodes.Add(left);
                        Debug.Log("Created Lefthanded reflection");
                        Debug.Log("Found " + left.tracePoints.Count + " reflection points");


                        tmp = calcPos;
                        tmp.x += 1f;

                        LaserTrace right = new LaserTrace();
                        right.tracePoints.Add(tmp);
                        right = drawReflection(tmp, rechts, 0, maxReflectionCount, right);
                        poss.nodes.Add(right);
                        Debug.Log("Created Righthanded reflection");
                        Debug.Log("Found " + right.tracePoints.Count + " reflection points");
                    }

                    // Finish the current line
                    poss.tracePoints.Add(position);
                    return poss;

                // EXPERIMENTAL

                //Direction und position aktualisieren auf Treffpunkt;

                //return poss;

                default:
                    print("Hit something else...");
                    break;
            }

            //Direction und position aktualisieren auf Treffpunkt;
            direction = Vector3.Reflect(direction, hit.normal);
            position = hit.point;
        }
        else
        {
            //Laser geht ins nix;
            position += direction * maxDistance;
            poss.tracePoints.Add(position);
            return poss;
        }

        poss.tracePoints.Add(position);

        // Recursive call;
        //refRemaining = refRemaining - 1;
        return drawReflection(position, direction, bounceCount + 1, refRemaining - 1, poss);
    }

    public void setChanges(bool flag)
    {
        this.hasChanges = flag;
    }



    private enum CubeFace
    {
        None,
        Oben, // Up
        Down,
        Rechts, // East
        Links, // West
        North,
        South
    }

    private CubeFace GetHitFace(RaycastHit hit)
    {
        Vector3 incomingVec = hit.normal - Vector3.up;

        if (incomingVec == new Vector3(0, -1, -1))
            return CubeFace.South;

        if (incomingVec == new Vector3(0, -1, 1))
            return CubeFace.North;

        if (incomingVec == new Vector3(0, 0, 0))
            return CubeFace.Oben;

        if (incomingVec == new Vector3(1, 1, 1))
            return CubeFace.Down;

        if (incomingVec == new Vector3(-1, -1, 0))
            return CubeFace.Links;

        if (incomingVec == new Vector3(1, -1, 0))
            return CubeFace.Rechts;

        return CubeFace.None;
    }



}