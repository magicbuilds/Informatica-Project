using System.Collections.Generic;
using UnityEngine;

public class WaypointManager : MonoBehaviour
{
    public static WaypointManager Instance;

    public Dictionary<(int pathIndex, int waypointNumber), Waypoint> waypoints = new Dictionary<(int, int), Waypoint>();

    [SerializeField] private Waypoint waypointPrefab;
    private GameObject waypointContainer;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        waypointContainer = new GameObject();
        waypointContainer.name = "Waypoint Container";
    }

    public Waypoint AddNewWaypoint(Vector2 position, int pathIndex, int waypointNumber, Waypoint nextWaypoint)
    {
        Waypoint newWaypoint = Instantiate(waypointPrefab, position, Quaternion.identity);
        newWaypoint.nextWaypoint = nextWaypoint;
        newWaypoint.waypointIndex = pathIndex;
        
        waypoints[(pathIndex, waypointNumber)] = newWaypoint;

        newWaypoint.name = "Waypoint " + pathIndex + "-" + waypointNumber;
        newWaypoint.transform.parent = waypointContainer.transform;

        return newWaypoint;
    }
}
