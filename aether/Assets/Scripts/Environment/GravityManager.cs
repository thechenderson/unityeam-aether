using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityManager : MonoBehaviour
{
    Vector3 gravitySelected;
    Vector3 defaultGravity = new Vector3(0f, -9.81f, 0f);

    void Awake() {
        setGravity(defaultGravity);
    }

    public void setGravity(Vector3 gravity)
    {
        gravitySelected = gravity;
        Physics.gravity = gravitySelected;
    }

    public Vector3 getGravity()
    {
    return gravitySelected;
    }

    private IEnumerator loopGravity(Vector3 gravity)
    {
        setGravity(gravity);
        yield return new WaitForSeconds(2);
    }
}
