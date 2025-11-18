using UnityEngine;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

public class HeadLook : MonoBehaviour
{
  public  bool isPlayer;
    [Header("References")]
    public Transform headTransform;

    [Header("View Settings")]
    public float viewDistance = 5f;
    public float viewAngle = 30f;
    public LayerMask obstacleMask;
    public LayerMask targetLayer;

    // STATIC event accessible by all scripts
    public static event Action<Transform, Transform> OnEyeContactGlobal;
    public static event Action OnEyeContactLocal;
    public static event Action OnEyeContactLocalLost;

    private List<Transform> seenTargets = new List<Transform>();

    void Update()
    {
        if (headTransform == null) return;

        seenTargets.Clear();
        Vector2 forward = headTransform.up;

        Collider2D[] hits = Physics2D.OverlapCircleAll(headTransform.position, viewDistance, targetLayer);

        foreach (Collider2D hit in hits)
        {
            Transform target = hit.transform;
            if (target == transform) continue;

            Vector2 toTarget = (target.position - headTransform.position).normalized;
            float distance = Vector2.Distance(headTransform.position, target.position);
            float angle = Vector2.Angle(forward, toTarget);

            if (angle > viewAngle) continue;
            if (Physics2D.Raycast(headTransform.position, toTarget, distance, obstacleMask)) continue;

            seenTargets.Add(target);

            // Mutual eye contact check
            HeadLook targetLook = target.GetComponent<HeadLook>();
            if (targetLook != null && targetLook.CanSee(transform))
            {
                // FIRE STATIC GLOBAL EVENT
                OnEyeContactGlobal?.Invoke(transform, target);
                if (isPlayer)
                {
                    OnEyeContactLocal?.Invoke();
                }
                else
                {
                    OnEyeContactLocalLost?.Invoke();
                }
            }
          
        }
    }

    public void Rotate(float angle)
    {
        // Get direction to target
       transform.rotation = Quaternion.Euler(0, 0,angle);
    }
    public bool CanSee(Transform target)
    {
        if (headTransform == null) return false;

        Vector2 forward = headTransform.up;
        Vector2 toTarget = (target.position - headTransform.position).normalized;
        float distance = Vector2.Distance(headTransform.position, target.position);
        float angle = Vector2.Angle(forward, toTarget);

        if (angle > viewAngle) return false;
        if (Physics2D.Raycast(headTransform.position, toTarget, distance, obstacleMask)) return false;

        return true;
    }

    void OnDrawGizmos()
    {
        if (headTransform == null) return;

        Vector3 origin = headTransform.position;
        Vector2 forward = headTransform.up;

        int segments = 20;
        for (int i = 0; i <= segments; i++)
        {
            float lerp = Mathf.Lerp(-viewAngle, viewAngle, i / (float)segments);
            Vector3 dir = Quaternion.Euler(0, 0, lerp) * forward;
            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(origin, origin + dir * viewDistance);
        }

        foreach (var target in seenTargets)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(origin, target.position);
        }

        Gizmos.color = new Color(1, 1, 1, 0.15f);
        Gizmos.DrawWireSphere(origin, viewDistance);
    }
}
