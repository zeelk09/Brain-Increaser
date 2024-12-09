using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class DemoScript2 : MonoBehaviour
{
    int UnlockInt;
    int totallvl;
    int currentlevel;
    private void Start()
    {
        totallvl = PlayerPrefs.GetInt("totallevel", 0);
    }
     public void scenechange()
    {
        SceneManager.LoadScene(2);
    }

    public void Unlockchange()
    {
        currentlevel = PlayerPrefs.GetInt("currentlvl", 0);
        UnlockInt = PlayerPrefs.GetInt("level", 0);
        if(UnlockInt==currentlevel)
        {
            if(currentlevel<totallvl)
            {
                UnlockInt++;
                PlayerPrefs.SetInt("level", UnlockInt);
            }
            else
            {
                Debug.Log("All Successfully completed level");
            }
        }
    }
}
