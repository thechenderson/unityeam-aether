using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityManager : MonoBehaviour
{
    Vector3 gravitySelected;

    public void setGravity(Vector3 gravity)
    {
        gravitySelected = gravity;
        Physics.gravity = gravitySelected;
        Debug.Log("Gravity Set To: " + gravitySelected);
    }

    public Vector3 getGravity()
    {
    return gravitySelected;
    }
}
