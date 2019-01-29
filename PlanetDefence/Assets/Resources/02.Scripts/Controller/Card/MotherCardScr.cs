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

public abstract class MotherCardScr : MonoBehaviour 
{
    public string sCardName;
    public CardType eCardType;
    public WaveInfo cWaveInfo;
}
