using UnityEngine;

public class ScenePersistent : MonoBehaviour
{
    // Singleton instance
    public static ScenePersistent Instance { get; private set; } 

    private void Awake()
    {
        // Check if another instance exists
        if (Instance != null && Instance != this) 
        {
            // If so, destroy this duplicate
            Destroy(gameObject); 
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); 
    }
}
