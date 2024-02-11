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

    public void AddNewWaypoint(Vector2 position, int pathIndex, int waypointNumber)
    {
        Waypoint newWaypoint = Instantiate(waypointPrefab, position, Quaternion.identity);

        
        (int, int) currentWaypoint = (pathIndex, waypointNumber);
        waypoints[currentWaypoint] = newWaypoint;

        newWaypoint.name = "Waypoint " + currentWaypoint.Item1 + "-" + currentWaypoint.Item2;
        newWaypoint.transform.parent = waypointContainer.transform;
    }
}
