using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityUp : MonoBehaviour
{
    void OnCollisionEnter(Collision collisionInfo)
    {
        if (collisionInfo.collider.tag == "Player")
        {
            Vector3 gravityModifier = new Vector3(0f, 10f, 0f);
            Debug.Log("Collision Detected: Switching Gravity");
            FindObjectOfType<GravityManager>().setGravity(gravityModifier);
        }
    }
}
