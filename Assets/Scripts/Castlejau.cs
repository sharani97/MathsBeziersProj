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
    [SerializeField]
    private Dictionary<List<Transform>, List<GameObject>> Dic = new Dictionary<List<Transform>, List<GameObject>>();
    private List<GameObject> objectPositions = new List<GameObject>();
    private List<Vector3> bezierCurvePoints;
    private float t;

    #endregion Members

    void Start()
    {
        bezierCurvePoints = new List<Vector3>();
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            s -= 0.005f;
            if (s <= 0) s = 0.001f;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            s += 0.005f;
            if (s >= 1) s = 0.999f;
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
            Dic.TryAdd(ControlPointsTransform, objectPositions);

            GameObject parent = new GameObject();
            foreach(GameObject obj in objectPositions)
            {
                obj.transform.parent = parent.transform;
            }
            foreach (Transform obj in ControlPointsTransform)
            {
                obj.parent = parent.transform;
            }


            bezierCurvePoints = new List<Vector3>();
            objectPositions = new List<GameObject>();
            ControlPointsTransform = new List<Transform>();
            
        }


        if (Input.GetButtonDown("Fire1"))
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = 20f;
            Vector3 objectPos = Camera.main.ScreenToWorldPoint(mousePos);
            GameObject controlPoint = Instantiate(cp, objectPos, Quaternion.identity);
            ControlPointsTransform.Add(controlPoint.transform);
            ResetCurve();
            TryDrawCurve();
        }
        if (Input.GetButtonDown("Fire2"))
        {
            
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if(hit.collider.tag == "points")
                {
                    Destroy(hit.collider.transform.parent.gameObject);
                }
                Destroy(hit.collider.gameObject);
                ControlPointsTransform.Remove(hit.collider.gameObject.transform);
                
                bezierCurvePoints = new List<Vector3>();
                ResetCurve();
                TryDrawCurve();
            }
        }
        if (Input.GetButton("Fire3"))
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = 20f;
            Ray ray = Camera.main.ScreenPointToRay(mousePos);
            Vector3 objectPos = Camera.main.ScreenToWorldPoint(mousePos);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                
                hit.collider.gameObject.transform.position = objectPos;
                bezierCurvePoints = new List<Vector3>();
                ResetCurve();
                TryDrawCurve();
            }
        }

    }


    public void ResetCurve()
    {
        List<GameObject> points = objectPositions;
        for(int i = 0; i < points.Count; i++)
        {
            Destroy(points[i]);
        }
    }
    public void TryDrawCurve()
    {
        objectPositions = new List<GameObject>();

        for (t = 0; t <= 1.0f; t += s)
        {
            Vector3 vec = getBezierPoint(t, ControlPointsTransform);
            bezierCurvePoints.Add(vec);
            GameObject pointBez = Instantiate(prefab, vec, Quaternion.identity);
            objectPositions.Add(pointBez);
        }

        for (int i = 0; i < objectPositions.Count - 1; i++)
        {
            LineRenderer line = objectPositions[i].AddComponent<LineRenderer>();
            GameObject nextPoint = objectPositions[i + 1];

            line.startWidth = 0.2f;
            line.endWidth = 0.2f;
            line.positionCount = 2;
            line.startColor = Color.yellow;
            line.endColor = Color.yellow;

            line.SetPosition(0, objectPositions[i].transform.position);
            line.SetPosition(1, nextPoint.transform.position);
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
