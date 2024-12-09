using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DemoScript : MonoBehaviour
{
    [SerializeField]
    GameObject DemoScreen, DScreen;
    [SerializeField]
    Button[] AlllevelBtn;
    int UnlockInt;
    int totallvl;
    
    void Start()
    {
        PlayerPrefs.SetInt("totallevel",AlllevelBtn.Length-1);
        UnlockInt = PlayerPrefs.GetInt("level", 0);
        for (int i = 0; i <= UnlockInt; i++)
        {
            AlllevelBtn[i].interactable = true;
            AlllevelBtn[i].transform.GetChild(1).gameObject.SetActive(false);
        }
        totallvl = PlayerPrefs.GetInt("totallevel", 0);
    }

    public void startlvl(int no)
    {
        currentlevel = no;
       // PlayerPrefs.SetInt("currentlvl", no);
        DemoScreen.SetActive(false);
        DScreen.SetActive(true);
    }
    
  
    public void scenechange()
    {
        DScreen.SetActive(false);
        DemoScreen.SetActive(true);
    }
    int currentlevel;
    public void Unlockchange()
    {
       // no = PlayerPrefs.GetInt("currentlvl", 0);
        UnlockInt = PlayerPrefs.GetInt("level", 0);
        if (UnlockInt == currentlevel)
        {
            if (currentlevel < totallvl)
            {
                UnlockInt++;
                PlayerPrefs.SetInt("level", UnlockInt);
                for(int i = 0;i <= UnlockInt;i++)
                {
                    AlllevelBtn[i].interactable=true;
                    AlllevelBtn[i].transform.GetChild (1).gameObject.SetActive(false);
                    scenechange();
                }
            }
            else
            {
                Debug.Log("All Successfully completed level");
            }
        }
    }
}

