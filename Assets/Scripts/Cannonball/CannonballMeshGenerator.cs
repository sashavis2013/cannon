using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CannonballMeshGenerator : MonoBehaviour
{
    private Mesh _mesh;
    private Vector3[] _vertices;
    private int[] _triangles;
    private Vector3[] _normals;
    private Vector2[] _uv;
    [SerializeField] private Material meshMaterial;
    private MeshRenderer _meshRenderer;

    private readonly float _maxDeviation = 0.05f;
    [SerializeField] private float minSize=0.2f;
    [SerializeField] private float maxSize=0.6f;

    private void Awake()
    {
        _meshRenderer = gameObject.GetComponent<MeshRenderer>();
    }

    void Start()
    {
        _mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = _mesh;
        CreateRandomizedBullet();
    }

    void GenerateMesh()
    {
        
        _mesh.Clear();
        _vertices = new Vector3[8];

        // Randomize the size of the cube
        float size = Random.Range(minSize, maxSize);

        // Define the vertices of a cube
        float halfSize = size / 2f;
        _vertices[0] = new Vector3(-halfSize, -halfSize, -halfSize);
        _vertices[1] = new Vector3(halfSize, -halfSize, -halfSize);
        _vertices[2] = new Vector3(-halfSize, halfSize, -halfSize);
        _vertices[3] = new Vector3(halfSize, halfSize, -halfSize);
        _vertices[4] = new Vector3(-halfSize, -halfSize, halfSize);
        _vertices[5] = new Vector3(halfSize, -halfSize, halfSize);
        _vertices[6] = new Vector3(-halfSize, halfSize, halfSize);
        _vertices[7] = new Vector3(halfSize, halfSize, halfSize);
        
        for (int i = 0; i < _vertices.Length; i++)
        {
            _vertices[i] += new Vector3(
                Random.Range(-_maxDeviation, _maxDeviation),
                Random.Range(-_maxDeviation, _maxDeviation),
                Random.Range(-_maxDeviation, _maxDeviation)
            );
        }


        // Generate triangles, normals, and UV coordinates (you may need to adjust this based on your needs)
        _triangles = new int[]
        {
            0, 2, 1,  // Front face
            1, 2, 3,
            4, 5, 6,  // Back face
            5, 7, 6,
            0, 1, 5,  // Top face
            0, 5, 4,
            2, 6, 7,  // Bottom face
            2, 7, 3,
            0, 4, 6,  // Left face
            0, 6, 2,
            1, 3, 7,  // Right face
            1, 7, 5
        };

        _normals = new Vector3[_vertices.Length];
        _uv = new Vector2[_vertices.Length];

        for (int i = 0; i < _vertices.Length; i++)
        {
            _normals[i] = Random.insideUnitSphere.normalized; // Randomize normals
            _uv[i] = new Vector2(Random.value, Random.value); // Randomize UV coordinates
        }

        _mesh.vertices = _vertices;
        _mesh.triangles = _triangles;
        _mesh.normals = _normals;
        _mesh.uv = _uv;
        _meshRenderer.material = meshMaterial;
    }

    public void CreateRandomizedBullet()
    {
        GenerateMesh();
    }
}
