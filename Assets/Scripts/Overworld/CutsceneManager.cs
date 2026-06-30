using DG.Tweening;
using Fungus;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneManager : MonoBehaviour
{
    public static CutsceneManager Ins;

    [SerializeField] CinemachineBrain brain;

    [SerializeField] Flowchart chart;

    public Animator camAnim;

    public Animator zombieAnim;

    [SerializeField] OverworldController player;

    [SerializeField] Transform playerReposition;
    [SerializeField] Transform walkTarget;

    [SerializeField] Character heroPlaceholderCharacter;

    private Vector3 smoothDampVelocity = new Vector3(1, 0, 0);

    private bool sequenceDone = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SceneManager.sceneLoaded += OverwriteCutsceneManager;
        if (Ins == null)
        {
            Ins = this;
        }
        else if (Ins != this)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this);
    }

    public void Sequence01()
    {
        chart.ExecuteBlock("CutsceneSequence-0");
    }

    public void Sequence12()
    {
        chart.ExecuteBlock("CutsceneSequence-1");
    }

    public void Sequence23()
    {
        PlayerReposition();
        chart.ExecuteBlock("CutsceneSequence-2");
    }

    public void Sequence34()
    {
        chart.ExecuteBlock("CutsceneSequence-3");
    }

    public void Sequence45()
    {
        chart.ExecuteBlock("CutsceneSequence-4");
    }

    public void Sequence56()
    {
        var seq5 = chart.FindBlock("CutsceneSequence-5").CommandList;
        chart.ExecuteBlock("CutsceneSequence-5");
    }

    public void Sequence67()
    {
        chart.ExecuteBlock("CutsceneSequence-6");
    }

    private void PlayerToCamera()
    {
        WalkingScene();

    }

    private void BackToIdle()
    {
        player.BackToIdle();
    }

    public void WalkingScene()
    {
        Debug.Log("Walking Scene");
        player.PlayCutsceneWalk(); 
        player.gameObject.transform.DOMove(walkTarget.position, 2).onComplete = BackToIdle;
    }

    private void PlayerReposition()
    {
        player.gameObject.transform.position = playerReposition.position;
        player.gameObject.transform.LookAt(zombieAnim.transform.position);
    }

    public void ExitCutscene()
    {
        Debug.Log("Cutscene Manager Exiting Cutscene");
        camAnim.enabled= false;
        sequenceDone = true;
        player.BackToIdle();
        OverworldManager.Ins.ExitCutscene();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<OverworldController>() != null && sequenceDone == false)
        {
            camAnim.enabled = true;
            Sequence01();
            player = other.gameObject.GetComponent<OverworldController>();
            gameObject.GetComponent<SphereCollider>().enabled = false;
        }
    }

    public void OverwriteCutsceneManager(Scene scene, LoadSceneMode mode)
    {
        List<GameObject> list = scene.GetRootGameObjects().ToList();
        foreach (GameObject objs in list)
        {
            if (objs.GetComponent<CutsceneManager>() != null)
            {
                objs.GetComponent<CutsceneManager>().sequenceDone = CutsceneManager.Ins.sequenceDone;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
