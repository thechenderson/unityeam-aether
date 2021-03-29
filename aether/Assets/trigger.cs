using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine.UI;
using UnityEngine;

public class trigger : MonoBehaviour
{
    public int hitCount;
    public int toolCount;

    // Start is called before the first frame update
    void Start()
    {
        hitCount = 0;
        toolCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider col)
    {
        

        
        if (col.gameObject.CompareTag("Collectable"))
        {

            col.gameObject.SetActive(false);
            toolCount++;

        }

            }

}
