using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CardType
{
    None,
    Normal,
    Boss,
    Event,
    Max
}

public enum MobType
{
    Normal,
    Kamikaze,
    Pirate
}

[System.Serializable]
public struct WavesMob
{
    public MobType eMobType;
    public int nMobNum;
    public float fDelayTime;
    public float fFirstDelayTime;
}

public class CardInfo : MonoBehaviour
{
    public string sCardName;
    public CardType eCardType;
    public WavesMob[] Waves;

    public void Selected()
    {
        FindObjectOfType<CardCtrl>().DeleteCards();

        SceneLoader.LoadScene("Battle");
    }
}