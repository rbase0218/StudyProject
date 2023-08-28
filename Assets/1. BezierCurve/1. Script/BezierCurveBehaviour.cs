using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BezierPointData
{
    public Vector3 startPoint;
    public Vector3 endPoint;
    public Vector3 startTangent;
    public Vector3 endTangent;
}

public class BezierCurveBehaviour : MonoBehaviour
{
    public List<BezierPointData> bezierPointDatas;
    public bool isShow;

    public void CreatePoint()
    {
        for (int i = 0; i < bezierPointDatas.Count - 1; ++i)
        {
            bezierPointDatas[i].startPoint = new Vector3(i, i, i);
            bezierPointDatas[i].endPoint = new Vector3(i + 1, i + 1, i + 1);
            bezierPointDatas[i].startTangent = Vector3.zero;
            bezierPointDatas[i].endTangent = Vector3.zero;
        }
    }
}

