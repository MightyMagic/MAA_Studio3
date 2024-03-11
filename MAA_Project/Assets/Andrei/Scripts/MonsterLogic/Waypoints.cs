using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Waypoint 
{
    public Transform waypoint;
    public bool usedInPatrol;

    public int chunkNumber;
}

[System.Serializable]
public class ChunkWaypoints
{
    public List<Waypoint> waypoints;
    public int chunkNumber;
}
