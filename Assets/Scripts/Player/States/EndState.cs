using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class EndState : MonoBehaviour, SheepState
{
    private SheepController sheep_controller;
    private MovementController movement_controller;
    private GameObject sheep_camera;
    private GameObject end_camera;
    private bool at_end;

    public void StateUpdate()
    {
        if (at_end)
        {
            movement_controller.RunIntoDistance();
            GameData.footstep_sound_enabled = false;
        }
    }

    public void Enter()
    {
        LoadComponents();
        at_end = false;
        sheep_controller.GetMusicPlayer().StopMusic();
        end_camera = Instantiate(sheep_camera);
        end_camera.transform.position = sheep_camera.transform.position;
        end_camera.transform.rotation = sheep_camera.transform.rotation;
        sheep_camera.SetActive(false);

        sheep_controller.StartAnimation(Constants.RUN_ANIM);
        StartCoroutine(End());
    }

    public void Exit()
    {
        sheep_controller.StopAnimation(Constants.RUN_ANIM);
        sheep_camera.SetActive(true);
        Destroy(end_camera);
        at_end = false;
    }

    private void LoadComponents()
    {
        sheep_controller = GetComponent<SheepController>();
        movement_controller = GetComponent<MovementController>();
        sheep_camera = sheep_controller.GetComponentInChildren<Camera>().gameObject;
    }

    private IEnumerator End()
    {
        yield return StartCoroutine(movement_controller.MoveToEnd());
        at_end = true;
    }
}
