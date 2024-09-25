using System.Collections;
using Unity.VisualScripting;
using UnityEngine;


public class DoorTrap : MonoBehaviour
{
    [SerializeField] private bool notTouched = true;
    [SerializeField] private float chanceToPass = 0.4f;
    [SerializeField] private float timeForDeleteInSec = 2f;
    [SerializeField] private Color32 trapColor = new Color32(255, 0, 0, 255);
    [SerializeField] private int trapDamage = 5;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            var rand = (Random.value < chanceToPass) ? 0 : 1;
            Debug.Log(rand);
            if (notTouched == true)
            {
                if (rand == 1 && transform.parent.transform.parent.gameObject.GetComponent<CounterClass>().counter < 2)
                {
                    transform.parent.transform.parent.gameObject.GetComponent<CounterClass>().counter += 1;
                    other.GetComponent<Player>().TakeDamage(trapDamage);
                    transform.parent.Find("DoorLeft").gameObject.GetComponent<Renderer>().material.color = trapColor;
                    transform.parent.Find("DoorRight").gameObject.GetComponent<Renderer>().material.color = trapColor;
                    notTouched = false;
                }
                else
                {
                    transform.parent.transform.parent.gameObject.GetComponent<CounterClass>().counter = 0;
                    transform.parent.Find("DoorLeft").gameObject.layer = 3;
                    transform.parent.Find("DoorRight").gameObject.layer = 3;
                    transform.parent.Find("DoorLeft").gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                    transform.parent.Find("DoorRight").gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                    StartCoroutine("Delete");
                    this.GetComponent<BoxCollider>().enabled = false;
                }
            }
            else
                other.GetComponent<Player>().TakeDamage(trapDamage);
        }
    }
    IEnumerator Delete()
    {
        yield return new WaitForSeconds(1);
        Destroy(transform.parent.Find("DoorLeft").gameObject);
        Destroy(transform.parent.Find("DoorRight").gameObject);
    }
}
