using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor (typeof(FieldofView))]
public class FOVEditor : Editor
{
    private void OnSceneGUI()
    {
        FieldofView fov = (FieldofView)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(fov.transform.position, Vector3.forward, Vector3.right, 360, fov.viewRadius);
        Vector3 viewAngleA = fov.DirFromAngle(-fov.viewAngle / 2, false);
        Vector3 viewAngleB = fov.DirFromAngle(fov.viewAngle / 2, false);

        var a1 = fov.transform.position;
        var a2 = fov.transform.position + viewAngleA * fov.viewRadius;
        Handles.DrawLine(a1, a2);

        var b1 = fov.transform.position;
        var b2 = fov.transform.position + viewAngleB * fov.viewRadius;
        Handles.DrawLine(b1, b2);

        Handles.color = Color.yellow;
        foreach(Transform visibleTarget in fov.visibleTargets)
        {
            Handles.DrawLine(fov.transform.position, visibleTarget.position);
        }
    }
}
