
using System.Collections.Generic;
using UnityEngine;
using static Cinemachine.DocumentationSortingAttribute;

public class LocationGenerator : MonoBehaviour
{

    public List<GameObject> knownTraps;
    private Vector3 rotation;
    private int rand;
    
    private int nextStartPoint = 9;
    private int[][] arrayBot;
    private int[][] arrayMidTop;
    private int[] arrayRaws = new int[] { -3, 0, 3 };
    private int[] arrayLevels = new int[] { 20, 30, 40 , 10};
    private int trapsType;
    private int length;
    private float chanceToGetWay = 0.5f;

    private bool isLast = false;
    private bool nextNextStartPoint = false;
    private void Start()
    {
       
        knownTraps = new List<GameObject>(Resources.LoadAll<GameObject>("Traps"));
        rotation = new Vector3(-90, 0, 0);

        int[] list0 = new int[1] { 4 };
        int[] list1 = new int[1] { 5 };
        int[] list2 = new int[2] { 0, 1};
        int[] list3 = new int[2] { 0, 2 };
        int[] list4 = new int[2] { 1, 2 };
        int[] list5 = new int[3] { 0, 1, 2 };
        int[] list6 = new int[3] { 0, 2, 3 };
        int[] list7 = new int[3] { 0, 1, 3 };
        int[] list8 = new int[3] { 1, 2, 3 };
        int[] list9 = new int[4] { 0, 1, 2, 3 };

        arrayBot = new int[][] { list0, list1, list2, list3, list4, list5 };
        arrayMidTop = new int[][] { list0, list1, list2, list3, list4, list5, list6, list7, list8, list9 };
       
        
        
       
        trapsType = Random.Range(0, 6);
        ChooseGeneraionType(arrayBot, trapsType, 0, 8);
        trapsType = Random.Range(0, 10);
        nextNextStartPoint = true;
        ChooseGeneraionType(arrayMidTop, trapsType, 1, 8);
        
        
        Generator();
        isLast = true;
       
        ChooseGeneraionType(arrayBot, 0, 0, 8);
        ChooseGeneraionType(arrayMidTop, 0, 1, 8);
        ChooseGeneraionType(arrayMidTop, 0, 2, 8);

     
        nextStartPoint = 8 * 3 + nextStartPoint;
        GetComponent<Spawner>().OnSpawnPrefabPoint(knownTraps[7], new Vector3(0, 30, 50 + nextStartPoint), rotation);
        GetComponent<Spawner>().OnSpawnPrefabPoint(knownTraps[7], new Vector3(0, 50, 50 + nextStartPoint), rotation);
      
        GenerateWay(arrayBot[4], 3, 16, nextStartPoint, 3);
        GetComponent<Spawner>().OnSpawnPrefabPoint(knownTraps[8], new Vector3(0, 9, 17*3+ nextStartPoint), new Vector3(0,0,0));


    }


    
   private void Generator()
   {
        
       
        var arrays = new int[] { 6, 8, 10 };
        length = arrays[Random.Range(0, arrays.Length)];
       

        //количество участков препятствий
        for (int i = 0; i < 4; i++)
       {
            length = arrays[Random.Range(0, arrays.Length)];
            
               //уровень высоты на котором находятся препятствия
               for (int height = 0; height < 3; height++)
               {
                   if (height == 0)
                   {
                       trapsType = Random.Range(0, 6);
                       ChooseGeneraionType(arrayBot,trapsType, height, length);
                   }
                   else
                   {
                       trapsType = Random.Range(0, 10);
                       if (height == 2)
                       {
                           nextNextStartPoint = true;
                       }
                       ChooseGeneraionType(arrayMidTop, trapsType, height, length);  
                   }
               }
           
       }
   }

   private void ChooseGeneraionType(int[][] array, int trapsType, int height, int numberOfTiles)
   {
       if (trapsType == 0)
       {
           GenerateWay(array[trapsType], height, numberOfTiles, nextStartPoint, 3);
       }
       else if (trapsType == 1)
       {
           GenerateWay(array[trapsType], height, numberOfTiles / 2, nextStartPoint, 6);
       }
       else if (trapsType > 5)
       {
           Generate(array[trapsType], height, numberOfTiles, nextStartPoint, 3);
       }
       else if ((trapsType > 1) && (trapsType < 6))
       {
            var rand = (Random.value < chanceToGetWay) ? 0 : 1;
            if (rand == 1)
            {
                Generate(array[trapsType], height, numberOfTiles, nextStartPoint, 3);
            }
            else
            {
                GenerateWay(array[trapsType], height, numberOfTiles, nextStartPoint, 3);
            }
        }

   }
   
    private void GenerateWay(int[] traps, int level, int numberOfTiles, int startPoint, int tileLengthMultiplier)
    {
        for (int i = 0; i < numberOfTiles; i++)
        {
           
            rand = traps[Random.Range(0, traps.Length)];
            GetComponent<Spawner>().OnSpawnPrefabPoint(knownTraps[rand], new Vector3(0, arrayLevels[level], i * tileLengthMultiplier + startPoint), rotation);
        }
        if (isLast == false)
        {
            GetComponent<Spawner>().OnSpawnPrefabPoint(knownTraps[6], new Vector3(0, arrayLevels[level], numberOfTiles * tileLengthMultiplier + startPoint), rotation);
            if (nextNextStartPoint == true)
            {
                nextStartPoint = numberOfTiles * tileLengthMultiplier + startPoint + 3;
                nextNextStartPoint = false;
            }
        }
       
    }
    private void Generate(int[] traps, int level, int numberOfTiles, int startPoint, int tileLengthMultiplier)
    {
        
        for (int i = 0; i < numberOfTiles; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                rand = traps[Random.Range(0, traps.Length)];
                GetComponent<Spawner>().OnSpawnPrefabPoint(knownTraps[rand], new Vector3(arrayRaws[j], arrayLevels[level], i * tileLengthMultiplier + startPoint), rotation);

            }

        }
        if (isLast == false)
        {
            GetComponent<Spawner>().OnSpawnPrefabPoint(knownTraps[6], new Vector3(0, arrayLevels[level], numberOfTiles * tileLengthMultiplier + startPoint), rotation);
            if (nextNextStartPoint == true)
            {
                nextStartPoint = numberOfTiles * tileLengthMultiplier + startPoint + 3;
                nextNextStartPoint = false;
            }
        }
        
    }

}
