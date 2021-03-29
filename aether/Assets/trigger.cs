using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine.UI;
using UnityEngine;

public class trigger : MonoBehaviour
{
    public int hitCount;
    public int toolCount;

    public Text count;
    public GameObject win;


    // Start is called before the first frame update
    void Start()
    {
        hitCount = 0;
        toolCount = 0;

        win.SetActive(false);
        count.text = (toolCount + "out of 3");
    }

    // Update is called once per frame
    void Update()
    {
        if (toolCount == 3) {
            
            win.SetActive(true);
        }
    }

    void OnTriggerEnter(Collider col)
    {
        

        
        if (col.gameObject.CompareTag("Collectable"))
        {

            col.gameObject.SetActive(false);
            toolCount++;
            count.text = (toolCount + " out of 3");

        }

            }

}
