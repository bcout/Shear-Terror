using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    public Vector3 move_min;
    public Vector3 move_max;
    public Vector3 rotation;
    public float speed;

    private float t;
    private bool moving;
    private GameObject sheep;
    private SheepController sheep_controller;
    private RagdollController ragdoll_controller;
    private SoundEffectPlayer sfx_player;

    // Start is called before the first frame update
    void Start()
    {
        LoadComponents();
        moving = false;
    }

    private void LoadComponents()
    {
        sheep = GameObject.Find("Sheep");
        sheep_controller = sheep.GetComponent<SheepController>();
        sfx_player = sheep.GetComponent<SoundEffectPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!moving)
        {
            StartCoroutine(MoveBackAndForth(Vector3.zero, move_min, move_max, Vector3.zero, rotation));
        }
    }

    private IEnumerator MoveBackAndForth(Vector3 move_origin, Vector3 move_min, Vector3 move_max, Vector3 rotation_origin, Vector3 rotation_max)
    {
        if (move_origin != move_max)
        {
            yield return MoveTo(move_origin, move_max, rotation_origin, rotation_max);
            yield return MoveTo(move_max, move_origin, rotation_max, rotation_origin);
        }
        if (move_origin != move_min)
        {
            yield return MoveTo(move_origin, move_min, rotation_max, rotation_origin);
            yield return MoveTo(move_min, move_origin, rotation_origin, rotation_max);
        }
    }

    private IEnumerator MoveTo(Vector3 move_a, Vector3 move_b, Vector3 rotation_a, Vector3 rotation_b)
    {
        moving = true;
        Quaternion initial = Quaternion.identity;
        
        while(t < 1)
        {
            t += Time.deltaTime * speed;

            transform.localPosition = Vector3.Lerp(move_a, move_b, t);
            Vector3 current_rotation = Vector3.Lerp(rotation_a, rotation_b, t);
            transform.localRotation = initial * Quaternion.Euler(current_rotation);

            yield return new WaitForEndOfFrame();
        }

        t = 0f;
        moving = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "Sheep")
        {
            sheep_controller.SetState(sheep_controller.GetRagdollState());
            sheep_controller.SetCollidedObstacle(gameObject);
            sfx_player.PlayThudSound();
        }
    }
}
