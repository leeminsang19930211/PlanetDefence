using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LabStackCtrl : MonoBehaviour
{
    public GameObject[] m_LabStackTexts;

    void Start()
    {
        m_LabStackTexts = GameObject.FindGameObjectsWithTag("LABSTACKTEXT");
    }

    void Update()
    {
        for (int i = 0; i < (int)Lab.End; i++)
        {
            if (Player.Inst.m_labInfos[i].stacks < Player.Inst.LabMaxStacks[i])
            {
                int LabStack = Player.Inst.m_labInfos[i].stacks;
                m_LabStackTexts[i].GetComponent<Text>().text = LabStack.ToString();
            }

            else if (Player.Inst.m_labInfos[i].stacks >= Player.Inst.LabMaxStacks[i])
            {
                m_LabStackTexts[i].GetComponent<Text>().text = "M";
            }
        }

    }
}
