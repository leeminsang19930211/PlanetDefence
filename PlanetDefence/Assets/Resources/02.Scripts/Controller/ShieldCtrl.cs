using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldCtrl: MonoBehaviour
{
    public GameObject[] m_lv2ShieldImgs = null;
    public GameObject[] m_lv3ShieldImgs = null;

    public void ShowShield(PlanetArea area, Turret turret)
    {
        switch (area)
        {
            case PlanetArea.Up:
                if(turret == Turret.Lv2_Shield)
                {
                    m_lv2ShieldImgs[0].SetActive(true);
                }
                else if(turret == Turret.Lv3_Shield)
                {
                    m_lv3ShieldImgs[0].SetActive(true);
                }      
                break;
            case PlanetArea.Left:
                if (turret == Turret.Lv2_Shield)
                {
                    m_lv2ShieldImgs[1].SetActive(true);
                }
                else if (turret == Turret.Lv3_Shield)
                {
                    m_lv3ShieldImgs[1].SetActive(true);
                }
                break;
            case PlanetArea.Down:
                if (turret == Turret.Lv2_Shield)
                {
                    m_lv2ShieldImgs[2].SetActive(true);
                }
                else if (turret == Turret.Lv3_Shield)
                {
                    m_lv3ShieldImgs[2].SetActive(true);
                }
                break;
            case PlanetArea.Right:
                if (turret == Turret.Lv2_Shield)
                {
                    m_lv2ShieldImgs[3].SetActive(true);
                }
                else if (turret == Turret.Lv3_Shield)
                {
                    m_lv3ShieldImgs[3].SetActive(true);
                }
                break;
        }
    }

    public void HideAllShields(PlanetArea area)
    {
        switch (area)
        {
            case PlanetArea.Up:
                m_lv2ShieldImgs[0].SetActive(false);
                m_lv3ShieldImgs[0].SetActive(false);
                break;
            case PlanetArea.Left:
                m_lv2ShieldImgs[1].SetActive(false);
                m_lv3ShieldImgs[1].SetActive(false);
                break;
            case PlanetArea.Down:
                m_lv2ShieldImgs[2].SetActive(false);
                m_lv3ShieldImgs[2].SetActive(false);
                break;
            case PlanetArea.Right:
                m_lv2ShieldImgs[3].SetActive(false);
                m_lv3ShieldImgs[3].SetActive(false);
                break;
        }
    }

    private void Awake()
    {
        foreach(GameObject shield in m_lv2ShieldImgs)
        {
            shield.SetActive(false);
        }
        foreach (GameObject shield in m_lv3ShieldImgs)
        {
            shield.SetActive(false);
        }
    }
}
