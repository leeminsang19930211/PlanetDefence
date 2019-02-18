using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyButtonsCtrl: MonoBehaviour
{
    public GameObject m_startButton = null;
    public GameObject m_endButton = null;
    public GameObject m_newButton = null;
    public GameObject m_playButton = null;

    public void UpdateButtons()
    {
        if (FileMgr.Inst.PlayerReset == true)
        {
            m_startButton.SetActive(true);
            m_newButton.SetActive(false);
            m_playButton.SetActive(false);
            m_endButton.SetActive(true);
        }
        else
        {
            m_startButton.SetActive(false);
            m_newButton.SetActive(true);
            m_playButton.SetActive(true);
            m_endButton.SetActive(true);
        }
    }

    void OnEnable()
    {
        UpdateButtons();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
