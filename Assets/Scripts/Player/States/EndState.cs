using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class EndState : MonoBehaviour, SheepState
{
    private SheepController sheep_controller;
    private MovementController movement_controller;
    private GameObject sheep_camera;
    private GameObject end_camera;
    private string current_animation;

    public void StateUpdate()
    {
        //sheep_camera.transform.position = current_camera_position;
    }

    public void Enter()
    {
        LoadComponents();
        sheep_controller.GetMusicPlayer().StopMusic();
        end_camera = Instantiate(sheep_camera);
        end_camera.transform.position = sheep_camera.transform.position;
        end_camera.transform.rotation = sheep_camera.transform.rotation;
        sheep_camera.SetActive(false);

        current_animation = Constants.RUN_ANIM;
        sheep_controller.StartAnimation(current_animation);
        StartCoroutine(End());
    }

    public void Exit()
    {
        sheep_controller.StopAnimation(current_animation);
        sheep_camera.SetActive(true);
        Destroy(end_camera);
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
        sheep_controller.StopAnimation(current_animation);
        current_animation = Constants.IDLE_ANIM;
        sheep_controller.StartAnimation(current_animation);
    }
}
