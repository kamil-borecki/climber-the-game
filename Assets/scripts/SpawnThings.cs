using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnThings : MonoBehaviour
{
    //GameObject / Things
    public GameObject[] things;
    public List<float> paths;
    public List<List<GameObject>> items = new List<List<GameObject>>();
    public int pooledAmount = 10;




    ////time
    public float minTimeSpawning = 3.0f;
    public float maxTimeSpawning = 6.0f;

    public float thingMassFrom = 1f;
    public float thingMassTo = 1f;


    //// start Time
    void Start()
    {
        for(int i = 0; i < things.Length; i++)
        {
            List<GameObject> itemsInGroup = new List<GameObject>();
            for(int y = 0; y < pooledAmount; y++)
            {
                GameObject item = Instantiate(things[i], transform.position, Quaternion.identity);
                item.SetActive(false);
                item.tag = "endThing";
                itemsInGroup.Add(item);
            }
            items.Add(itemsInGroup);
        }
        StartCoroutine(Time());
    }

    ////random Time
    IEnumerator Time()
    {
            float waitTime = Random.Range(minTimeSpawning, maxTimeSpawning);
            yield return new WaitForSeconds(waitTime);
            SpawnRandom();
            StartCoroutine(Time());
    }


    //void Update()
    //{
      
    //}

    //// Randomize things and paths
    public void SpawnRandom()
    {
    //    //position
        
        int randomIndex = Random.Range(0, items.Count);
        Debug.Log(randomIndex);
        Debug.Log(items.Count);
        //float x = Random.Range(0, paths.Count);
        //float y = Camera.main.gameObject.transform.position.y + 7;
        for (int i = 0; i < items[randomIndex].Count; i++)
        {
            Debug.Log(i);
            if (!items[randomIndex][i].activeInHierarchy)
            {
                float x = Random.Range(0, paths.Count);
                float y = Camera.main.gameObject.transform.position.y + 7;
                GameObject obj = items[randomIndex][i];
                obj.transform.position = new Vector2(paths[(int)x], y);
                obj.GetComponent<Rigidbody2D>().mass = Random.Range(thingMassFrom, thingMassTo);
                obj.SetActive(true);
                break;
            }
        }


        //GameObject obj = Instantiate(things[randomIndex], new Vector2(paths[(int)x], y), Quaternion.identity);
        //obj.tag = "endThing";
        //obj.GetComponent<Rigidbody2D>().mass = Random.Range(thingMassFrom, thingMassTo);

    }
 
}