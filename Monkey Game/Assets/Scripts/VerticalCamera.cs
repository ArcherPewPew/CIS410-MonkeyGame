using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class VerticalCamera : MonoBehaviour
{

    [SerializeField] private float sensitivityY;
    [SerializeField] private float sensitivityScroll;
    [SerializeField] private float maxZoom = 3;
    [SerializeField] private float minZoom = 2;
    [SerializeField] private float maxYOrbit = 3;
    private CinemachineOrbitalTransposer vcam;
    private float y;
    private float scroll;

    void Start()
    {
        vcam = GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineOrbitalTransposer>();
        scroll = -40;
        y = 10;
    }
    void Update()
    {

        y -= +Input.GetAxis("Mouse Y") * sensitivityY;
        y = Mathf.Clamp(y, -maxYOrbit, maxYOrbit);
        if (y > 0)
        {
            vcam.m_FollowOffset.y = y;
        }
        else
        {
            vcam.m_FollowOffset.y = 0;
        }
        scroll += Input.GetAxis("Mouse ScrollWheel") * sensitivityScroll;
        scroll = Mathf.Clamp(scroll, -maxZoom, -minZoom);
        vcam.m_FollowOffset.x = scroll;
        vcam.m_FollowOffset.z = scroll;
    }
}
