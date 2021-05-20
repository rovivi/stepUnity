using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomMvement : MonoBehaviour
{
    // Start is called before the first frame update
    private float angle;
    Renderer m_Renderer;
    public float speed = 0.00000002f; 
    void Start()
    {
        m_Renderer = GetComponent<Renderer>();
        angle = Random.Range(-360, System.DateTime.Now.Ticks %360);
    }
    // Update is called once per frame
    void Update()
    {
        var currentpos = gameObject.transform.position;
        transform.Translate(new Vector3( Mathf.Cos(angle) * speed,  Mathf.Sin(angle) * speed) );
        if (!m_Renderer.isVisible)
            Destroy(gameObject);
    }
}
