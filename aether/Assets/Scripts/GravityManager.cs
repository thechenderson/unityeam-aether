using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityManager : MonoBehaviour
{
    public void updateGravity(Vector3 gravity)
    {
        Vector3 gravitySelected = gravity;
        Physics.gravity = gravitySelected;
        Debug.Log("Gravity Set To: " + gravitySelected);
    }
}
