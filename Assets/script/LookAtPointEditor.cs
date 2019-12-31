using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(LookAtPoint))]
[CanEditMultipleObjects]
public class LookAtPointEditor : Editor
{
    public void OnSceneGUI()
    {
        var t = (target as LookAtPoint);

        t.changeValue( t.numVertices, t.firstNumVertices);

        t.pointDefinition(); // create a center point

        for (int i = 0; i < t.numVertices; i++) // create handles
        {
            t.BottomPoints[i] = Handles.PositionHandle(t.BottomPoints[i], Quaternion.identity);
        }


       // t.centerPoint =  Handles.PositionHandle(t.centerPoint, Quaternion.identity);


        for (int i = 0; i < t.numVertices -1; i++) // drow lines between hamdles
        {
            Handles.DrawLine(t.BottomPoints[i], t.BottomPoints[i +1]);
        }
        Handles.DrawLine(t.BottomPoints[0], t.BottomPoints[t.numVertices-1]);

        if (t.symmetry)
        {
            for (int i = 0; i < t.numVertices; i++) // equale up and down
            {
                t.highPoints[i] = t.BottomPoints[i] + new Vector3(0, t.hight, 0);
            }

        }

        t.getMesh1();

        for (int i = 0; i < t.numVertices - 1; i++) //  create walls
        {
           t.createWall1(t.highPoints[i], t.highPoints[i + 1], t.BottomPoints[i], t.BottomPoints[i + 1]);
        }
        t.createWall1(t.highPoints[t.numVertices -1], t.highPoints[0], t.BottomPoints[t.numVertices -1], t.BottomPoints[0]); //  create last wall

        for (int i = 0; i < t.numVertices - 1; i++) //  create triangles
        {
            t.createTriangle(t.centerPoint, t.highPoints[i], t.highPoints[i+1]);
        }
        t.createTriangle(t.centerPoint, t.highPoints[t.highPoints.Length - 1], t.highPoints[0]); //  create last triangle

        t.createMesh();

        t.vertices.Clear();
        //t.uvs.Clear();
        t.numOfVerticesOnRoof = 0;
    }
}

   