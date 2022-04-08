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
    private double fakeProgress;
    private double oldFakeProgress;
    private bool keepGoing;
    private double count;
    private float denom;

    private string levelName;
    public void Start()
    {
        keepGoing = true;
        denom = UnityEngine.Random.Range(0.1f, 0.3f);
        TwoDSheepCtrl.placeDaSheep();
        count = 0.1;
        levelName = "Level " + GameData.current_level;
    }

    public void Update()
    {
        if (keepGoing)
        {
            if (fakeProgress < 1f)
            {
                count += 0.001;
                oldFakeProgress = fakeProgress;
                
                // I mapped this function out in desmos and it works pretty good as a pseudo loading bar progress rate function.
                fakeProgress += (Math.Sin(Math.Pow((count), 5*count)/(denom*count))+1.2)*0.5*0.005;
                
                TwoDSheepCtrl.MoveDaSheep((float)(fakeProgress - oldFakeProgress));
                loadingBar.value = (float)fakeProgress;
            }
            else
            {
                TwoDSheepCtrl.anim.speed = 0f;
                keepGoing = false;
                
                // Actually load the level.
                StartCoroutine(LoadSceneAsync(levelName));
            }
        }
    }

    public void loadScene(string name)
    {
        StartCoroutine(LoadSceneAsync(name));
    }

    IEnumerator LoadSceneAsync (string level)
    {
        op = SceneManager.LoadSceneAsync(level);
        while (!op.isDone)
        {
            yield return null;
        }
    }
}
