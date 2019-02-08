using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpaceShipCtrl : MonoBehaviour
{
    public enum STATE
    {
        FALLING,
        STAYING,
        REVOLVING,
        LANDING,
        END
    }

    public float m_fallingSpeed = 0;
    public float m_revolvingSpeed = 0;          // 가장 외각의 원을 기준으로 초당 회전 각도값. 
    public float[] m_fallingDists = new float[3];
    public float m_stayDuration = 0;            // 공전하기 이전에 잠깐 대기하는 시간 
    public Vector3 m_fallingDir = Vector3.zero;

    private int m_fallingRound = 0;             // 몇번째 낙하인지를 나타내는 값. 0~3 범위를 가진다. 
    private float m_stayingTimeAcc = 0;
    private float m_revolvingSpeedScalar = 0;   // 행성 원점으로부터 현재 거리에따라 스피드에 곱해져야 하는 값
    private float m_revolvingAngleAcc = 0;
    private STATE m_state = STATE.END;
    private Vector3 m_fallingTargetPos = Vector3.zero;
    private Vector3 m_startPos = Vector3.zero;
    private Transform m_transform = null;

    private delegate void StateProc();
    private readonly StateProc[] m_stateProcs = new StateProc[(int)STATE.END];

    public Vector3 Pos
    {
        get
        {
            return m_transform.position;
        }
    }

    public float AngleFromPlanetUp
    {
        get
        {
            Vector3 toSpaceShip = m_transform.position - BattleGameObjectMgr.Inst.PlanetPos;

            return MyMath.LeftAngle180(BattleGameObjectMgr.Inst.PlanetUp, toSpaceShip);           
        }
    }

    /* events */
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PLANET")
        {
            BeforeDestruction();

            SpaceShipMgr.Inst.RemoveSpaceShip(this);

            Destroy(gameObject);
        }
    }

    // 자식 우주선에서 호출해줄것
    protected void Init()
    {
        m_stateProcs[(int)STATE.FALLING] = StateProc_Falling;
        m_stateProcs[(int)STATE.STAYING] = StateProc_Staying;
        m_stateProcs[(int)STATE.REVOLVING] = StateProc_Revolving;
        m_stateProcs[(int)STATE.LANDING] = StateProc_Landing;

        m_transform = GetComponent<Transform>();

        m_startPos = m_transform.position;

        ChangeState(STATE.FALLING);
    }


    /* not events */
    // 파괴되기 이전의 호출되는 함수이다. 필요하면 상속받은 클래스에서 정의 해주자.
    protected virtual void BeforeDestruction()
    {

    }
   
    // 업데이트에서 호출해줄 것 . 호출해주면 떨어지고 공전하면서 움직이게 된다
    protected void MoveBody()
    {
        m_stateProcs[(int)m_state]();
    }

    private void StateProc_Falling()
    {
        float curMoveDist = m_fallingSpeed * Time.deltaTime;
        Vector3 left = m_fallingTargetPos - m_transform.position;

        if (curMoveDist < left.magnitude)
        {
            m_transform.position += m_fallingDir.normalized * curMoveDist;
        }
        else
        {
            m_transform.position += left;
            ChangeState(STATE.STAYING);
        }
    }

    private void StateProc_Staying()
    {
        m_stayingTimeAcc += Time.deltaTime;

        if(m_stayingTimeAcc >= m_stayDuration)
        {
            ChangeState(STATE.REVOLVING);
        }
    }

    private void StateProc_Revolving()
    {
        float curAngle = m_revolvingSpeed * m_revolvingSpeedScalar * Time.deltaTime;

        if(m_revolvingAngleAcc+ curAngle < 360f)
        {
            transform.RotateAround(BattleGameObjectMgr.Inst.PlanetPos, Vector3.forward, curAngle);           
        }
        else
        {
            transform.RotateAround(BattleGameObjectMgr.Inst.PlanetPos, Vector3.forward,  360f- m_revolvingAngleAcc);

            if(m_fallingRound >=2)
            {
                ChangeState(STATE.LANDING);
            }
            else
            {
                ChangeState(STATE.FALLING);
            }       
        }

        m_revolvingAngleAcc += curAngle;
    }

    private void StateProc_Landing()
    {
        float curMoveDist = m_fallingSpeed * Time.deltaTime;
        Vector3 left = m_fallingTargetPos - m_transform.position;

        if (curMoveDist < left.magnitude)
        {
            m_transform.position += m_fallingDir.normalized * curMoveDist;
        }
        else
        {
            m_transform.position += left;
        }
    }

    private void ChangeState(STATE newState)
    {
        if (m_state == newState)
            return;

        switch (m_state)
        {
            case STATE.FALLING:
                switch (newState)
                {
                    case STATE.STAYING:
                        m_stayingTimeAcc = 0;
                        break;
                }
                break;
            case STATE.STAYING:
                switch (newState)
                {
                    case STATE.REVOLVING:
                        m_revolvingAngleAcc = 0;
                        m_revolvingSpeedScalar = CalculateRevolvingSpeedScalar();
                        break;
                }
                break;
            case STATE.REVOLVING:
                switch (newState)
                {
                    case STATE.FALLING:
                        m_fallingRound += 1;
                        m_fallingTargetPos = CalculateFallingTargetPos(m_fallingRound);
                        break;
                    case STATE.LANDING:
                        m_fallingRound += 1;
                        m_fallingTargetPos = CalculateFallingTargetPos(m_fallingRound);
                        break;
                }
                break;
            case STATE.END:
                    switch (newState)
                    {
                        case STATE.FALLING:
                        m_fallingTargetPos = CalculateFallingTargetPos(m_fallingRound);
                            break;
                    }
                break;
        }

        m_state = newState;
    }

    private Vector3 CalculateFallingTargetPos(int fallingRound)
    {
        if (fallingRound >= 3)
            return BattleGameObjectMgr.Inst.PlanetPos; // 마지막 추락일때는 행성에 충돌해야한다.

        return m_transform.position + m_fallingDir.normalized * m_fallingDists[fallingRound];
    }

    private float CalculateRevolvingSpeedScalar()
    {
        // 가장 외각의 원의 반지름을 나타낸다. 이때를 기준으로 반지름이 작아질수록 회전각이 더 커지게된다.
        float stdDist = (BattleGameObjectMgr.Inst.PlanetPos - (m_startPos + m_fallingDir.normalized * m_fallingDists[0])).magnitude;

        float curDist = (BattleGameObjectMgr.Inst.PlanetPos - m_transform.position).magnitude;

        if (curDist == 0)
            return 0;

        return 1f; // 테스트용
    }
}
