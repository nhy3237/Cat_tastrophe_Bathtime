using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDCenter : MonoBehaviour
{
    [Header("일시정지")]
    [SerializeField] public PausePopUp pausePopUp;

    [Header("정지 오브젝트")]
    [SerializeField] public GameObject cameraRotate;
    [SerializeField] public GameObject inputHandle;

    [Header("플레이어 스탯")]
    [SerializeField] public PlayerStats playerStats;

    [Header("플레이어 무기")]
    [SerializeField] public Weapon soapRifle;
    [SerializeField] public Weapon splashBuster;
    [SerializeField] public Weapon bubbleGun;

    [Header("플레이어 HUD")]
    [SerializeField] public HPObserver hpObserver;
    [SerializeField] public RollObserver rollObserver;

    [SerializeField] public WeaponSelection WeaponSelection;

    [Header("보스 파츠별 충돌 확인")]
    [SerializeField] public List<PartsSubject> partsSubjects;
    [Header("보스 스탯")]
    [SerializeField] public CatStatsSubject catStatsSubject;

    [Header("보스 HUD")]
    //[SerializeField] public CleanlinessObserver cleanlinessObserver;
    [SerializeField] public LikeabilityObserver likeabilityObserver;
    [SerializeField] public LikeabilityObserver likeabilityPopUpObserver;

    private void Start() 
    {
        playerStats.AddObserver<IObserver>(playerStats.hpObserverList,hpObserver);
        playerStats.AddObserver<IObserver>(playerStats.rollObserverList,rollObserver);

        foreach (var partsSubject in partsSubjects)
        {
            partsSubject.AddObserver<IObserver>(partsSubject.observers, catStatsSubject);
        }

        catStatsSubject.AddObserver<IObserver>(catStatsSubject.likeabilityObservers, likeabilityObserver);
        catStatsSubject.AddObserver<IObserver>(catStatsSubject.likeabilityObservers, likeabilityPopUpObserver);

        pausePopUp.OnPausePopupTrue += PauseTrue;
        pausePopUp.OnPausePopupFalse += PauseFalse;
    }

    public void UpdateBullet()
    {
        
    }

    public void PauseTrue()
    {
        PlayerPrefs.SetInt("Pause", 1);
        cameraRotate.SetActive(false);
        inputHandle.SetActive(false);
    }

    public void PauseFalse()
    {
        PlayerPrefs.SetInt("Pause", 0);
        cameraRotate.SetActive(true);
        inputHandle.SetActive(true);
    }
}
