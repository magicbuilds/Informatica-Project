using System.Collections.Generic;
using UnityEngine;

public class WaypointManager : MonoBehaviour
{
    public static WaypointManager Instance;

    public Dictionary<int, Waypoint> waypoints = new Dictionary<int, Waypoint>();

    [SerializeField] private Waypoint waypointPrefab;

    private void Awake()
    {
        Instance = this; 
    }

    public void AddNewWaypoint(Vector2 position)
    {
        Waypoint newWaypoint = Instantiate(waypointPrefab, position, Quaternion.identity);

        int currentWaypointNumber = waypoints.Count;
        waypoints[currentWaypointNumber] = newWaypoint;
        newWaypoint.name = "Waypoint " + currentWaypointNumber;
    }
}
