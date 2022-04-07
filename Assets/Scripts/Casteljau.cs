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
    private List<Vector3> coordsList;
    private List<Vector3> defaultCoords;
    private List<Vector3> tempCoord;
    private List<Vector3> tempCoordPerStep;
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
        coordsList = new List<Vector3>();
        defaultCoords = new List<Vector3>();
        tempCoord = new List<Vector3>();
        tempCoordPerStep = new List<Vector3>();

        defaultCoords.Add(ControlPointsTransform[0].position);
        defaultCoords.Add(ControlPointsTransform[1].position);
        defaultCoords.Add(ControlPointsTransform[2].position);
        defaultCoords.Add(ControlPointsTransform[3].position);

        
        for (t = 0; t<=1; t+=step)
        {

            tempCoord = defaultCoords;

            for(etape = 1; etape <= n; etape++)
            {
                tempCoordPerStep.Clear();

                for(point = 0; point <= n - etape ; point++)
                {
                    tempCoordPerStep.Add((1 - t) * tempCoord[point] + t * tempCoord[point+1]);
                    Debug.Log(tempCoordPerStep);
                }
                tempCoord = tempCoordPerStep;
            }
            coordsList.Add(tempCoord[0]);
        }
        
        Debug.Log(coordsList.Count);
    }

    /*
    // Update is called once per frame
    void Update()
    {
        
    }
    */
}
