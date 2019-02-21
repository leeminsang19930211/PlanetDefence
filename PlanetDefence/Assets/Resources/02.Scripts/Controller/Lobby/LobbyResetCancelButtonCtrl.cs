using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyResetCancelButtonCtrl : MonoBehaviour
{
    public GameObject resetPopUp = null;

    public void OnClick()
    {
        resetPopUp.SetActive(false);
    }
}
