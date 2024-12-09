using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static Unity.IO.LowLevel.Unsafe.AsyncReadManagerMetrics;

public class MainScript : MonoBehaviour
{
    [SerializeField]
    GameObject MainScreen, SettingScreen, HelpScreen,LoadingScreen,ExitScreen;
    [SerializeField]
    Slider sliderobject;
    [SerializeField]
    float speed;
    bool flag;
    [SerializeField]
    Button Musicbtn, Soundbtn;
    [SerializeField]
    Sprite Musicoff, Musicon;
    [SerializeField]
    Sprite Soundon, Soundoff;
    [SerializeField]
    AudioSource musicSource, soundsource;
    bool MusicFlag;
    bool Soundflag;
    private void Start()
    {
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
            soundsource.mute = false;
        }
        else
        {
            Soundbtn.GetComponent<Image>().sprite = Soundoff;
            soundsource.mute = true;

        }
    }
    void Update()
    {
        if (flag == true)
        {
            if (sliderobject.value < sliderobject.maxValue)
            {
                sliderobject.value = sliderobject.value + speed * Time.deltaTime;
            }
            else
            {
                SceneManager.LoadScene(1);
            }
        }
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (MainScreen.activeInHierarchy)
            {
                MainScreen.SetActive(false);
                ExitScreen.SetActive(true);

            }
            else
            {
                MainScreen.SetActive(true);
                ExitScreen.SetActive(false);
            }
        }
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (SettingScreen.activeInHierarchy)
            {
                SettingScreen.SetActive(false);
                MainScreen.SetActive(true);

            }
        }
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (HelpScreen.activeInHierarchy)
            {
                HelpScreen.SetActive(false);
                MainScreen.SetActive(true);

            }
        }
    }

    public void MainScreenToLoadingScreen()
    {
        flag = true;
        MainScreen.SetActive(false);
        LoadingScreen.SetActive(true);
    }

    public void SettingClickAction()
    {
        MainScreen.SetActive(false);
        SettingScreen.SetActive(true);
    }

    public void SettingScreenCancelClickAction()
    {
        SettingScreen.SetActive(false);
        MainScreen.SetActive(true);
    }

    public void HelpScreenClickAction()
    {
        MainScreen.SetActive(false);
        HelpScreen.SetActive(true);
    }

    public void HelpScreenCancelClickAction()
    {
        HelpScreen.SetActive(false);
        MainScreen.SetActive(true);
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
            MusicScript.instance.sound = false;

        }
        else
        {
            Soundbtn.GetComponent<Image>().sprite = Soundon;
            soundsource.mute = false;
            MusicScript.instance.sound = true;
        }
        Soundflag = !Soundflag;
    }
    public void plybtnsound(AudioClip Sclip)
    {
        soundsource.clip = Sclip;
        soundsource.Play();
    }

    public void ExitPanelYesBtnClickAction()
    {
        Application.Quit();
    }
    public void ExitPanelNoBtnClickAction()
    {
        ExitScreen.SetActive(false);
        MainScreen.SetActive(true);
    }
}
