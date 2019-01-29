using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceCtrl : MonoBehaviour
{
    public GameObject m_junk = null;
    public GameObject m_eleCircuit = null;
    public GameObject m_coin = null;

    private Text m_junkText = null;
    private Text m_eleCircuitText = null;
    private Text m_coinText = null;

    public int JunkCnt
    {
        set
        {
            if (value < 0)
                return;

            m_junkText.text = value.ToString();
        }
    }


    public int EleCircuitCnt
    {
        set
        {
            if (value < 0)
                return;

            m_eleCircuitText.text = value.ToString();
        }
    }


    public int CoinCnt
    {
        set
        {
            if (value < 0)
                return;

            m_coinText.text = value.ToString();
        }
    }

    void Start()
    {
        m_junkText = m_junk.GetComponent<Text>();
        m_eleCircuitText = m_eleCircuit.GetComponent<Text>();
        m_coinText = m_coin.GetComponent<Text>();
    }
 
    void Update()
    {
        
    }
}
