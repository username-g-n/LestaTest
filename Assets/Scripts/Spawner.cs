using UnityEngine;

public class Spawner : MonoBehaviour
{
    private string namePref;
    private GameObject clone;
    public void OnSpawnPrefabPoint(GameObject choise, Vector3 position, Vector3 rotation)
    {
        Quaternion rotate = Quaternion.Euler(rotation);
        namePref = choise.name;
        clone = Instantiate(choise, position, rotate);
        clone.name = (namePref);
    }
   
}
