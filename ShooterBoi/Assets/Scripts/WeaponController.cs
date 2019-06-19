using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponController : MonoBehaviour
{
    public Transform origin;

    protected bool attacking = false;
    public float radius = 0.3f;

    // Update is called once per frame
    protected virtual void Update(Camera cam)
    {
        if (!attacking)
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            Plane plane = new Plane(Vector3.forward, 0f);
            float rayDistance;
            Vector3 position = Vector3.zero;
            Vector3 dir = Vector3.zero;
            if (plane.Raycast(ray, out rayDistance))
            {
                position = ray.GetPoint(rayDistance);
                dir = (position - origin.position).normalized;
            }
            Vector3 bowPosition = origin.position + dir * radius;
            this.transform.position = bowPosition;
            float angle = 90 - Mathf.Rad2Deg * Mathf.Atan2(dir.x, dir.y);
            transform.eulerAngles = Vector3.forward * angle;
        }
    }
}
