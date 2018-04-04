using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Line : MonoBehaviour
{

    public bool usePhysics = true;
    public float colliderThickness = 0.1f;
    public float vertexPrecision = 0.01f;
    public GameObject player;
    Vector3 inPoint;
    Vector3 outPoint;
    public Color color;
    public PlayerController playerController;
    private bool isnewLine = true;



    void Start()
    {
        playerController = player.GetComponent<PlayerController>();


    }

    void Update()
    {
        if ((playerController.movementKeyPressed == true))
        {
            if (isnewLine == true)
            {


                if ((Input.GetButtonDown("Horizontal")) || (Input.GetButtonDown("Vertical")))
                {
                    StartCoroutine(draw());

                }
            }

        }

        if (Input.GetButtonDown("Jump"))
        {
            if (isnewLine == false)
            {
                isnewLine = true;
                playerController.movementKeyPressed = true;
            }
            else
                isnewLine = false;
            playerController.movementKeyPressed = false;

        }

        if (Input.GetMouseButtonDown(0))
        {
            playerController.movementKeyPressed = false;
        }


    }
    Vector3 newVertex;
    Vector3 lastVertex;

    IEnumerator draw()
    {
        LineRenderer r = new GameObject().AddComponent<LineRenderer>();
        isnewLine = false;
        r.transform.SetParent(transform);
        r.startWidth = 0.1f;
        r.endWidth = 0.1f;
        r.material.color = color;
        List<Vector3> positions = new List<Vector3>();




        while (playerController.movementKeyPressed == true)
        {
            newVertex = player.transform.position + Vector3.forward * 2;
            if (Vector3.Distance(lastVertex, newVertex) >= vertexPrecision) //Esto mejora la calidad de la linea
            {
                positions.Add(newVertex);
                r.positionCount = positions.Count;
                r.SetPositions(positions.ToArray());
                lastVertex = newVertex;

            }
            yield return new WaitForEndOfFrame();
        }
        r.useWorldSpace = false;




        if (usePhysics)
        {
            List<Vector2> posiciones2 = new List<Vector2>();

            for (int i = 0; i < positions.Count; i++)
            {
                posiciones2.Add(new Vector2(positions[i].x, positions[i].y));
            }

            for (int i = positions.Count - 1; i > 0; i--) // Esta parte permite colliders concavos
            {
                posiciones2.Add(new Vector2(positions[i].x, positions[i].y + colliderThickness));
            }

            PolygonCollider2D col = r.gameObject.AddComponent<PolygonCollider2D>();
            col.isTrigger = true;
            col.points = posiciones2.ToArray();
            col.gameObject.tag = "Border";


            //col.gameObject.AddComponent<Rigidbody2D>();
        }
    }

}
