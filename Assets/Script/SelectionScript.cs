using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectionScript : MonoBehaviour
{
    [SerializeField]
    GameObject SelectionScreen,AnswerScreen,BackScreen,AllQuestionWinScreen,WinScreen;
    [SerializeField]
    Categories[] categories, FiledName;
    [SerializeField]
    TextMeshProUGUI[] OptionTxt;
    [SerializeField]
    Button[] AllBtn;
    [SerializeField]
    TextMeshProUGUI QuestionTxt;
    int choice;
    int count;
    int UnlockInt;
    [SerializeField]
    Image SliderObject;
    [SerializeField]
    float speed;
    bool flag;
    [SerializeField]
    TextMeshProUGUI CounterTxt;


    [SerializeField]
    GameObject[] AllCounterObject;
    float Counter;
    float val;
    [SerializeField]
    TextMeshProUGUI CoinTxt;
    int CoinVal;
    [SerializeField]
    Button Musicbtn, Soundbtn ;
    [SerializeField]
    Sprite Musicoff, Musicon;
    [SerializeField]
    Sprite Soundon, Soundoff;
    [SerializeField]
    AudioSource musicSource, soundsource;
    bool MusicFlag;
    bool Soundflag;
    [SerializeField]
    AudioClip[] AllBtnSound;

    private void Start()
    {
        UnlockFill();
        CoinVal = PlayerPrefs.GetInt("coin", 0);
        CoinTxt.text = CoinVal.ToString();
        if (MusicScript.instance.music)
        {
            Musicbtn.GetComponent<Image>().sprite = Musicon;
            musicSource.mute = false;
        }
        else
        {
            Musicbtn.GetComponent<Image>().sprite = Musicoff;
            musicSource.mute = true;
        }
        if (MusicScript.instance.sound)
        {
            Soundbtn.GetComponent<Image>().sprite = Soundon;
            soundsource.clip = AllBtnSound[0];
            soundsource.mute = false;
        }
        else
        {
            Soundbtn.GetComponent<Image>().sprite = Soundoff;
            soundsource.clip= AllBtnSound[0];
            soundsource.mute = true;
        }
    }
    private void Update()
    {
        if(flag)
        {
            if(SliderObject.fillAmount<1)
            {
                SliderObject.fillAmount=SliderObject.fillAmount+speed*Time.deltaTime;
                val=Mathf.Round(SliderObject.fillAmount * 10.0f) * 0.1f;
                CounterTxt.text=(10-(val*10)).ToString("0");
               
            }
            else
            {
                BackScreen.SetActive(true);
                Debug.Log("Game Over");
                flag = false;
            }
        }
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (SelectionScreen.activeInHierarchy)
            {
                SceneManager.LoadScene(0);

            }
            else if(AnswerScreen.activeInHierarchy)
            {
                AnswerScreen.SetActive(false);
                SelectionScreen.SetActive(true);
            }
            else if(AllQuestionWinScreen.activeInHierarchy)
            {
                AllQuestionWinScreen.SetActive(false);
                SelectionScreen.SetActive(true);
            }
        }
    }
    public void SlectionBtn(int value)
    {
        choice = value;
        SelectionScreen.SetActive(false);
        AnswerScreen.SetActive(true);
        SetData();
        flag = true;
    }
     void SetData()
    {
        SliderObject.fillAmount = 0;
        count=PlayerPrefs.GetInt(choice.ToString(),0);
        QuestionTxt.text= categories[choice].allQuestion[count].Question;
        for (int j = 0; j < categories[choice].allQuestion[count].Option.Length; j++)
            {
            OptionTxt[j].text = categories[choice].allQuestion[count].Option[j].optName;
           
            }
        Debug.Log(categories[choice].allQuestion[count].Answer);
    }



    public void SelectionScreenBackBtnClickAction()
    {
        StartCoroutine(ObjectOn());
    }
    IEnumerator ObjectOn()
    {
        yield return new WaitForSeconds(0.3f);
        if(SelectionScreen.activeInHierarchy)
        {
            SceneManager.LoadScene(0);
        }
    }
    public void AnswerScreenToSelectionScreen()
    {
        AnswerScreen.SetActive(false);
        SelectionScreen.SetActive(true);
        flag = false;
    }

    public void checkAns(TextMeshProUGUI Ans)
    {
        string ans = categories[choice].allQuestion[count].Answer;
        ans= ans.Trim();
        string Click=(Ans.text).Trim();
        if (Click== ans)
        {
        
            if (categories[choice].allQuestion.Length - 1 == count)
            {
                flag = false;
                Debug.Log("for this categories all question are done");
                PlayerPrefs.SetInt(choice.ToString(),0);
                AllQuestionWinScreen.SetActive(true);
                AnswerScreen.SetActive(false);
                UnlockInt=PlayerPrefs.GetInt("level",0);
                soundsource.clip = AllBtnSound[1];
                soundsource.Play();
                if (UnlockInt==choice)
                {
                    if(choice<AllBtn.Length-1)
                    {
                        UnlockInt++;
                        PlayerPrefs.SetInt("level",UnlockInt);
                        UnlockFill();
                        
                    }
                    else
                    { 
                        Debug.Log("All level successfully completed");
                    }
                }
            }
            else
            {
                count++;
                PlayerPrefs.SetInt(choice.ToString(), count);
                WinScreen.SetActive(true);
                AnswerScreen.SetActive(false);
                flag = false;
                CoinVal = PlayerPrefs.GetInt("coin", 0);
                CoinVal += 10;
                PlayerPrefs.SetInt("coin", CoinVal);
                CoinTxt.text = CoinVal.ToString();
                Counter= Mathf.Round(10 - (val * 10));
                soundsource.clip = AllBtnSound[1];
                soundsource.Play();
                switch (Counter)
                {
                    case 0:
                    case 1:
                    case 2:
                        AllCounterObject[0].SetActive(true);
                        AllCounterObject[1].SetActive(false);
                        AllCounterObject[2].SetActive(false);
                        break;
                    case 3:
                    case 4:
                    case 5:
                        AllCounterObject[0].SetActive(true);
                        AllCounterObject[1].SetActive(true);
                        AllCounterObject[2].SetActive(false);
                        break;
                    case 6:
                        case 7:
                       
                    case 8:
                        case 9:
                        case 10:
                        AllCounterObject[0].SetActive(true);
                        AllCounterObject[1].SetActive(true);
                        AllCounterObject[2].SetActive(true);
                        break;
                }
            }
        }
        else
        {
            Debug.Log("Game Over");
            BackScreen.SetActive(true);
            AnswerScreen.SetActive (false);
            flag= false;
           soundsource.clip= AllBtnSound[2];
            soundsource.Play();
        }
    }
    public void WinNextQuestion()
    {
        flag= true;
        SliderObject.fillAmount=0;
        WinScreen.SetActive(false);
        AnswerScreen.SetActive(true);
        SetData();
    }
    public void WinScreenHomeClickAction()
    {
        StartCoroutine(WinScenehome());
    }
    IEnumerator WinScenehome()
    {
        yield return new WaitForSeconds(0.3f);
        if (WinScreen.activeInHierarchy)
        {
            SceneManager.LoadScene(0);
        }
    }
    public void UnlockFill()
    {
        PlayerPrefs.SetInt("totallevel", FiledName.Length - 1);
        UnlockInt = PlayerPrefs.GetInt("level", 0);
        for (int i = 0; i <= UnlockInt; i++)
        {
            AllBtn[i].interactable = true;
        }
    }
    public void BackScreenReplayClickAction()
    {
        SliderObject.fillAmount = 0;
        flag = true;
        BackScreen.SetActive(false);
        AnswerScreen.SetActive(true);
    }
    public void BackScreenHomeClickAction()
    {
       StartCoroutine (BackScenehome());
    }
    IEnumerator BackScenehome()
    {
        yield return new WaitForSeconds(0.3f);
        if (BackScreen.activeInHierarchy)
        {
            SceneManager.LoadScene(0);
        }
    }
    public void AllQuestionWinREplyClickAction()
    {
        flag = false;
        AllQuestionWinScreen.SetActive(false);
        SelectionScreen.SetActive(true);
    }
    public void AllQuestionWinHomeClickAction()
    {
        StartCoroutine(AllWinScenehome());
    }
    IEnumerator AllWinScenehome()
    {
        yield return new WaitForSeconds(0.3f);
            SceneManager.LoadScene(0);
    
    }
    public void MusicManagment()
        {
            if (MusicScript.instance.music)
            {
                Musicbtn.GetComponent<Image>().sprite = Musicoff;
                musicSource.mute = true;
                MusicScript.instance.music = false;
            }
            else
            {
                Musicbtn.GetComponent<Image>().sprite = Musicon;
                musicSource.mute = false;
                MusicScript.instance.music = true;
            }
            MusicFlag = !MusicFlag;
        }
        public void SoundManagment()
        {
            if (MusicScript.instance.sound)
            {
                Soundbtn.GetComponent<Image>().sprite = Soundoff;
                soundsource.mute = true;
            soundsource.clip = AllBtnSound[0];
            MusicScript.instance.sound = false;
              
            }
            else
            {
                Soundbtn.GetComponent<Image>().sprite = Soundon;
                soundsource.mute = false;
            soundsource.clip = AllBtnSound[0];
                MusicScript.instance.sound = true;
              
            }
            Soundflag = !Soundflag;
        }
    public void plybtnsound()
    {
        soundsource.clip = AllBtnSound[0];
        soundsource.Play();
    }
}

[System.Serializable]
public class Categories
{
    public string FiledName;
    public Questions[] allQuestion;
}
[System.Serializable]
public class Questions
{
    public string Question;
    public string Answer;
    public Options[] Option;
}
[System.Serializable]
public class Options
{
    public string optName;
}
