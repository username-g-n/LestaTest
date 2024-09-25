using UnityEngine;

public class TeleportTrap : MonoBehaviour
{
    private Vector3 teleportPoint;
    private void Start()
    {
        teleportPoint = new Vector3(0, 3, -3);
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("enter");
            Debug.Log("teleportPoint");
            other.GetComponent<CharacterController>().enabled = false;
            other.transform.position = other.transform.position + teleportPoint;
            other.GetComponent<CharacterController>().enabled = true;
        }

    }



}
