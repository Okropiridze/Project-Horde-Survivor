using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorFollow : MonoBehaviour
{
    private Vector3 mouseWorldPos;

    private float followTime = 15f;

    private void Update()
    {
        GetMousePos();
        FollowMouse();
    }

    void GetMousePos()
    {
        mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0f;
    }

    void FollowMouse()
    {
        transform.position = Vector3.Lerp(transform.position, mouseWorldPos, Time.deltaTime * followTime);
    }
}
