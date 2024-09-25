using StarterAssets;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;
public class WindTrap : MonoBehaviour
{

    public bool wasHurted = true;
    [SerializeField] private CharacterController controller;
    [SerializeField] private float windStrength = 2;

    [SerializeField] private Vector2 windDirect2;
    [SerializeField] private Vector3 windDirect3;
    [SerializeField] private int changeWindCooldown;

    private void Start()
    {
        changeWindCooldown = 2;
        StartCoroutine("Step");
        Debug.Log(windDirect3);

    }
    IEnumerator Step()
    {

        windDirect2 = Random.insideUnitCircle.normalized;
        windDirect3 = new Vector3(windDirect2.x, 0f, windDirect2.y);
        yield return new WaitForSeconds(changeWindCooldown);
        Back();
    }
    private void Back()
    {
        StartCoroutine("Step");
    }

    private void OnTriggerStay(Collider other)
    {
        Debug.Log(windDirect3);
        if (other.gameObject.tag == "Player")
        {
            other.GetComponent<ThirdPersonController>().MoveWithWind(windDirect3, windStrength);
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.GetComponent<ThirdPersonController>().MoveWithNorm();
        }
    }


}
