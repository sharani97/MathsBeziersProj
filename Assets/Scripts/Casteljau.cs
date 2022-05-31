using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Casteljau : MonoBehaviour
{

    #region Members

    //PUBLIC
    public List<Transform> ControlPointsTransform;
    public Color MaterialColor;
    public int stepLength = 5;

    //PRIVATE
    private List<Vector3> bezierCurvePoints;
    private List<Vector3> curvePoints;
    private List<Vector3> controlPointsVector;
    private float step;
    private float t;

    #endregion Members

    void Start()
    {
        step = 1 / stepLength;
        controlPointsVector = new List<Vector3>();
        bezierCurvePoints = new List<Vector3>();

        foreach(var c in ControlPointsTransform)
        {
            controlPointsVector.Add(c.position);
        }


        for (t = 0; t<=1; t+=step)
        {
            Vector3 vec = getBezierPoint(t, controlPointsVector);
            Debug.Log(vec);
            bezierCurvePoints.Add(vec);
        }
        
    }


    private Vector3 getBezierPoint(float time, List<Vector3> controlPointsList)
    {
        var degree = controlPointsList.Count - 1;

        curvePoints = new List<Vector3>();

        for (int etape = degree; etape >= 0; etape--)
        {
            if (etape == degree)
            {
                for (int k = 0; k <= degree; k++)
                {
                    curvePoints.Add(controlPointsList[k]);
                }
                continue;
            }
            

            int last = curvePoints.Count;            //our last index in the last list
            int lvl = etape + 2;                     //our level is count - 1 so we need to add 2 to be on +1
            int index = last - lvl;                  //find our current index

            for (int point = 0; point <= etape; point++)
            {
                Vector3 p = (1 - time) * curvePoints[index] + time * curvePoints[index + 1]; ;
                curvePoints.Add(p);
                ++index;
            }

        }

        int lastElmnt = curvePoints.Count - 1;
        return curvePoints[lastElmnt];
    }


    
    // Update is called once per frame
    void Update()
    {
        
    }
    
}
