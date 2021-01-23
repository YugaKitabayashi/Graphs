using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ControllVirtualCamera : MonoBehaviour
{
    CinemachineFreeLook freeLook;
    [SerializeField] private float speed;
    void Start()
    {
        freeLook = GetComponent<CinemachineFreeLook>();
    }
    void Update()
    {
        freeLook.m_XAxis.Value = speed * Time.deltaTime;
    }

}
