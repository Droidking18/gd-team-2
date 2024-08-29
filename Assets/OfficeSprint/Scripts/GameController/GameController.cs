using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] public Countdown countdown;

    void Start()
    {
        // Subscribes to the event
        countdown.OnTimeExpired += HandleTimeExpired; 
    }

    private void HandleTimeExpired()
    {
        Debug.Log("Time has expired!");

        // Loads Game lost scene
        SceneManager.LoadScene("YouLoseScene"); 
    }
}
