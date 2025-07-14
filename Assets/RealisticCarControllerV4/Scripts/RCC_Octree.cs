using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RCC_Octree {

    public RCC_OctreeNode root;

    private readonly int maxDepth = 20;
    private readonly int maxVerticesPerNode = 5000;

    public RCC_Octree(MeshFilter meshFilter) {

        root = new RCC_OctreeNode(meshFilter);

    }

    public void Insert(Vector3 vertex) {

        Insert(root, vertex, 0);

    }

    private void Insert(RCC_OctreeNode node, Vector3 vertex, int depth) {

        if (node.IsLeaf) {

            node.vertices.Add(vertex);

            if (node.vertices.Count > maxVerticesPerNode && depth < maxDepth) {

                Subdivide(node);
                List<Vector3> verticesToReinsert = new List<Vector3>(node.vertices);
                node.vertices.Clear();

                foreach (Vector3 v in verticesToReinsert)
                    Insert(node, v, depth);

            }

        } else {

            // Insert into the appropriate child node

            foreach (var child in node.children) {

                if (child.bounds.Contains(vertex)) {

                    Insert(child, vertex, depth + 1);
                    break;

                }

            }

        }

    }

    private void Subdivide(RCC_OctreeNode node) {

        node.children = new RCC_OctreeNode[8];
        Vector3 size = node.bounds.size / 2f;
        Vector3 center = node.bounds.center;

        // Create 8 children nodes (split the current bounds into 8 smaller bounds)
        for (int i = 0; i < 8; i++) {

            Vector3 newCenter = center + new Vector3(size.x * ((i & 1) == 0 ? -0.5f : 0.5f),
                                                     size.y * ((i & 2) == 0 ? -0.5f : 0.5f),
                                                     size.z * ((i & 4) == 0 ? -0.5f : 0.5f));
            node.children[i] = new RCC_OctreeNode(new Bounds(newCenter, size));

        }

    }

    public Vector3 FindNearestVertex(Vector3 point, MeshFilter meshFilter) {

        return FindNearestVertex(root, point, meshFilter);

    }

    private Vector3 FindNearestVertex(RCC_OctreeNode node, Vector3 point, MeshFilter meshFilter) {

        float minDistSqr = Mathf.Infinity;
        Vector3 bestVertex = Vector3.zero;

        if (node.IsLeaf) {

            foreach (var vertex in node.vertices) {

                float distSqr = (vertex - point).sqrMagnitude;

                if (distSqr < minDistSqr) {

                    minDistSqr = distSqr;
                    bestVertex = vertex;

                }

            }

        } else {

            foreach (var child in node.children) {

                if (child != null || child.bounds.SqrDistance(point) < minDistSqr) {

                    foreach (var vertex in child.vertices) {

                        float distSqr = (vertex - point).sqrMagnitude;

                        if (distSqr < minDistSqr) {

                            minDistSqr = distSqr;
                            bestVertex = vertex;

                        }

                    }

                }

            }

        }

        return bestVertex;

    }

}

