﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CreditsController : MonoBehaviour
{
    [SerializeField] GameObject prompt;
    [SerializeField] GameObject names, headings, title;
    [SerializeField] AudioSource music_source;
    [SerializeField] float end_y_value;
    [SerializeField] float start_y_value;
    [SerializeField] float default_speed;
    
    public Text score;
    public Text deaths;

    private float speed;

    private bool prompt_visible;

    private void Start()
    {
        speed = default_speed;
        prompt.SetActive(false);
        prompt_visible = false;

        score.text = "Score: " + GameData.score;
        deaths.text = "Deaths: " + GameData.deaths;

        transform.localPosition = new Vector3(transform.localPosition.x, start_y_value, transform.localPosition.z);
    }

    private void Update()
    {
        if (transform.localPosition.y < end_y_value)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y + Time.deltaTime * speed, transform.localPosition.z);
        }
        else
        {
            names.SetActive(false);
            headings.SetActive(false);
            title.SetActive(false);
            StartCoroutine(DelayShowPrompt());
        }

        HandleInput();
    }

    private void HandleInput()
    {
        if (prompt_visible && Input.anyKeyDown)
        {
            music_source.Stop();
            GameData.scoreBeforeLevelStart = 0;
            GameData.score = 0;
            GameData.deaths = 0;
            GameData.current_level = 1;
            GameData.sheepLivesRemaining = GameData.sheepLivesInitial;
            SceneManager.LoadScene(Constants.MAIN_MENU_SCENE_NAME);
        }
    }

    private IEnumerator DelayShowPrompt()
    {
        yield return new WaitForSeconds(2);
        prompt.SetActive(true);
        prompt_visible = true;
    }
}
