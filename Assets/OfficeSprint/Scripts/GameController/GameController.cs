using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] public Countdown countdown;

    void Start()
    {
        countdown.OnTimeExpired += HandleTimeExpired; // Subscribe to the event
    }

    private void HandleTimeExpired()
    {
        Debug.Log("Time has expired!");
        // Add game over logic here
    }
}
