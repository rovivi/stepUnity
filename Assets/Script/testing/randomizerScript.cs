using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomizerScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject ObjectToGenerate;


    void Start()
    {
        ///InvokeRepeating("Generate", 0.01f, 1);
    }

    // Update is called once per frame
    void Update()
    {

        Generate();
        
    }


    public void Generate()
    {


        Vector3 PlaceToSpawn = new Vector3(Random.Range(-10, 10), Random.Range(-10, 10));

        Instantiate(ObjectToGenerate, PlaceToSpawn, Quaternion.identity);

    }
}
