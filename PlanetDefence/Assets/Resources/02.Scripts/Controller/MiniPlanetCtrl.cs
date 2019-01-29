using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniPlanetCtrl : MonoBehaviour
{
    public GameObject m_miniPlanet_spot = null;

    private Transform m_miniPlanet_spotTrsf = null;

    void Start()
    {
        m_miniPlanet_spotTrsf = m_miniPlanet_spot?.GetComponent<Transform>();
    }

    void Update()
    {
        
    }

    public void RotateToTarget(Vector3 targetDir)
    {
        float angle = MyMath.LeftAngle360(Vector3.up, targetDir);

        m_miniPlanet_spotTrsf.eulerAngles = new Vector3(0, 0, angle);

        //m_miniPlanet_spotTrsf.Rotate(Vector3.forward, angle);
    }
}
