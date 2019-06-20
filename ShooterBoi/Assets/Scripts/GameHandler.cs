using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    public static GameHandler instance = null;

    Camera cam;
    void Start()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(instance.gameObject);
        }

        cam = Camera.main;
    }

    public Vector3 GetMouseWorldPos()
    {
        Vector3 position = Vector3.zero;

        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.forward, 0f);
        float rayDitance;

        if(plane.Raycast(ray, out rayDitance))
        {
            position = ray.GetPoint(rayDitance);
        }

        return position;
    }
}
