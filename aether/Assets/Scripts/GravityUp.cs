using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityUp : MonoBehaviour
{
    Vector3 gravityModifier = new Vector3(0f, 1f, 0f);
    void OnCollisionEnter(Collision collisionInfo)
    {
        if (collisionInfo.collider.tag == "Player")
        {
            Debug.Log("Collision Detected: Switching Gravity");
            FindObjectOfType<GravityManager>().updateGravity(gravityModifier);
        }
    }
}
