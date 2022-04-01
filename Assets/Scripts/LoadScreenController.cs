using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class LoadScreenController : MonoBehaviour
{
    // public GameObject loadingPanel;
    public TwoDSheepController TwoDSheepCtrl;
    public Slider loadingBar;
    private AsyncOperation op;
    private int duration;
    private Int64 start;
    private Int64 now;
    private float fakeProgress;
    private float oldFakeProgress;

    public void LoadLevel (string levelName)
    {
        // loadingPanel.SetActive(true);
        duration = UnityEngine.Random.Range(3000, 6000);
        start = DateTimeOffset.Now.ToUnixTimeMilliseconds();
        TwoDSheepCtrl.placeDaSheep();
        StartCoroutine(LoadSceneAsync(levelName));
    }

    IEnumerator LoadSceneAsync ( string levelName )
    {
        op = SceneManager.LoadSceneAsync(levelName);
        op.allowSceneActivation = false;
        
        while (!op.isDone)
        {
            if (fakeProgress < 1f){
                now = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                oldFakeProgress = fakeProgress;
                fakeProgress = (float) (now - start) / (duration);
                TwoDSheepCtrl.MoveDaSheep(fakeProgress - oldFakeProgress);
                loadingBar.value = fakeProgress;
            }
            else{
                op.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}
