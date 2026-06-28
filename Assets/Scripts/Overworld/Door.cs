using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] Transform spawnLocation;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Transform SpawnLocation
    {
        get { return spawnLocation; }
    }
}
