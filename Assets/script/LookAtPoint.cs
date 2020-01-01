using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[ExecuteInEditMode]
public class LookAtPoint : MonoBehaviour
{
    [Tooltip("number of angales")]
    public int numVertices = 3; 
    [HideInInspector]
    public int firstNumVertices = 3; // varuable to chake if was change in "numVertices"
    [Tooltip("num that created mesh the roof from him, usely enter same num that 'high'")]
    public int heightOfTheCenterPointOfTheRoof = 3;
    [Tooltip("height of the object")]
    public float hight = 3; 
   // [HideInInspector]
    public Vector3[] BottomPoints = new Vector3[3]; //array that inclouded the vector 3 botton points of the object
    [HideInInspector]
    public Vector3[] highPoints = new Vector3[3]; //array that inclouded the vector 3 high points of the object
    [HideInInspector]
    public Vector3 centerPoint = new Vector3(0,0,0); // point that from it created a mesh to roof
    [HideInInspector]
    public int numOfVerticesOnRoof = 0;
    public bool symmetry = true;
    Mesh mesh;
    [HideInInspector]
     public List<Vector3> vertices = new List<Vector3>();


    //public List<Vector2> uvs = new List<Vector2>();
    public void Reset() //this funtion is calles when reseting the script, and reset the array to size defult
    {
        BottomPoints = new Vector3[numVertices];
        highPoints = new Vector3[numVertices];
    }


    public void changeValue(int newSum , int firitSum) // this funtion is chakes if was change in size the array 
    {
        if (newSum != firitSum)
        {
            firstNumVertices = numVertices;
            BottomPoints = new Vector3[numVertices];
            highPoints = new Vector3[numVertices];
        }
    }

    public void getMesh1()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

    }

    public void pointDefinition() //this function is called every frame and update the senter point
    {
        centerPoint = highPoints[0] + highPoints[highPoints.Length / 2] * 0.5f;

        centerPoint.y = heightOfTheCenterPointOfTheRoof;
    }

    public void createWall1(Vector3 highR, Vector3 highL, Vector3 bottonR, Vector3 bottonL)
    {       // this function is adding to list "vertices" a points of the wall, to send them to mesh in other function
        vertices.Add(highL); vertices.Add(highR); vertices.Add(bottonL); vertices.Add(bottonL); vertices.Add(highR); vertices.Add(bottonR);
    }

    public void createTriangle(Vector3 center, Vector3 Edge1, Vector3 Edge2)
    {       // this function is adding to list "vertices" a points of the triangle, to send them to mesh in other function
        vertices.Add(center); vertices.Add(Edge1); vertices.Add(Edge2);
        numOfVerticesOnRoof += 3;
    }

    public void createMesh()
    {       // this function is creates a new  list "triangles" that contintion a cirial int numbers to mesh and Initializing the mesh 
        int[] triangles = new int[vertices.Count];

        for (int i = 0; i < vertices.Count; i++)
        {
            triangles[i] = i;
        }


        Vector2[] uvs = new Vector2[vertices.Count];
        //Debug.Log(numOfVerticesOnRoof);
        for (int i = 0; i < uvs.Length - (numOfVerticesOnRoof); i+=3)
        {
            uvs[i] = new Vector2(vertices[i].x, vertices[i].y);

            if (Math.Abs(Math.Abs(vertices[i].x) -Math.Abs(vertices[i].x) + Math.Abs(vertices[i + 1].x) - Math.Abs(vertices[i].x)) >= (Math.Abs(Math.Abs(vertices[i].z) -Math.Abs(vertices[i].z) + Math.Abs(vertices[i + 1].z - Math.Abs(vertices[i].z)))))
            {
                uvs[i] = new Vector2(vertices[i].x, vertices[i].y);
                uvs[i +1] = new Vector2(vertices[i+1].x, vertices[i+1].y);
                uvs[i +2] = new Vector2(vertices[i +2].x, vertices[i+2].y);

            }
            else if (Math.Abs(Math.Abs(vertices[i].x) -  Math.Abs(vertices[i + 1].x) + Math.Abs(vertices[i + 1].x) -  Math.Abs(vertices[i + 1].x)) <= (Math.Abs(Math.Abs(vertices[i].z) -Math.Abs(vertices[i].z) + Math.Abs(vertices[i + 1].z) - Math.Abs(vertices[i].z))))//|| vertices[i].x == vertices[i+1].x)
            {
                uvs[i] = new Vector2(vertices[i].z, vertices[i].y);
                uvs[i + 1] = new Vector2(vertices[i + 1].z, vertices[i + 1].y);
                uvs[i + 2] = new Vector2(vertices[i + 2].z, vertices[i + 2].y);
            }

        }

        mesh.Clear();
        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles;
        mesh.uv = uvs;
        mesh.RecalculateNormals();
    }
}



//uvs.Length / numVertices - numVertices



            //if (vertices[i].x == vertices[i + 1].x && vertices[i + 1].x == vertices[i + 2].x)  
            //{
            //    uvs[i] = new Vector2(vertices[i].z, vertices[i].y);
            //    uvs[i + 1] = new Vector2(vertices[i + 1].z, vertices[i + 1].y);
            //    uvs[i + 2] = new Vector2(vertices[i + 2].z, vertices[i + 2].y);
         

            //}




