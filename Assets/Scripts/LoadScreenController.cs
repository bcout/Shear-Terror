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
    private double fakeProgress;
    private double oldFakeProgress;
    private bool keepGoing;
    private double move;
    private double count;
    private float denom;

    private string levelName = "Level";
    public void Start()
    {
        keepGoing = true;
        denom = UnityEngine.Random.Range(0.1f, 0.3f);
        start = DateTimeOffset.Now.ToUnixTimeMilliseconds();
        TwoDSheepCtrl.placeDaSheep();
        // start = 0;
        count = 0.1;
        // TwoDSheepCtrl.anim.speed = 2f;
    }

    public void Update()
    {
        if (keepGoing)
        {
            if (fakeProgress < 1f)
            // if(count < 2)
            {
                count += 0.001;
                oldFakeProgress = fakeProgress;
                // now = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                fakeProgress += (Math.Sin(Math.Pow((count), 5*count)/(denom*count))+1.2)*0.5*0.005;
                TwoDSheepCtrl.MoveDaSheep((float)(fakeProgress - oldFakeProgress));
                loadingBar.value = (float)fakeProgress;
                // Delay((float)(Math.Sin(Math.Pow(((double) (now - start) / (duration)) * 10.0, UnityEngine.Random.Range(0.1f, 0.14f)))*0.1));
            }
            else
            {
                TwoDSheepCtrl.anim.speed = 0f;
                keepGoing = false;
                StartCoroutine(LoadSceneAsync(levelName));
            }
        }
    }

    void OnEnable()
    {
        Debug.Log("OnEnable called");
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("OnSceneLoaded: " + scene.name);
        Debug.Log(mode);
        // LoadLevel("Level");
    }
    
    
    IEnumerator Delay(float secs)
    {
        yield return new WaitForSeconds(secs);
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
