using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroMovieController : MonoBehaviour
{
    private const int NUM_SCENES = 8;

    [SerializeField] private GameObject[] scene_0_objects;
    [SerializeField] private GameObject[] scene_1_objects;
    [SerializeField] private GameObject[] scene_2_objects;
    [SerializeField] private GameObject[] scene_3_objects;
    [SerializeField] private GameObject[] scene_4_objects;
    [SerializeField] private GameObject[] scene_5_objects;
    [SerializeField] private GameObject[] scene_6_objects;
    [SerializeField] private GameObject[] scene_7_objects;

    private GameObject[][] scenes;

    private int current_scene;

    // Start is called before the first frame update
    void Start()
    {
        InitScenes();
        current_scene = 0;
        LoadScene(current_scene);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            UnloadScene(current_scene);
            current_scene++;

            if (current_scene >= NUM_SCENES)
            {
                SceneManager.LoadScene(Constants.MAIN_MENU_SCENE_NAME);
            }
            else
            {
                LoadScene(current_scene);
            }
            
        }
    }

    private void InitScenes()
    {
        scenes = new GameObject[][] { scene_0_objects, scene_1_objects, scene_2_objects, scene_3_objects, scene_4_objects, scene_5_objects, scene_6_objects, scene_7_objects };
        for (int i = 0; i < scenes.Length; i++)
        {
            for (int j = 0; j < scenes[i].Length; j++)
            {
                scenes[i][j].SetActive(false);
            }
        }
    }

    private void UnloadScene(int scene_num)
    {
        if (scene_num >= NUM_SCENES || scene_num < 0)
        {
            return;
        }

        for (int i = 0; i < scenes[scene_num].Length; i++)
        {
            scenes[scene_num][i].SetActive(false);
        }
    }

    private void LoadScene(int scene_num)
    {
        if (scene_num >= NUM_SCENES || scene_num < 0)
        {
            return;
        }

        for (int i = 0; i < scenes[scene_num].Length; i++)
        {
            scenes[scene_num][i].SetActive(true);
        }
    }
}
