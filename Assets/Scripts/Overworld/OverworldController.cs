using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class OverworldController : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] float originalSpeed = 5f;
    [SerializeField] float speed = 0;
    [SerializeField] Camera cam;

    [SerializeField] Animator anim;
    public bool inDialogue = false;

    public bool inCutscene;

    float angle = 0;
    void Start()
    {
        cam = Camera.main;
        rb = gameObject.GetComponent<Rigidbody>();

        //anim = GetComponent<Animator>();
        cam.transform.LookAt(transform.position);


        SceneManager.sceneLoaded += RepositionCameraOnSceneLoad;
    }

    public void PlayerMovement(Vector3 moveDir)
    {

        Vector3 lastDir = new Vector3();

        if (moveDir != Vector3.zero)
        {
            lastDir = moveDir;
        }
        transform.position += moveDir * speed * Time.deltaTime;
        anim.SetBool("isWalking", true);
        transform.rotation = Quaternion.LookRotation(lastDir);
    }

    void RepositionCameraOnSceneLoad(Scene scene, LoadSceneMode mode)
    {
        if (cam == null)
        {
            cam = Camera.main;
        }
    }

    public void EnableControl()
    {
        if (!inDialogue)
        {
            speed = originalSpeed;
        }
    }

    public void DisableControl()
    {
        speed = 0;
    }

    public void PlayCutsceneWalk()
    {
        anim.SetBool("isCutsceneWalk", true);
        anim.Play("Cutscene Walk");
    }

    public void BackToIdle()
    {
        Debug.Log("Stop Cutscene Walking: ");
        anim.SetBool("isCutsceneWalk", false);
    }

    // Update is called once per frame
    void Update()
    {
            Vector3 moveDir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        if (moveDir != Vector3.zero)
        {
            PlayerMovement(moveDir);
        }
        else
        {
            anim.SetBool("isWalking", false);
        }
    }
}
