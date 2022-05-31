using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Castlejau : MonoBehaviour
{
    #region Members

    //PUBLIC
    public List<Transform> ControlPointsTransform;
    public Color MaterialColor;
    public float s = 0.2f;
    public GameObject prefab,cp;

    //PRIVATE
    private List<Vector3> bezierCurvePoints;
    private List<Vector3> curvePoints;
    private List<Vector3> controlPointsVector;
    private float t;

    #endregion Members

    void Start()
    {
        controlPointsVector = new List<Vector3>();
        bezierCurvePoints = new List<Vector3>();
    }


    // Update is called once per frame
    void Update()
    {
        if(s < 0f)
        {
            s = 0f;
        }else if(s > 1f)
        {
            s = 1f;
        }
        if (Input.GetKeyDown("z"))
        {
            TryDrawCurve();
        }
        if (Input.GetKeyDown("r"))
        {
            ResetCurve();
        }
        if (Input.GetKeyDown("x"))
        {
            bezierCurvePoints = new List<Vector3>();
            ControlPointsTransform = new List<Transform>();
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            s -= 0.1f;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            s += 0.1f;
        }

        if (Input.GetButtonDown("Fire1"))
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = 10f;
            Vector3 objectPos = Camera.main.ScreenToWorldPoint(mousePos);
            GameObject controlPoint = Instantiate(cp, objectPos, Quaternion.identity);
            ControlPointsTransform.Add(controlPoint.transform);
        }

    }


    public void ResetCurve()
    {
        GameObject[] points = GameObject.FindGameObjectsWithTag("points");
        for(int i = 0; i < points.Length; i++)
        {
            Destroy(points[i]);
        }
    }
    public void TryDrawCurve()
    {
        for (t = 0; t <= 1; t += s)
        {
            Vector3 vec = getBezierPoint(t, ControlPointsTransform);
            bezierCurvePoints.Add(vec);
            GameObject pointBez = Instantiate(prefab, vec, Quaternion.identity);
        }
    }

    private Vector3 getBezierPoint(float time, List<Transform> controlPoints)
    {
        var degree = controlPoints.Count - 1;

        List<Vector3> bezierPoints = new List<Vector3>();
        for (int level = degree; level >= 0; level--)
        {
            if (level == degree)
            {
                for (int i = 0; i <= degree; i++)
                {
                    bezierPoints.Add(controlPoints[i].position);
                }
                continue;
            }

            int lastIdx = bezierPoints.Count;
            int levelIdx = level + 2;
            int idx = lastIdx - levelIdx;
            for (int i = 0; i <= level; i++)
            {
                Vector3 pi = (1 - time) * bezierPoints[idx] + time * bezierPoints[idx + 1];
                bezierPoints.Add(pi);
                ++idx;
            }
        }

        int lastElmnt = bezierPoints.Count - 1;
        return bezierPoints[lastElmnt];
    }

}
