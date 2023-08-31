using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEditor;

[System.Serializable]
public class Line
{
    public Vector3[] point;
}

public class BezierCurveBehaviour : MonoBehaviour
{
    public Line line1;
    public Line line2;

    [CustomValueDrawer("MyCustomDrawerStatic")]
    public float t;

    public bool isView = false;

    private static float MyCustomDrawerStatic(float value, GUIContent label)
    {
        return EditorGUILayout.Slider(label, value, 0f, 1f);
    }

    [Button]
    public void SetLinePos()
    {
        line1.point[0] = Vector3.zero;
        line1.point[1] = Vector3.zero;
        line1.point[1].y += 1f;

        line2.point[0] = Vector3.zero;
        line2.point[1] = Vector3.zero;
        line2.point[0].y = line2.point[1].y = line1.point[1].y;
        line2.point[1].x += 1f;
    }

    public void OnDrawGizmos()
    {
        if (!isView)
            return;

        // Start Pops
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(line1.point[0], 0.05f);
        Gizmos.DrawSphere(line2.point[0], 0.05f);

        // End Pos
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(line1.point[1], 0.05f);
        Gizmos.DrawSphere(line2.point[1], 0.05f);

        // Line Connect
        Gizmos.color = Color.green;
        Gizmos.DrawLine(line1.point[0], line1.point[1]);
        Gizmos.DrawLine(line2.point[0], line2.point[1]);

        // Line1
        var line1_S = line1.point[0];
        var line1_E = line1.point[1];

        // Line2 
        var line2_S = line2.point[0];
        var line2_E = line2.point[1];

        // Ratio
        Gizmos.color = Color.black;

        var line1Ratio = t * (line1_S.y + line1_E.y);

        // StartPos : 0, 1, 0 * Scalr (0) = 0, 0, 0
        // EndPos : 1, 1, 0 * Scalr (1) 1, 1, 0
        var line2Ratio = t * (line2_S.x + line2_E.x);


        var line1Y = line1_S;
        line1Y.y = line1Ratio;
        Gizmos.DrawSphere(line1Y, 0.03f);

        var line2X = line2_S;
        line2X.x = line2Ratio;
        Gizmos.DrawSphere(line2X, 0.03f);

        Gizmos.color = Color.black;
        Gizmos.DrawLine(line1Y, line2X);

        var connectRatio = t * (line1Y + line2X);
        connectRatio.y /= 2;

        Gizmos.color = Color.red;

        // Ratio Label
        var labelPos = (line1Y + line2X) / 2;

        GUIStyle style = new GUIStyle();
        style.normal.textColor = Color.red;

        Handles.Label(labelPos, $"T : {t}", style);
    }
}