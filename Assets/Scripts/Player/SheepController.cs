using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepController : MonoBehaviour
{
    [SerializeField] private GameObject forward_camera;
    [SerializeField] private GameObject backwards_camera;
    [SerializeField] private GameObject music_player_object;
    [SerializeField] private GameObject sfx_player_object;
    [SerializeField] private GameObject level_end_field;

    private SheepState state;
    private RunningState running_state;
    private IdleState idle_state;
    private EndState end_state;
    private StartState start_state;
    private JumpingState jumping_state;
    private RagdollState ragdoll_state;

    private MovementController movement_controller;
    private Animator animator;
    private SoundEffectPlayer sfx_player;
    private MusicPlayer music_player;

    private List<GameObject> blocks_in_level;
    private List<GameObject> obstacles_in_level;
    private GameObject current_block;
    private GameObject current_lane;
    private GameObject level_parent;
    private GameObject obstacle_parent;
    private GameObject collided_obstacle;

    private int current_block_index;

    private float vertical_position;

    private void Awake()
    {
        LoadStates();
        LoadComponents();
        SetDefaultState(idle_state);
    }

    private void Start()
    {
        LoadExternalComponents();
        collided_obstacle = null;
        vertical_position = 0f;

        backwards_camera.SetActive(false);
        forward_camera.SetActive(true);
    }

    private void Update()
    {
        state.StateUpdate();
    }

    private void LoadComponents()
    {
        movement_controller = GetComponent<MovementController>();
        animator = GetComponent<Animator>();
    }

    private void LoadExternalComponents()
    {
        sfx_player = sfx_player_object.GetComponent<SoundEffectPlayer>();
        music_player = music_player_object.GetComponent<MusicPlayer>();
    }

    // Called by GameController once the level is generated
    public void StartLevel()
    {
        ReadLevelBlocks();
        ReadLevelObstacles();
        StartFollowingLevel();
        SetState(start_state);
    }

    private void ReadLevelBlocks()
    {
        level_parent = GameObject.Find(Constants.LEVEL_PARENT_NAME);
        blocks_in_level = new List<GameObject>();

        for (int i = 0; i < level_parent.transform.childCount; i++)
        {
            blocks_in_level.Add(level_parent.transform.GetChild(i).gameObject);
        }

        current_block_index = 0;
    }

    private void ReadLevelObstacles()
    {
        obstacle_parent = GameObject.Find(Constants.OBSTACLE_PARENT_NAME);
        obstacles_in_level = new List<GameObject>();

        for (int i = 0; i < obstacle_parent.transform.childCount; i++)
        {
            obstacles_in_level.Add(obstacle_parent.transform.GetChild(i).gameObject);
        }
    }

    private void StartFollowingLevel()
    {
        current_block = blocks_in_level[current_block_index];
        current_lane = current_block.GetComponent<BlockData>().GetLane(PlayerData.curr_lane).gameObject;

        movement_controller.StartFollowingLevel();
    }

    public void DestroyPreviousBlocks()
    {
        if (current_block_index - 2 >= 0)
        {
            GameObject block_to_delete = blocks_in_level[current_block_index - 2];
            if (block_to_delete != null)
            {
                Destroy(block_to_delete);  
            }
        }
        
        if (current_block_index - 3 >= 0)
        {
            GameObject obstacle_to_destroy = obstacles_in_level[current_block_index - 3];
            if (obstacle_to_destroy != null)
            {
                Destroy(obstacle_to_destroy);
            }
        }
    }

    public void SpawnLevelEndField()
    {
        Transform spawn_point = blocks_in_level[blocks_in_level.Count - 1].transform.Find("End");
        Instantiate(level_end_field, spawn_point.position, spawn_point.rotation);
    }

    public void StopAnimation(string anim_name)
    {
        animator.SetBool(Animator.StringToHash(anim_name), false);
    }

    public void StartAnimation(string anim_name)
    {
        animator.SetBool(Animator.StringToHash(anim_name), true);
    }

    #region Vertical position Get/Set Methods
    public float GetVerticalPosition()
    {
        return vertical_position;
    }

    public void SetVerticalPosition(float value)
    {
        vertical_position = value;
    }
    #endregion

    #region Block Get/Set Methods
    public GameObject GetCurrentBlock()
    {
        return current_block;
    }

    public void SetCurrentBlock(GameObject value)
    {
        current_block = value;
    }

    public int GetCurrentBlockIndex()
    {
        return current_block_index;
    }

    public void SetCurrentBlockIndex(int value)
    {
        current_block_index = value;
    }

    public List<GameObject> GetBlocksInLevel()
    {
        return blocks_in_level;
    }
    #endregion

    #region Lane Get/Set Methods
    public GameObject GetCurrentLane()
    {
        return current_lane;
    }

    public void UpdateLane()
    {
        current_lane = current_block.GetComponent<BlockData>().GetLane(PlayerData.curr_lane).gameObject;
    }
    #endregion
    
    #region State Get/Set Methods
    public void SetState(SheepState new_state)
    {
        state.Exit();
        state = new_state;
        state.Enter();
    }

    public SheepState GetState()
    {
        return state;
    }

    public RunningState GetRunningState()
    {
        return running_state;
    }

    public IdleState GetIdleState()
    {
        return idle_state;
    }

    public JumpingState GetJumpingState()
    {
        return jumping_state;
    }

    public RagdollState GetRagdollState()
    {
        return ragdoll_state;
    }

    public StartState GetStartState()
    {
        return start_state;
    }
    
    public EndState GetEndState()
    {
        return end_state;
    }

    private void SetDefaultState(SheepState default_state)
    {
        state = default_state;
        state.Enter();
    }

    private void LoadStates()
    {
        running_state = GetComponent<RunningState>();
        idle_state = GetComponent<IdleState>();
        jumping_state = GetComponent<JumpingState>();
        ragdoll_state = GetComponent<RagdollState>();
        start_state = GetComponent<StartState>();
        end_state = GetComponent<EndState>();
    }
    #endregion

    public void Respawn()
    {
        vertical_position = 0f;
        transform.Find(Constants.PIVOT).transform.localRotation = Quaternion.identity;
        SetState(running_state);
        movement_controller.SetCoroutineAvailability(true);
    }

    public GameObject GetCollidedObstacle()
    {
        return collided_obstacle;
    }

    public void SetCollidedObstacle(GameObject collided)
    {
        collided_obstacle = collided;
    }

    public void LookBack()
    {
        forward_camera.SetActive(false);
        backwards_camera.SetActive(true);
    }

    public void LookForward()
    {
        forward_camera.SetActive(true);
        backwards_camera.SetActive(false);
    }

    public SoundEffectPlayer GetSoundEffectsPlayer()
    {
        return sfx_player;
    }

    public MusicPlayer GetMusicPlayer()
    {
        return music_player;
    }

    private void PlayFootstepSound()
    {
        sfx_player.GetComponent<SoundEffectPlayer>().PlayFootstepSound();
    }
}