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

    void Update()
    {


        if ((Input.GetButtonDown("Horizontal")) || (Input.GetButtonDown("Vertical")))
        {

            StartCoroutine(draw());
        }




    }
    Vector3 newVertex;
    Vector3 lastVertex;
    IEnumerator draw()
    {


        LineRenderer r = new GameObject().AddComponent<LineRenderer>();
        r.transform.SetParent(transform);
        r.startWidth = 0.1f;
        r.endWidth = 0.1f;

        r.material.color = color;

        List<Vector3> posiciones = new List<Vector3>();


        while ((Input.GetButton("Horizontal")) || (Input.GetButton("Vertical")))
        {
            newVertex = player.transform.position + Vector3.forward * 2;
            if (Vector3.Distance(lastVertex, newVertex) >= vertexPrecision) //Esto mejora la calidad de la linea
            {
                posiciones.Add(newVertex);
                r.positionCount = posiciones.Count;
                r.SetPositions(posiciones.ToArray());
                lastVertex = newVertex;
            }
            yield return new WaitForEndOfFrame();
        }
        r.useWorldSpace = false;

        if (usePhysics)
        {
            List<Vector2> posiciones2 = new List<Vector2>();

            for (int i = 0; i < posiciones.Count; i++)
            {
                posiciones2.Add(new Vector2(posiciones[i].x, posiciones[i].y));
            }

            for (int i = posiciones.Count - 1; i > 0; i--) // Esta parte permite colliders concavos
            {
                posiciones2.Add(new Vector2(posiciones[i].x, posiciones[i].y + colliderThickness));
            }

            PolygonCollider2D col = r.gameObject.AddComponent<PolygonCollider2D>();
            col.points = posiciones2.ToArray();

            col.gameObject.AddComponent<Rigidbody2D>();
        }
    }
}
