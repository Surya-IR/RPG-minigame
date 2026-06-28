using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class OverworldController : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] float originalSpeed = 5f;
    [SerializeField] float speed = 0;
    [SerializeField] Camera cam;

    private Animator anim;
    public bool inDialogue = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cam = Camera.main;
        rb = gameObject.GetComponent<Rigidbody>();
        PositionCamera();

        anim = GetComponent<Animator>();
        cam.transform.LookAt(transform.position);

        DontDestroyOnLoad(this);
        DontDestroyOnLoad(cam);

        SceneManager.sceneLoaded += RepositionCameraOnSceneLoad;
    }

    void PlayerMovement()
    {
        Vector3 moveDir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        Vector3 lastDir = new Vector3();
        if (moveDir != Vector3.zero)
        {
            lastDir = moveDir;
        }
        transform.position += moveDir * speed * Time.deltaTime;

        if (moveDir != Vector3.zero)
        {
            anim.SetBool("isWalking", true);
        }
        else
        {
            anim.SetBool("isWalking", false);
        }

        Debug.Log("lastDir value: " + lastDir);
        transform.rotation = Quaternion.LookRotation(lastDir);
    }

    void PositionCamera()
    {
        Vector3 playerPos = gameObject.transform.position;
        cam.transform.position = playerPos + new Vector3(0, 5, -7f);
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

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
        PositionCamera();
    }
}
