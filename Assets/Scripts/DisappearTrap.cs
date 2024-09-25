using System.Collections;
using UnityEngine;


public class DisappearTrap : MonoBehaviour
{
    //[SerializeField] private int changeAppearCooldown = 2;
    [SerializeField] public bool trapNotWorking = true;
    [SerializeField] public int counter;
    private GameObject parent;
    private Renderer rend;
    private Collider colliders;
    void Start()
    {
        parent = gameObject;
        rend = parent.GetComponent<Renderer>();
        colliders = parent.GetComponent<Collider>();
        counter = 0;
    }

    private void OnTriggerStay(Collider other)
    {
  
        if (trapNotWorking == true) { 
            rend.enabled = false;
            colliders.enabled = false;
            trapNotWorking = false;
            StartCoroutine("Appear");
        }
    }


    IEnumerator Appear()
    {
        yield return new WaitForSeconds(2);
        rend.enabled = true;
        colliders.enabled = true;
        trapNotWorking = true;
        Debug.Log("after");

    }
}
