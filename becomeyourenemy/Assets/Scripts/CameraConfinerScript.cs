using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraConfinerScript : MonoBehaviour
{
    public GameObject camera;
    public CinemachineConfiner2D cinemachineConfiner2D;
    public CinemachineConfiner cinemachineConfiner;
    public PolygonCollider2D collider1;
    public PolygonCollider2D collider2;

    private void Start()
    {
        cinemachineConfiner2D = camera.GetComponent<CinemachineConfiner2D>();
    }

    [ContextMenu("Change to first Collider")]
    public void ChangeCollider1()
    {
        cinemachineConfiner.m_BoundingShape2D = collider1;
    }
    [ContextMenu("Change to second Collider")]
    public void ChangeCollider2()
    {
        cinemachineConfiner.m_BoundingShape2D = collider2;
    }
}
