using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Node : IHeapItem<Node>
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
    int heapIndex;

    public Node(bool walkble, Vector3 worldPos, int gridX, int gridY)
    {
        this.walkble = walkble;
        this.worldPosition = worldPos;
        this.gridX = gridX;
        this.gridY = gridY;
    }
    public int HeapIndex
    {
        get
        {
            return heapIndex;
        }
        set
        {
            heapIndex = value;
        }
    }
    public int CompareTo(Node nodeToCompare)
    {
        int compare = fCost.CompareTo(nodeToCompare.fCost);
        if (compare == 0)
        {
            compare = hCost.CompareTo(nodeToCompare.hCost);
        }
        return -compare;
    }
}
