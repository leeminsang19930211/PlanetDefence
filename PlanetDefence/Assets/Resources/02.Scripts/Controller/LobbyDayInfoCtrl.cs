using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyDayInfoCtrl : MonoBehaviour
{
    public Text m_text = null;

    void Start()
    {
        
    }
    
    void Update()
    {
        m_text.text = GlobalGameObjectMgr.Inst.CurDay.ToString() + "/" + GlobalGameObjectMgr.Inst.MaxDay.ToString() + " Day"; 
    }
}
