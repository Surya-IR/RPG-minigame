using UnityEngine;
using UnityEngine.Events;

public class OverworldController : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] float originalSpeed = 3f;
    [SerializeField] float speed = 0;
    [SerializeField] Camera cam;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cam = Camera.main;

        rb = gameObject.GetComponent<Rigidbody>();
        cam.transform.parent = gameObject.transform;
        PositionCamera();
    }

    void PlayerMovement()
    {
        Vector3 moveDir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        transform.position += moveDir * speed * Time.deltaTime;

    }

    void PositionCamera()
    {
        Vector3 playerPos = gameObject.transform.position;
        cam.transform.position = playerPos + new Vector3(0, 4, -7);

        cam.transform.LookAt(playerPos);
    }

    public void EnableControl()
    {
        speed = originalSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
    }
}
