using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RepairStackCtrl : MonoBehaviour
{
    public GameObject[] m_RepairStackTexts;

    void Start()
    {
        m_RepairStackTexts = GameObject.FindGameObjectsWithTag("REPAIRSTACKTEXT");
    }

    void Update()
    {
        for (int i = 0; i < (int)SpaceShipPart.End; i++)
        {
            if (Player.Inst.m_spcPartInfos[i]._repaired == false)
            {
                m_RepairStackTexts[i].GetComponent<Text>().text = "X";
            }

            else if (Player.Inst.m_spcPartInfos[i]._repaired == true)
            {
                m_RepairStackTexts[i].GetComponent<Text>().text = "O";
            }
        }
    }
}
