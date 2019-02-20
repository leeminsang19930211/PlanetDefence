using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class TurretDropPopUpCtrl : MonoBehaviour
{
    public Sprite[] m_sprites = new Sprite[(int)Turret.End];
    public Image m_turretImg = null;

    public void OnClick()
    {
        gameObject.SetActive(false);
    }

    public void PopUp(Turret turret)
    {
        if (turret == Turret.End)
            return;

        m_turretImg.sprite = m_sprites[(int)turret];

        gameObject.SetActive(true);
    }
}
