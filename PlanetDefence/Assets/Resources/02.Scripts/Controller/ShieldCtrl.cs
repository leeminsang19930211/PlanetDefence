using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldCtrl: MonoBehaviour
{
    public GameObject[] m_shieldImgs = null;

    public void SetShieldImgActive(PlanetArea area, Turret turret, bool active)
    {
        switch (turret)
        {
            case Turret.Lv2_Shield:
                switch (area)
                {
                    case PlanetArea.Up:
                        m_shieldImgs[0].SetActive(active);
                        break;
                    case PlanetArea.Left:
                        m_shieldImgs[1].SetActive(active);
                        break;
                    case PlanetArea.Down:
                        m_shieldImgs[2].SetActive(active);
                        break;
                    case PlanetArea.Right:
                        m_shieldImgs[3].SetActive(active);
                        break;
                }
                break;
        }
    }

    public void HideAllShield(PlanetArea area)
    {
        switch (area)
        {
            case PlanetArea.Up:
                m_shieldImgs[0].SetActive(false);
                break;
            case PlanetArea.Left:
                m_shieldImgs[1].SetActive(false);
                break;
            case PlanetArea.Down:
                m_shieldImgs[2].SetActive(false);
                break;
            case PlanetArea.Right:
                m_shieldImgs[3].SetActive(false);
                break;
        }
    }

    private void Start()
    {
        foreach(GameObject shield in m_shieldImgs)
        {
            shield.SetActive(false);
        }
    }
}
