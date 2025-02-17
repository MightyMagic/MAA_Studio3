using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridX : MonoBehaviour
{
    Node[,] grid;
    public Vector2 gridWorldSize;
    public float nodeRadius;
    public LayerMask unwalkbleMask;

    float nodeDiameter;
    int gridSizeX;
    int gridSizeY;
    private void Start()
    {
        nodeDiameter = nodeRadius * 2;
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x/nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y/nodeDiameter);
        CreateGrid();
    }
    public int MaxSize
    {
        get
        {
            return gridSizeX * gridSizeY;
        }
    }
    public void CreateGrid()
    {
        //Debug.LogError("Creating grid!");
        print(5);

        grid = new Node[gridSizeX, gridSizeY];
        Vector3 worldBottomleft = transform.position -
        Vector3.right * gridWorldSize.x/2 -
        Vector3.forward * gridWorldSize.y/2;

        for(int x = 0; x < gridSizeX; x++)
        {
            for(int y = 0; y < gridSizeY; y++)
            {
                Vector3 worldPoint = worldBottomleft + Vector3.right * (x * nodeDiameter + nodeRadius)+
                Vector3.forward * (y * nodeDiameter + nodeRadius);
                bool walkable = !(Physics.CheckSphere(worldPoint, nodeRadius,unwalkbleMask));
                grid[x,y] = new Node(walkable, worldPoint,x,y);
            }
        }
        print(6);
    }

    public List<Node> GetNeighbours(Node node)
    {
        List<Node> neighbours = new List<Node>();

        for(int x = -1; x <= 1; x++)
        {
            for(int y = -1; y <= 1; y++)
            {
                if(x == 0 && y == 0)
                {
                    continue;
                }
                int checkX = node.gridX + x;
                int checkY = node.gridY + y;
                if(checkX >= 0 && checkX < gridSizeX &&  checkY >= 0 && checkY < gridSizeY)
                {
                    neighbours.Add(grid[checkX,checkY]);
                }
            }
        }
        return neighbours;
    }
    public Node NodeFromWorldPoint(Vector3 worldPostion)
    {
        float percentX = worldPostion.x  / gridWorldSize.x + 0.5f;
        float percentY = worldPostion.z  / gridWorldSize.y + 0.5f;
        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
        int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);
        return grid[x,y];
    }

    public List<Node> path;
   private void OnDrawGizmos()
   {
       Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x,0.1f,gridWorldSize.y));
   
       if(grid != null)
       {
           foreach (Node n in grid)
           {
                Gizmos.color = (n.walkble) ? Color.white : Color.red;

                if (!n.walkble)
                {
                    Gizmos.color = Color.red;
                    Gizmos.DrawCube(n.worldPosition, Vector3.one * (nodeDiameter - .1f));
                }


                if(path != null)
                {
                    if(path.Contains(n))
                    {
                        Gizmos.color = Color.black;
                    }
                }
               // Gizmos.DrawCube(n.worldPosition, Vector3.one * (nodeDiameter - .9f));
           }
       }
   }
}
