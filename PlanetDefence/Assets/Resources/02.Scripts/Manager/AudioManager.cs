using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public enum eBGM
    {
        LobbyBGM,
        ChoiceBGM,
        BattleBGM,
        Max
    }

    public enum eSelectSFX
    {
        ButtonSFX,
        CardSFX,
        Max
    }

    public enum eBulletSFX
    {
        MissileSFX,
        LaserSFX,
        GatlingSFX,
        PoisonSFX,
        PauseSFX,
        SlowSFX,
        SniperSFX,
        HealSFX,
        KingSlayerSFX,
        Max
    }

    public enum eSpaceshipSFX
    {
        NomalSFX,
        ZombieSFX,
        GhostSFX,
        BattleSFX,
        Max
    }

    public enum eExplosionSFX
    {
        SpaceshipBoomSFX,
        TurretBoomSFX,
        Max
    }

    public enum eUnlockSFX
    {
        BluePrintSFX,
        ResearchSFX,
        Max
    }

    public enum eClearSFX
    {
        ClearSFX,
        FailedSFX,
        Max
    }


    private static AudioManager m_inst = null;

    public static AudioManager Inst
    {
        get
        {
            if (m_inst == null)
            {
                Debug.Log("AudioManager is Null");
            }

            return m_inst;
        }
    }

    void Awake()
    {
        m_inst = this;
        DontDestroyOnLoad(this.gameObject);
       
    }

    [SerializeField] private AudioMixerGroup BGMmixer = null;
    [SerializeField] private AudioMixerGroup SFXmixer = null;
    [SerializeField] private AudioClip[] BGM = new AudioClip[(int)eBGM.Max];
    [SerializeField] private AudioClip[] SelectSFX = new AudioClip[(int)eSelectSFX.Max];
    [SerializeField] private AudioClip[] BulletSFX = new AudioClip[(int)eBulletSFX.Max];
    [SerializeField] private AudioClip[] SpaceshipSFX = new AudioClip[(int)eSpaceshipSFX.Max];
    [SerializeField] private AudioClip[] ExplosionSFX = new AudioClip[(int)eExplosionSFX.Max];
    [SerializeField] private AudioClip[] UnlockSFX = new AudioClip[(int)eUnlockSFX.Max];
    [SerializeField] private AudioClip[] ClearSFX = new AudioClip[(int)eClearSFX.Max];
    private AudioSource BGMsource;
    private AudioSource SFXsource;
   
    void OnEnable()
    {
        // 설정으로 볼률설정하면 사용
        float BGMvolume = 1;
        float SFXvolume = 1;

        BGMsource = gameObject.AddComponent<AudioSource>();
        BGMsource.outputAudioMixerGroup = BGMmixer;
        BGMsource.volume = BGMvolume;
        BGMsource.playOnAwake = false;
        BGMsource.loop = true;

        SFXsource = gameObject.AddComponent<AudioSource>();
        SFXsource.outputAudioMixerGroup = SFXmixer;
        SFXsource.volume = SFXvolume;
        SFXsource.playOnAwake = false;
        SFXsource.loop = false;     
    }

    public void PlayBGM(eBGM eBGM)
    {
        BGMsource.clip = BGM[(int)eBGM];
        BGMsource.Play();
    }

    public void playSelectSFX(eSelectSFX eSelect)
    {
        SFXsource.PlayOneShot(SelectSFX[(int)eSelect]);
    }

    public void playBulletSFX(eBulletSFX eBullet)
    {
        SFXsource.PlayOneShot(BulletSFX[(int)eBullet]);
    }

    public void playSpaceShipSFX(eSpaceshipSFX eSpaceship)
    {
        SFXsource.PlayOneShot(SpaceshipSFX[(int)eSpaceship]);
    }

    public void playExplosionSFX(eExplosionSFX eExplosion)
    {
        SFXsource.PlayOneShot(ExplosionSFX[(int)eExplosion]);
    }

    public void playUnlockSFX(eUnlockSFX eUnlock)
    {
        SFXsource.PlayOneShot(UnlockSFX[(int)eUnlock]);
    }

    public void playClearSFX(eClearSFX eClear)
    {
        SFXsource.PlayOneShot(ClearSFX[(int)eClear]);
    }

    // 추가
    public void playBulletSFX(Vector3 worldPos, eBulletSFX eBullet)
    {
        if (CheckViewPortOut(worldPos))
            return;

        SFXsource.PlayOneShot(BulletSFX[(int)eBullet]);
    }

    public void playSpaceShipSFX(Vector3 worldPos, eSpaceshipSFX eSpaceship)
    {
        if (CheckViewPortOut(worldPos))
            return;

        SFXsource.PlayOneShot(SpaceshipSFX[(int)eSpaceship]);
    }

    public void playExplosionSFX(Vector3 worldPos, eExplosionSFX eExplosion)
    {
        if (CheckViewPortOut(worldPos))
            return;

        SFXsource.PlayOneShot(ExplosionSFX[(int)eExplosion]);
    }

    private bool CheckViewPortOut(Vector3 worldPos)
    {
        Vector2 viewPos = Camera.main.WorldToViewportPoint(worldPos);

        return viewPos.x < -0.5f || viewPos.y < -0.5f || viewPos.x > 1.5f || viewPos.y > 1.5f; // 0.5 는 여유분. 화면 밖에 약간 벗어나도 들리도록.
    }
}
