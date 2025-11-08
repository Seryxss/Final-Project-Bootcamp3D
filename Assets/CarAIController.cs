using UnityEngine;

public class SimpleCarWaypoint : MonoBehaviour
{
    [Header("Waypoint Settings")]
    public Transform[] waypoints;
    public int currentWaypointIndex = 0;
    public bool loopWaypoints = true;
    public bool isMoving = false;
    public int speed = 10;
    public int rotationSpeed = 5;
    

    void Start()
    {
        transform.position = waypoints[0].position;
        transform.rotation = waypoints[0].rotation;
        StartMoving();
    }

    void StartMoving()
    {
        currentWaypointIndex = 0;
        isMoving = true;
    }

    void Update()
    {
        NavigateWaypoints();
    }

    void NavigateWaypoints()
    {
        if (!isMoving)
            return;



        if (currentWaypointIndex < waypoints.Length)
        {

            transform.position = Vector3.MoveTowards(transform.position, waypoints[currentWaypointIndex].position, speed * Time.deltaTime);

            var direction = transform.position - waypoints[currentWaypointIndex].position;
            var targetRotation = Quaternion.LookRotation(direction, Vector3.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);

            var distance = Vector3.Distance(transform.position, waypoints[currentWaypointIndex].position);
            if (distance < 0.1f)
            {
                currentWaypointIndex++;
                if (loopWaypoints && currentWaypointIndex >= waypoints.Length)
                {
                    if (loopWaypoints)
                    {
                        transform.position = waypoints[0].position;
                        transform.rotation = waypoints[0].rotation;
                        currentWaypointIndex = 0;
                    }
                    else
                    {
                        isMoving = false;
                    }
                }
            }

        }
        
    }
}