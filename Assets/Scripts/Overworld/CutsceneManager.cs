using NUnit.Framework;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

public class CutsceneManager : MonoBehaviour
{
    [SerializeField] List<CinemachineCamera> cams;
    [SerializeField] CinemachineBrain brain;
    int currentCamera = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log(brain.ActiveBlend);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
