using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeAndrei 
{
    public bool walkable;
    public Vector3 worldPosition;

    public int gCost;
    public int hCost;

    public int gridX;
    public int gridY;

    public NodeAndrei parent;

    public NodeAndrei(bool walkable, Vector3 worldPosition, int gridX, int gridY)
    {
        this.walkable = walkable;
        this.worldPosition = worldPosition;

        this.gridX = gridX;
        this.gridY = gridY;
    }

    public int fCost {  get { return gCost + hCost; } }
}
