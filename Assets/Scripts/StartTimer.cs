using UnityEngine;

public class StartTimer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<Player>().startTimer = true;
            Destroy(this);
        }
    }
}
