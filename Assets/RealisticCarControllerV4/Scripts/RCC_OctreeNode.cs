using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RCC_OctreeNode {

    public MeshFilter meshFilter;

    public Bounds bounds;

    [System.NonSerialized]
    public List<Vector3> vertices;

    [System.NonSerialized]
    public RCC_OctreeNode[] children;

    public bool IsLeaf => children == null;

    public RCC_OctreeNode(MeshFilter meshFilter) {

        this.meshFilter = meshFilter;
        this.bounds = meshFilter.mesh.bounds;
        this.bounds.center = meshFilter.mesh.bounds.center;

        vertices = new List<Vector3>();

    }

    public RCC_OctreeNode(Bounds bounds) {

        this.bounds = bounds;
        this.bounds.center = bounds.center;

        vertices = new List<Vector3>();

    }

}
