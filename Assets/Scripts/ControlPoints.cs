using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPoints : MonoBehaviour
{
    public float x;
    public float y;
    public float z;


    ControlPoints(float xi, float yi, float zi)
    {
        this.x = xi;
        this.y = yi;
        this.z = zi;
    }

    float getX()
    {
        return this.x;
    }

    float getY()
    {
        return this.y;
    }

    float getZ()
    {
        return this.z;
    }
}
