using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityManager : MonoBehaviour
{
    public float gravity;

    void Start()
    {
        Physics.gravity = new Vector3(0f, -9.8f, 0f);
    }

    void updateGravity(Vector3 gravity)
    {
        Vector3 gravitySelected = gravity;
        Physics.gravity = new Vector3(0f, -0f, 10f);
        Debug.Log("Gravity Set To: " + gravitySelected);
    }
}
