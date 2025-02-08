using UnityEngine;
using System.Collections.Generic;

public class MeshGenerator : MonoBehaviour
{

    public Material mat;
    private float damage;


    public float Damage => damage;
    //private List<Vector3> polygonPoints;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void GenerateMesh(List<Vector3> polygonPoints, bool regularPolygon)
    {
        // polygonPoints = new List<Vector3> {
        // new Vector3(0, 0.01f, 0),   // Point 1
        // new Vector3(1, 0, 0),   // Point 2
        // new Vector3(1, 0, 1),   // Point 3
        // new Vector3(0, 0, 1),    // Point 4 (for a rectangle)
        // new Vector3(-0.5f, 0, 0.5f)  // Point 5
        // };

        Mesh polygonMesh = CreatePolygonMesh(polygonPoints, regularPolygon);



        MeshFilter meshFilter = gameObject.AddComponent<MeshFilter>();
        meshFilter.mesh = polygonMesh;


        MeshCollider meshCollider = gameObject.AddComponent<MeshCollider>();
        meshCollider.sharedMesh = polygonMesh;
        meshCollider.convex = true;
        meshCollider.isTrigger = true;

        MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer>();
        meshRenderer.material = mat;
        //meshRenderer.material.SetInt("_Cull", (int)UnityEngine.Rendering.CullMode.Off);
        //polygonMesh.RecalculateNormals();

        // lineRenderer = gameObject.AddComponent<LineRenderer>();
        // lineRenderer.positionCount = polygonPoints.Count + 1;
        // lineRenderer.startWidth = 0.05f;
        // lineRenderer.endWidth = 0.05f;
        // lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        // lineRenderer.startColor = Color.green;
        // lineRenderer.endColor = Color.green;
        // UpdateLineRenderer();

    }



    // Update is called once per frame
    void Update()
    {
        
    }


    private Mesh CreatePolygonMesh(List<Vector3> points, bool regularPolygon){
        
        //needs at least 3 points to work
        if (points.Count < 3){
            return null;
        }



        Mesh mesh = new Mesh();


        List<Vector3> vertices = new List<Vector3>();
        List<int> triangles = new List<int>();

        vertices.AddRange(points);

    

        //create triangles from vertex 0 and the next two vertices
        for (int i = 0; i < points.Count - 2; i++){
            triangles.Add(0);
            triangles.Add(i + 1);
            triangles.Add(i + 2);
        }

        //last polygon  to close it.
        if (regularPolygon){
            triangles.Add(0);
            triangles.Add(points.Count - 1);
            triangles.Add(1);
        }


        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();

        mesh.RecalculateNormals();


        // Vector3[] normals = new Vector3[vertices.Count];
        // for (int i = 0; i < vertices.Count; i++)
        // {
        //     normals[i] = Vector3.up;  // Set the normal to point upwards (Y direction)
        // }
        // mesh.normals = normals;

        

        return mesh;

    }


    public void Reset(){
        
        Destroy(this.GetComponent<MeshFilter>());
        Destroy(this.GetComponent<MeshCollider>());
        Destroy(this.GetComponent<MeshRenderer>());
    }

    public void SetDamage(float newDamage){
        damage = newDamage;
    }
}
