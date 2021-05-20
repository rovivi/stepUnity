using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameplayController : MonoBehaviour
{

    public GameObject prefabStep0;
    public GameObject prefabStep1;
    public GameObject prefabStep2;
    public GameObject prefabStep3;
    public GameObject prefabStep4;

    double currentBPM = 180;
    double currentSecond = 0;
    double currentBeat = 0;
    int speed = 5;

    List<GameObject> arrowsPrefbs = new List<GameObject>();

    List<GameObject> generatedObjects = new List<GameObject>();


    float[] positions= new float[] {-1,-0.5f,0,0.5f,1};
    void Start()
    {

        arrowsPrefbs.Add(prefabStep0);
        arrowsPrefbs.Add(prefabStep1);
        arrowsPrefbs.Add(prefabStep2);
        arrowsPrefbs.Add(prefabStep3);
        arrowsPrefbs.Add(prefabStep4);
        

        Debug.LogError("Voya rendereqaars");
      
        Debug.LogError("pues ya rendereee segun ");

        try
        {
            StreamReader reader = new StreamReader(@"G:\JUEGOS\STEP_F2_V1_15 (Steppack 2.15)\Songs\16-FIESTA 2\(1) 1303 - Passacaglia\1303 - Passacaglia.ssc");
            var fileInfo = reader.ReadToEnd();
            reader.Close();

            var steps = SSCParser. ParseFile(fileInfo);
            var notes =steps.Levels[5].Notes;
            
            foreach (var rowNote in notes) {
                var count = 0;
                foreach (var stepsss in rowNote.Arrows) {
                    if (stepsss.Type != NoteTypes.NOTE_EMPTY)
                    {
                    Vector3 PlaceToSpawn = new Vector3(positions[count%5], -(float)(rowNote.CurrentBeat* speed), -1);
                    var w = Instantiate(arrowsPrefbs[count%5], PlaceToSpawn, Quaternion.identity);
                    }
                    count++;
                }
            }
        }
        catch (Exception ex)
        {
            Debug.LogError(ex.Message);
        }
        //readFile
    }

    // Update is called once per frame
    void Update()
    {
        //se calculan los tiempos 
        currentSecond += Time.deltaTime;
        currentBeat += Time.deltaTime / (60 / currentBPM);
        //            Debug.LogError(currentBeat);

        foreach (var obj in generatedObjects)
        {
            Destroy(obj);
        }

        //foreach (var pos in positions)
        //{
        //    Vector3 PlaceToSpawn = new Vector3(pos, 0, -1);
        //    var w = Instantiate(prefabStep, PlaceToSpawn, Quaternion.identity);
        //    generatedObjects.Add(w);
        //}

    }
}
