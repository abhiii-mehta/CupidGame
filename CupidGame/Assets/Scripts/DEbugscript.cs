using UnityEngine;

public class PlayerPrefsDebugger : MonoBehaviour
{
    void Start()
    {
        Debug.Log("Saved Player Profiles:");
        Debug.Log(PlayerPrefs.GetString("PlayerProfiles", "None found"));

        if (PlayerPrefs.HasKey("CurrentPlayer"))
        {
            Debug.Log("Current Player: " + PlayerPrefs.GetString("CurrentPlayer"));
        }
    }
}
