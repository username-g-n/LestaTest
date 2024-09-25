using System.Collections;
using UnityEngine;

public class DamageTrap : MonoBehaviour
{
   
    [SerializeField] private Color32 trapColorStep = new Color32(255, 155, 0, 255);
    [SerializeField] private Color32 trapColorBoom = new Color32(255, 0, 0, 255);
    [SerializeField] private Color32 trapColorNormal = new Color32(105, 250, 136, 255);
    [SerializeField] private float timeToActivateTrapSec = 1;
    [SerializeField] private float timeDealingDamageSec = 0.2f;
    [SerializeField] private float trapCooldownSec = 5;
    [SerializeField] public bool trapWorking = false;
    [SerializeField] private int trapDamage = 20;
    [SerializeField] private bool trapDamageDisactive = true;

    // Start is called before the first frame update


    private void OnTriggerStay(Collider other)
    {

        if (other.gameObject.tag == "Player")
        {
            if (trapWorking == false)
            {
                Renderer myRenderer = GetComponent<Renderer>();
                StartCoroutine(Step(timeToActivateTrapSec, myRenderer));
                trapWorking = true;
            }

            else if (trapDamageDisactive == false)
            {
                other.GetComponent<Player>().TakeDamage(trapDamage);
            }
        }
            
    }
    IEnumerator Step(float timeInSec, Renderer render)
    {
        render.material.color = trapColorStep;
        yield return new WaitForSeconds(timeInSec);
        StartCoroutine(Red(timeDealingDamageSec, render));
    }
    IEnumerator Red(float timeInSec, Renderer render)
    {
        render.material.color = trapColorBoom;
        trapDamageDisactive = false;
        yield return new WaitForSeconds(timeInSec);
        trapDamageDisactive = true;
        render.material.color = trapColorNormal;
        StartCoroutine(Countdown(trapCooldownSec, render));
    }
    IEnumerator Countdown(float timeInSec, Renderer render)
    {
        yield return new WaitForSeconds(timeInSec);
        trapWorking = false;
        Debug.Log("ready");
    }

}
