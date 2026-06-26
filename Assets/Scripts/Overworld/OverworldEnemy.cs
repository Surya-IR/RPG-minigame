using UnityEngine;
using UnityEngine.SceneManagement;

public class OverworldEnemy : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<OverworldController>() != null)
        {
            SceneManager.LoadScene("TurnBasedScene");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
