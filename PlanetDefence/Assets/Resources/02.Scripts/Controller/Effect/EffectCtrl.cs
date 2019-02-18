using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectCtrl : MonoBehaviour
{
    public int m_maxPlayCnt = 1;
    public string m_animState = "";

    private int m_curPlayCnt = 0;
    private Transform m_trsf = null;
    private Animator m_animator = null;

    public void Play(Vector3 pos)
    {
        m_trsf.position = pos;
        StartCoroutine("PlayAnim");    
    }

    private IEnumerator PlayAnim()
    {
        m_animator.Play(m_animState, 0, 0);
        m_curPlayCnt += 1;

        while (true)
        {
            AnimatorStateInfo info = m_animator.GetCurrentAnimatorStateInfo(0);

            if (info.normalizedTime >= 1f)
            {
                if(m_curPlayCnt >= m_maxPlayCnt)
                {
                    break;
                }
                else
                {
                    m_animator.Play(m_animState, 0, 0);
                    m_curPlayCnt += 1;                    
                }
            }

            yield return null;
        }

        gameObject.SetActive(false);
    }

    private void Awake()
    {
        m_trsf = GetComponent<Transform>();
        m_animator = GetComponent<Animator>();
    }
}
