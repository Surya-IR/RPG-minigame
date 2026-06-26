using Unity.Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Camera mainCam;
    [SerializeField] CinemachineBrain brain;
    [SerializeField] OverworldController player;

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    { 
        mainCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
