using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTest : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("Play");
            AudioManager.Inst.playSelectSFX(AudioManager.eSelectSFX.ButtonSFX);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log("BGM1");
            AudioManager.Inst.PlayBGM(AudioManager.eBGM.LobbyBGM);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("BGM2");
            AudioManager.Inst.PlayBGM(AudioManager.eBGM.ChoiceBGM);
        }
    }
}
