using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModifyGravityTrigger : MonoBehaviour
{
    public float gravityX;
    public float gravityY;
    public float gravityZ;
    
    void OnCollisionEnter(Collision collisionInfo)
    {
        if (collisionInfo.collider.tag == "Player")
        {
            Vector3 gravityModifier = new Vector3(gravityX, gravityY, gravityZ);
            Debug.Log("Collision Detected - gravity set to: " + gravityX + "," + gravityY + "," + gravityZ);
            FindObjectOfType<GravityManager>().setGravity(gravityModifier);
        }
    }
}
