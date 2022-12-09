using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.PostProcessing;

public class MainMenu : MonoBehaviour {


    public PostProcessingBehaviour ppb;
    PostProcessingProfile mProfile;
    public float transitionSpeed;
    public GameObject MM;
    public GameObject PAUSE;
    public GameObject STAGE;
    public StageClearAnim animStage;
    float startflou;
    public StageManager SM;
    public TomatoSpawner TomatoSpawner;
    public Text stageText;
    public static int currentStage = 0;


   
    public enum STATE
    {
        MM,
        PA,
        ST,
        NONE
    }
    public static STATE currentState;
	// Use this for initialization
	void Start () {
        mProfile = Instantiate(ppb.profile);
        ppb.profile = mProfile;
        startflou = mProfile.depthOfField.settings.focusDistance;
	}
	public void OnClickContinue()
    {
        floute(false);
        ChangePanel(STATE.NONE);
        SetTime(true);
    }

	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Escape) && currentState != STATE.MM && currentState != STATE.ST)
        {
            if (Input.GetKeyDown(KeyCode.Escape) && currentState == STATE.PA)
            {
                OnClickContinue();
            }
            else
            {
                SetTime(false);
                floute(true);
                ChangePanel(STATE.PA);
            }
        }
	}
    public void ChangePanel(STATE c)
    {
        MM.SetActive(false);
        PAUSE.SetActive(false);
        STAGE.SetActive(false);
        currentState = c;    
        switch(c)
        {
            case STATE.MM:
                MM.SetActive(true);
                break;
            case STATE.PA:
                PAUSE.SetActive(true);
                break;
            case STATE.ST:
                STAGE.SetActive(true);
                break;
            case STATE.NONE:
                break;
            default:
                MM.SetActive(true);
                break;
        }
    }
 
    public void OnClickPlay()
    {
        floute(false);
        MM.SetActive(false);
        currentState = STATE.NONE;
        TomatoSpawner.KillGuards();
        SM.ResetEverything();

        TomatoSpawner.StartCoroutine("spawner");
    }
    void floute(bool isflootintheend)
    {
        StopCoroutine("UnFlou");
        StopCoroutine("ReFlou");
        if (isflootintheend)
            StartCoroutine("ReFlou");
        else
            StartCoroutine("UnFlou");
    }
    void SetTime(bool isnatural)
    {
        StopCoroutine("StopTime");
        StopCoroutine("ReTime");
        if (isnatural)
            StartCoroutine("ReTime");
        else
            StartCoroutine("StopTime");
    }
    public void OnClickNextStage()
    {
        STAGE.SetActive(false);
        OnClickPlay();
    }

    public void SetVictoryPanel()
    {
        ++currentStage;
        stageText.text = "Stage " + currentStage + " Clear !";
        TomatoSpawner.StopCoroutine("spawner");
        StartCoroutine("littleWait");
    }
    IEnumerator littleWait()
    {
        yield return new WaitForSeconds(2);
        currentState = STATE.ST;
        floute(true);
        animStage.PlayAnim();
    }

    IEnumerator StopTime()
    {
        print("stop");
        float i = 0;
        while(i < 1)
        {
            i += Time.unscaledDeltaTime * 4;
            Time.timeScale = Mathf.Lerp(1, 0, i);
            yield return null;
        }
    }
    IEnumerator ReTime()
    {
        print("re");
        float i = 0;
        while (i < 1)
        {
            i += Time.unscaledDeltaTime * 4;
            Time.timeScale = Mathf.Lerp(0, 1, i);
            yield return null;
        }
    }

    IEnumerator ReFlou()
    {
        float i = 0;
        float end = 8.0f;
        var dep = mProfile.depthOfField.settings;
        mProfile.depthOfField.enabled = true;
        while (i < 1)
        {
            i += Time.unscaledDeltaTime * transitionSpeed;
            dep.focusDistance = Mathf.Lerp(end, startflou, i);
            mProfile.depthOfField.settings = dep;
            yield return null;
        }
    }
    IEnumerator UnFlou()
    {
        float i = 0;
        float end = 8.0f;
        var dep = mProfile.depthOfField.settings;
        mProfile.depthOfField.enabled = true;
        while (i < 1 )
        {
            i += Time.unscaledDeltaTime * transitionSpeed;
            dep.focusDistance = Mathf.Lerp(startflou, end, i);
            mProfile.depthOfField.settings = dep;
            yield return null;
         }
        mProfile.depthOfField.enabled = false;
    }
    public void OnClickQuit()
    {
        Application.Quit();
    }
    public void WaitToChange(float sec)
    {
        currentStage = 0;

        TomatoSpawner.StopCoroutine("spawner");
        StartCoroutine(Wait(sec));
    }
    IEnumerator Wait(float sec)
    {
        yield return new WaitForSeconds(sec);
        floute(true);
        yield return new WaitForSeconds(0.25f);
        ChangePanel(MainMenu.STATE.MM);
    }
}
