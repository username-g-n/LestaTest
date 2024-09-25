using StarterAssets;
using UnityEngine;

public class WaveTrap : MonoBehaviour
{

    [SerializeField] public Vector3 direction;
    [SerializeField] private Vector3 start;
    private int rand;
    public Vector3 directionOfPlatform;
    
    void Start()
    {
        rand = Random.Range(-3, 3);
        start = new Vector3(rand, 0, 0);
        Debug.Log(Random.value);
        transform.position = transform.position + start;
        var array = new int[] { 1, -1 };
        rand = array[Random.Range(0, array.Length)]; ;
        direction = new Vector3(rand, 0, 0);
    }
    void Update()
    {
        
        if (transform.position.x > 3)
        {
            direction = new Vector3(-1, 0, 0);
        }

        else if (transform.position.x < -3)
        {
            direction = new Vector3(1, 0, 0);
        }  
        
        transform.Translate(direction * Time.deltaTime);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.GetComponent<ThirdPersonController>().MoveWithPlatform(direction);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.GetComponent<ThirdPersonController>().MoveWithNorm();
        }
    }
    /*
    private void Action()
    {

        elapsedTime += Time.deltaTime;
        float elapsedPercentage = elapsedTime / timeToWaypoint;
        elapsedPercentage = Mathf.SmoothStep(0, 1, elapsedPercentage);
        transform.position = Vector3.Lerp(previousWaypoint.position, targetWaypoint.position, elapsedPercentage);

        if (elapsedPercentage >= 1)
        {
            TargetNextWaypoint();
        }
    }
    private void TargetNextWaypoint()
    {
        previousWaypoint = waypoint.GetWaypoint(targetWaypointIndex);
        targetWaypointIndex = waypoint.GetNextWaypointIndex(targetWaypointIndex);
        targetWaypoint = waypoint.GetWaypoint(targetWaypointIndex);

        elapsedTime = 0;

        float distanceToWayPoint = Vector3.Distance(previousWaypoint.position, targetWaypoint.position);
        timeToWaypoint = distanceToWayPoint / speed;
    }

    */

}
