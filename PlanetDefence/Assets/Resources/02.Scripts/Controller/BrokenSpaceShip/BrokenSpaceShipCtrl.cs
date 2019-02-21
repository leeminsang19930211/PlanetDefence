using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenSpaceShipCtrl : MonoBehaviour
{
    public GameObject[] m_rockets = null;
    public GameObject[] m_brokenSpcs = null;
    public GameObject m_repairedSpc = null;

    public void Init()
    {
        UpdateActives();
        m_repairedSpc.SetActive(false);
    }

    public void Launch()
    {
        foreach (GameObject rocket in m_rockets)
        {
            rocket.SetActive(false);
        }
        foreach (GameObject brokenSpc in m_brokenSpcs)
        {
            brokenSpc.SetActive(false);
        }

        m_repairedSpc.SetActive(true);
        EndingMgr.Inst.LaunchingSpaceShip = true;
        Player.Inst.Ended = true;
    }

    public void UpdateActives()
    {
        if(Player.Inst.Ended)
        {
            foreach (GameObject rocket in m_rockets)
            {
                rocket.SetActive(true);
            }
            m_brokenSpcs[3].SetActive(true);

            return;


        }

        foreach(GameObject rocket in m_rockets)
        {
            rocket.SetActive(false);
        }
        foreach (GameObject brokenSpc in m_brokenSpcs)
        {
            brokenSpc.SetActive(false);
        }

        m_brokenSpcs[0].SetActive(true);

        if (Player.Inst.m_spcPartInfos[0]._repaired == true)
        {
            if (Player.Inst.m_spcPartInfos[1]._repaired == false)
            {
                m_brokenSpcs[0].SetActive(false);
                m_brokenSpcs[1].SetActive(true);
            }
        }

        if (Player.Inst.m_spcPartInfos[1]._repaired == true)
        {
            if (Player.Inst.m_spcPartInfos[0]._repaired == false)
            {
                m_brokenSpcs[0].SetActive(false);
                m_brokenSpcs[2].SetActive(true);
            }

            if (Player.Inst.m_spcPartInfos[0]._repaired == true)
            {
                m_brokenSpcs[1].SetActive(false);
                m_brokenSpcs[2].SetActive(false);
                m_brokenSpcs[3].SetActive(true);
            }
        }

        if (Player.Inst.m_spcPartInfos[2]._repaired == true)
        {
            m_rockets[0].SetActive(true);
        }

        if (Player.Inst.m_spcPartInfos[3]._repaired == true)
        {
            m_rockets[1].SetActive(true);
        }

        if (Player.Inst.m_spcPartInfos[4]._repaired == true)
        {
            m_rockets[2].SetActive(true);
        }
    }     
}
