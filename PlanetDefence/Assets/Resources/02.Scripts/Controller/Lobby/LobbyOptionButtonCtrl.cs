using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyOptionButtonCtrl : MonoBehaviour
{
    public GameObject m_popUpPanel = null;
    
    public void OnClick()
    {
        m_popUpPanel.SetActive(true);
    }
}
