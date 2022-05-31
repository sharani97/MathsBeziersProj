using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Casteljau : MonoBehaviour
{

    #region Members

    //PUBLIC
    public List<GameObject> ControlPointsTransform;
    public Color MaterialColor;
    public int stepLength = 5;

    //PRIVATE
    private List<Vector3> bezierCurvePoints;
    private List<Vector3> curvePoints;
    private float step;
    private float t;
    private int point;
    private int etape;
    private int n;

    #endregion Members

    void Start()
    {
        step = 1 / stepLength;
        n = ControlPointsTransform.Count;
        bezierCurvePoints = new List<Vector3>();

        for (t = 0; t<=1; t+=step)
        {

        }
        
    }


    private Vector3 getBezierPoint(float time, List<Vector3> controlPointsList)
    {
        curvePoints = new List<Vector3>();

        for (etape = n - 1; etape >= 0; etape--)
        {
            if (etape == n - 1)
            {
                for (int k = 0; k <= n - 1; k++) curvePoints.Add(controlPointsList[k]);
                continue;
            }

            int last = curvePoints.Count;  //our last index in the last list
            int lvl = etape + 2;                     //our level is count - 1 so we need to add 2 to be on +1
            int index = last - lvl;                  //find our current index

            for (point = 0; point <= etape; point++)
            {
                Vector3 p = time * curvePoints[index + 1] + (1 - time) * curvePoints[index];
                curvePoints.Add(p);
            }

        }

        return curvePoints[curvePoints.Count - 1];
    }


    
    // Update is called once per frame
    void Update()
    {
        
    }
    
}
