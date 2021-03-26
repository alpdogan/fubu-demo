using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(LineOfSight))]
public class LineOfSightEditor : Editor
{
    void OnSceneGUI()
    {
        LineOfSight lineOfSight = (LineOfSight) target;
        Handles.color = Color.white;
        Transform transform = lineOfSight.transform;
        Handles.DrawWireArc(transform.position, transform.up, transform.forward, 360, lineOfSight.viewRadius);
        Handles.color = new Color(1,0,0, 0.5f);
        Vector3 viewAngleA = lineOfSight.transform.rotation * lineOfSight.DirFromAngle(-lineOfSight.viewAngle / 2);
        Vector3 viewAngleB = lineOfSight.transform.rotation * lineOfSight.DirFromAngle(lineOfSight.viewAngle / 2);
        Handles.DrawLine(transform.position, transform.position + viewAngleA * lineOfSight.viewRadius);
        Handles.DrawLine(transform.position, transform.position + viewAngleB * lineOfSight.viewRadius);
        // Handles.DrawWireArc();
        Handles.DrawSolidArc(transform.position, transform.up, viewAngleA, lineOfSight.viewAngle, lineOfSight.viewRadius);


        Handles.color = Color.cyan;
        foreach (Transform visibleTarget in lineOfSight.visibleTargets)
        {
            Handles.DrawLine(lineOfSight.transform.position, visibleTarget.position);
        }
    }
}