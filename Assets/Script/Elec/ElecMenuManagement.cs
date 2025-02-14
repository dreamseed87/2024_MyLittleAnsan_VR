using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ElecMenuManagement : MonoBehaviour    
{
    [SerializeField] GameObject menu; // 일시정지 메뉴
    [SerializeField] GameObject resultMenu; // 결과창
    [SerializeField] GameObject timerUI; // 타이머, 점수 UI
    [SerializeField] GameObject leftRayController;
    [SerializeField] GameObject rightRayController;
    [SerializeField] private OVRControllerHelper controllerHelperLeft;
    [SerializeField] private OVRControllerHelper controllerHelperRight;
    [SerializeField] private GameObject leftSolder;
    [SerializeField] private GameObject rightSolder;
    [SerializeField] TextMeshPro scoreText;
    public GameObject celebratePrefab;
    public AudioSource celebratesound;

    Timer timer;
    private int score;
    private bool isMenuOn = false;
    private bool gameEnd = false; // 게임 시간 종료 여부 확인
    DataManager datamanager;

    bool startgame = false;
    void Start()
    {
        score = 0;
        datamanager = FindAnyObjectByType<DataManager>();
        menu.SetActive(false);
        resultMenu.SetActive(false);
        scoreText.text = "";
        timer = FindAnyObjectByType<Timer>();
        
    }
    void Update()
    {
        if (gameEnd) return;
        gameEnd = timer.GetBool();
        
        if (gameEnd) // 시간 종료 첫 확인 시 실행
        {
            if (score >= 10) //점수가 10점 이상일 때 파티클과 소리가 켜짐
            {
                if (celebratePrefab != null)
                {
                    var particleSystem = celebratePrefab.GetComponent<ParticleSystem>();
                    if (particleSystem != null)
                    {
                        particleSystem.Play();
                        celebratesound.Play();
                    }
                }
                
            }
            StartCoroutine(DelayedResultMenu());
            
        }
        if (!isMenuOn) //메뉴 버튼을 눌렀을 때
        {
            if (OVRInput.GetDown(OVRInput.Button.One) || OVRInput.GetDown(OVRInput.Button.Two))
            {
                OpenMenu();
                rightRayController.SetActive(true);
            }
            else if (OVRInput.GetDown(OVRInput.Button.Three) || OVRInput.GetDown(OVRInput.Button.Four))
            {
                OpenMenu();
                leftRayController.SetActive(true);
            }
        }
        
    }

    private IEnumerator DelayedResultMenu() // 2초대기 후 메뉴 키기
    {
        yield return new WaitForSeconds(2); // 2초 대기
        datamanager.SetClear();
        resultMenu.SetActive(true);
        OpenMenu();
        celebratesound.Pause();
    }
    public void ScorePlus()
    {
        score++;
        scoreText.text = "점수: " + score.ToString();
    }
    public int GetScore()
    {
        return score;
    }
    public void LoadTitle()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Title");
    }
    public void LoadCityScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("CityDesign");
    }
    public void RestartScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Electronic");
    }
    public void EndGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); // 어플리케이션 종료
#endif
    }
    public void CloseMenu() //메뉴 닫을 때
    {
        timerUI.SetActive(true);
        Time.timeScale = 1;
        isMenuOn = false;
        menu.SetActive(false);
        if (startgame)
        {
            leftRayController.SetActive(false);
            rightRayController.SetActive(false);
        }
        leftSolder.gameObject.SetActive(true);
        rightSolder.gameObject.SetActive(true);
        
    }
    private void OpenMenu() //메뉴 열 때
    {
        timerUI.SetActive(false);
        Time.timeScale = 0;
        isMenuOn = true;
        menu.SetActive(true);
        leftRayController.SetActive(true);
        rightRayController.SetActive(true);
        leftSolder.gameObject.SetActive(false);
        rightSolder.gameObject.SetActive(false);
       
    }

    public void StartGame()
    {
        startgame = true;
    }



}