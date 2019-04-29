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
    private bool hasChanges;


    // Davids Klasse;

    private int i = 1;
    private GameObject box;
    private Grid[] grid;
    private Vector3 mouse_pos, tile_pos;

    // David 


    //private List<Vector2> asds = new List<Vector2>();

    // Start is called before the first frame update
    void Start()
    {
        // Hole den LineRenderer, den wir vorhin als Component in Unity geaddet haben
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = true; // constantly firering
        lineRenderer.useWorldSpace = true; // origin world points

        hasChanges = true;

        // Mirrorplacing

        grid = FindObjectsOfType<Grid>();

        // Mirrorplacing
    }

    // Update is called once per frame
    public void Update()
    {

        // Code aus David's Mirrorplacing.cs;

        if (Input.GetMouseButtonDown(0))
        {

            mouse_pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouse_pos[2] = 0; // Setze z == 0;


            /*tile_pos = grid[0].WorldToCell(mouse_pos);
            tile_pos[1] -= 4; 
            tile_pos[2] = 0;   Klappt alles noch nicht*/

            //Debug.Log(mouse_pos);
            //Debug.Log(tile_pos);

            box = GameObject.CreatePrimitive(PrimitiveType.Cube);
            box.gameObject.name = "Spiegel " + i;


            // Notiz Michael: Warum entfernst du den BoxCollider und addest einen MeshCollider? Wir brauchen doch den BoxColider, weil wir quasi die 3D-Physik verwenden;

            //Destroy(box.GetComponent<BoxCollider>());
            //box.AddComponent<MeshCollider>();

            box.transform.SetParent(grid[0].transform);
            box.transform.Rotate(0, 0, 45);
            box.transform.position = mouse_pos;
            //box.transform.position = tile_pos;
            box.transform.localScale = new Vector3(1, 1, 1);

            i++;
        }
        if(Input.GetMouseButtonUp(0))
        {
            this.setChanges(true);
            Debug.Log("New Changes!");
        }

        if (hasChanges)
        {
            Debug.Log("Calculating new Reflection!");

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

            
            hasChanges = false;
        }
    }

    public List<Vector3> drawReflection(Vector3 position, Vector3 direction, int bounceCount, int refRemaining, List<Vector3> poss)
    {
        Debug.Log(string.Format("----- Reflection #{0} ----- From: {1} - with angle: {2}", bounceCount, position, direction));


        // Genug reflektiert;
        if (refRemaining == 0)
        {

            Debug.Log("schüss");
            return poss;
        }

        // Weiter reflektieren;


        if(Physics.Raycast(position, direction, out RaycastHit hit, maxDistance))
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
                    poss.Add(position);
                    return poss;
                //Nicht-reflektierender Widerstand;
                case "NonReflective":
                    //Direction und position aktualisieren auf Treffpunkt;
                    position = hit.point;
                    poss.Add(position);
                    return poss;
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
            poss.Add(position);
            return poss;
        }

        poss.Add(position);

        // Recursive call;
        //refRemaining = refRemaining - 1;
        return drawReflection(position, direction, bounceCount+1, refRemaining-1, poss);
    }

    public void setChanges(bool flag)
    {
        this.hasChanges = flag;
    }
}
