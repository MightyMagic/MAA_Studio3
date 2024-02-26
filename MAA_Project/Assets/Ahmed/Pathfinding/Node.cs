using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public int gCost;
    public int hCost;
    public int fCost
    {
        get
        {
            return gCost + hCost;
        }
    }
    public Node parent;
    public bool walkble;
    public Vector3 worldPosition;
    public int gridX;
    public int gridY;

    public Node(bool walkble, Vector3 worldPos, int gridX, int gridY)
    {
        this.walkble = walkble;
        this.worldPosition = worldPos;
        this.gridX = gridX;
        this.gridY = gridY;
    }

}
