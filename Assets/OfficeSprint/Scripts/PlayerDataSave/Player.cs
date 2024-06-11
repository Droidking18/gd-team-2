using UnityEngine;

public class Player : MonoBehaviour
{
    // Player data 
    private Countdown countdown;

    private void Start()
    {
        // Find Countdown component (you could also reference it in the inspector)
        countdown = FindObjectOfType<Countdown>();
    }

    public void SavePlayerData()
    {
        if (countdown != null)
        {
            PlayerData data = new PlayerData(this, countdown.CurrentTime);
            SaveSystem.SavePlayer(data);
        }
        else
        {
            Debug.LogError("Countdown component not found. Cannot save player data.");
        }
    }
}

