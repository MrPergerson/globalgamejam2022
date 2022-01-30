using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldofView : MonoBehaviour
{
    public float viewRadius;
    [Range(0,360)]
    public float viewAngle;

    public LayerMask targetMask;
    public LayerMask obstacleMask;

    public bool findingTargets = true;

    public List<Transform> visibleTargets = new List<Transform>();

    void Start()
    {
        StartCoroutine("FindVisibleTargetsWithDelay", .2f);
    }

    IEnumerator FindVisibleTargetsWithDelay(float delay)
    {
        while(true)
        {
            yield return new WaitForSeconds(delay);
            if(findingTargets) FindVisibleTargets();
        }
    }

    void FindVisibleTargets()
    {
        visibleTargets.Clear();
        Collider2D[] targets = Physics2D.OverlapCircleAll(transform.position, viewRadius, targetMask);

        for(int i = 0; i < targets.Length; i++)
        {
            var target = targets[i].transform;
            var directionToTarget = (target.position - transform.position).normalized;

            var isTargetWithinViewAngle = Vector2.Angle(transform.right, directionToTarget) < viewAngle / 2; // I don't get why I need to divide by two yet
            if (isTargetWithinViewAngle)
            {
                var distanceToTarget = Vector2.Distance(transform.position, target.position);

                if(!Physics2D.Raycast(transform.position, directionToTarget, distanceToTarget, obstacleMask))
                {
                    visibleTargets.Add(target);
                }
            }
        }
    }

    public Vector2 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if(!angleIsGlobal)
        {
            var offset = transform.eulerAngles.z - 90;
            angleInDegrees -= offset;
        }


        // need to convert unity's angles to trigometry unit circle.
        // this can be done by substracting 90 from any given angle in unity. 
        // Because sin(90-x) = cos(x), which means we just need to swap sin and cos whenever we use an angle

        var angle = angleInDegrees * Mathf.Deg2Rad;

        return new Vector2(Mathf.Sin(angle), Mathf.Cos(angle));
    }

    public void ClearVisibleTargets()
    {
        visibleTargets.Clear();
    }
}
