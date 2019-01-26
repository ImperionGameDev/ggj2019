using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    private Camera m_Camera;
    
    void Awake()
    {
        m_Camera = Camera.main;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {        
        Vector3 WorldPosition = m_Camera.ScreenToWorldPoint(Input.mousePosition);
        WorldPosition.z = -1;
        this.transform.SetPositionAndRotation(WorldPosition, Quaternion.identity);
    }
}
