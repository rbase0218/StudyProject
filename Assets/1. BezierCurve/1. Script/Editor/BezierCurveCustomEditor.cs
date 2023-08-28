using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BezierCurveBehaviour))]
public class BezierCurveCustomEditor : Editor
{
    private Vector3[] points;

    private bool isShow;
    private BezierCurveBehaviour bezier;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (bezier)
        {
            EditorGUILayout.BeginHorizontal(); //BeginHorizontal() 이후 부터는 GUI 들이 가로로 생성됩니다.
            GUILayout.FlexibleSpace(); // 고정된 여백을 넣습니다. ( 버튼이 가운데 오기 위함)
            //버튼을 만듭니다 . GUILayout.Button("버튼이름" , 가로크기, 세로크기)

            if (GUILayout.Button("이펙트 실행", GUILayout.Width(120), GUILayout.Height(30)))
            {
                bezier.CreatePoint();
            }

            GUILayout.FlexibleSpace(); // 고정된 여백을 넣습니다.
            EditorGUILayout.EndHorizontal(); // 가로 생성 끝
        }
    }

    private void OnSceneGUI()
    {
        if (isShow)
        {
            var bezierData = bezier.bezierPointDatas;

            for (int i = 0; i < bezierData.Count; ++i)
            {
                int next = (i + 1 >= bezierData.Count || i == 0) ? i : i + 1;
                
                // i + 1의 StartPoint를 i로 하고 싶다.
                // i일 경우엔 i만.
                points = Handles.MakeBezierPoints(
                    bezierData[next].startPoint,
                    bezierData[next].startPoint,
                    bezierData[i].startTangent,
                    bezierData[i].endTangent,
                    20);
                
                Handles.Label(bezierData[i].startPoint, bezierData[i].startPoint.ToString() );
                Handles.Label(bezierData[i].endPoint, bezierData[i].endPoint.ToString() );
                Handles.color = Color.red;
                Handles.DrawAAPolyLine(points);
            }
        }
    }

    private void OnEnable()
    {
        bezier = (BezierCurveBehaviour)target;
        isShow = bezier.isShow;
    }

    // public void OnSceneGUI()
    // {
    //     Event currEvent = Event.current;
    //     
    //     Handles.color = Color.red;
    //     BezierCurveBehaviour myObj = (BezierCurveBehaviour)target;
    //     
    //     myObj.startPoint = Handles.PositionHandle(myObj.startPoint, Quaternion.identity);
    //     myObj.endPoint = Handles.PositionHandle(myObj.endPoint, Quaternion.identity);
    //     Handles.SphereHandleCap(
    //         0,
    //         myObj.endPoint,
    //         myObj.transform.rotation * Quaternion.LookRotation(Vector3.up),
    //         1f,
    //         EventType.Repaint
    //     );
    //
    //     if (currEvent.type == EventType.DragExited)
    //     {
    //         Debug.Log("놓기 종료");
    //     }
    //     
    //     myObj.startTangent = Handles.PositionHandle(myObj.startTangent, Quaternion.identity);
    //     myObj.endTangent = Handles.PositionHandle(myObj.endTangent, Quaternion.identity);
    //
    //     Handles.DrawBezier(myObj.startPoint, myObj.endPoint, myObj.startTangent, myObj.endTangent, Color.red, null, 2f);
    // }
}