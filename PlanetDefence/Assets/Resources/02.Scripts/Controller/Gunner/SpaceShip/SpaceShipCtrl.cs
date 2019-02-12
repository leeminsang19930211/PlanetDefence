using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpaceShipCtrl : Gunner 
{
    public enum STATE
    {
        FALLING,
        STAYING,
        REVOLVING,
        LANDING,
        END
    }

    public int m_dropJunk = 0;
    public int m_dropEleCircuit = 0;
    public int m_dropCoin = 0;
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

    private delegate void StateProc();
    private readonly StateProc[] m_stateProcs = new StateProc[(int)STATE.END];

    public float AngleFromPlanetUp
    {
        get
        {
            Vector3 toSpaceShip = m_trsf.position - PlanetCtrl.Inst.Position;

            return MyMath.LeftAngle360(PlanetCtrl.Inst.Up, toSpaceShip);           
        }
    }

    // 자식 우주선에서 호출해줄것
    protected new void Init()
    {
        base.Init();

        m_stateProcs[(int)STATE.FALLING] = StateProc_Falling;
        m_stateProcs[(int)STATE.STAYING] = StateProc_Staying;
        m_stateProcs[(int)STATE.REVOLVING] = StateProc_Revolving;
        m_stateProcs[(int)STATE.LANDING] = StateProc_Landing;

        m_startPos = m_trsf.position;

        ChangeState(STATE.FALLING);
    }

    protected override void _OnZeroHP()
    {
        UpdateRsrc();
        Die();
    }

    protected override void _OnDying()
    {        
        BattleGameObjectMgr.Inst.AddDestroyedEnemy(1);
    }

    protected override void CreateBullet()
    {
        if (m_state == STATE.FALLING || m_state == STATE.LANDING)
            return;

        Gunner target = TurretMgr.Inst.FindFirstTargetInFan(transform.position, m_fanAngle);

        Vector3 angle = m_trsf.localEulerAngles;

        angle.z += 180f;

        BulletMgr.Inst.FireBullet(m_bulletPool, BulletPoolIdx, m_trsf.position + m_trsf.up * -1f* m_fireDistAlignUp, angle,this, target);
    }

    // 업데이트에서 호출해줄 것 . 호출해주면 떨어지고 공전하면서 움직이게 된다
    protected void MoveBody()
    {
        m_stateProcs[(int)m_state]();
    }

    protected void UpdateRsrc()
    {
        Player.Inst.AddJunk(m_dropJunk);
        Player.Inst.AddEleCircuit(m_dropEleCircuit);
        Player.Inst.AddCoin(m_dropCoin);
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

    private void StateProc_Falling()
    {
        float curMoveDist = m_fallingSpeed * Time.deltaTime;
        Vector3 left = m_fallingTargetPos - m_trsf.position;

        if (curMoveDist < left.magnitude)
        {
            m_trsf.position += m_fallingDir.normalized * curMoveDist;
        }
        else
        {
            m_trsf.position += left;
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
            transform.RotateAround(PlanetCtrl.Inst.Position, Vector3.forward, curAngle);           
        }
        else
        {
            transform.RotateAround(PlanetCtrl.Inst.Position, Vector3.forward,  360f- m_revolvingAngleAcc);

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
        Vector3 left = m_fallingTargetPos - m_trsf.position;

        if (curMoveDist < left.magnitude)
        {
            m_trsf.position += m_fallingDir.normalized * curMoveDist;
        }
        else
        {
            m_trsf.position += left;
        }
    }

    private Vector3 CalculateFallingTargetPos(int fallingRound)
    {
        if (fallingRound >= 3)
            return PlanetCtrl.Inst.Position; // 마지막 추락일때는 행성에 충돌해야한다.

        return m_trsf.position + m_fallingDir.normalized * m_fallingDists[fallingRound];
    }

    private float CalculateRevolvingSpeedScalar()
    {
        // 가장 외각의 원의 반지름을 나타낸다. 이때를 기준으로 반지름이 작아질수록 회전각이 더 커지게된다.
        float stdDist = (PlanetCtrl.Inst.Position - (m_startPos + m_fallingDir.normalized * m_fallingDists[0])).magnitude;

        float curDist = (PlanetCtrl.Inst.Position - m_trsf.position).magnitude;

        if (curDist == 0)
            return 0;

        return 1f; // 테스트용
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PLANET")
        {
            Destroy(gameObject);
        }
    }
}
