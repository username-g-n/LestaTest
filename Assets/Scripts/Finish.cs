using UnityEngine;

public class Finish : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<Player>().startTimer = false;
            other.GetComponent<Player>().EndGame(true);
            Destroy(this);
        }
    }
}
