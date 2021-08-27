using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private PlayerRacer player;


    // Start is called before the first frame update
    void Start()
    {
        player = PlayerRacer.instance;
        player.OnPlayerDeath += ReloadLevel;
    }

    // Update is called once per frame
    void Update()
    {
        ToQuit();
    }

    void ToQuit()
    {
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
        
    }

    void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
