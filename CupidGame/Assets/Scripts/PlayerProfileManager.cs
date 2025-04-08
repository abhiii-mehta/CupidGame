using System.Collections.Generic;
using UnityEngine;

public class PlayerProfileManager : MonoBehaviour
{
    public static PlayerProfileManager Instance;

    private const string PlayerPrefsKey = "PlayerProfiles";
    private const string CurrentPlayerKey = "CurrentPlayer";

    public List<PlayerData> players = new List<PlayerData>();
    private PlayerData currentPlayer;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadPlayers();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadPlayers()
    {
        players.Clear();
        string savedData = PlayerPrefs.GetString(PlayerPrefsKey, "");
        if (!string.IsNullOrEmpty(savedData))
        {
            string[] entries = savedData.Split('|');
            foreach (string entry in entries)
            {
                string[] parts = entry.Split(',');
                if (parts.Length == 2)
                {
                    string name = parts[0];
                    int score = int.Parse(parts[1]);
                    players.Add(new PlayerData(name) { highScore = score });
                }
            }
        }

        string currentName = PlayerPrefs.GetString(CurrentPlayerKey, "");
        currentPlayer = players.Find(p => p.playerName == currentName);
    }


    public void SavePlayers()
    {
        List<string> entries = new List<string>();
        foreach (var player in players)
        {
            entries.Add(player.playerName + "," + player.highScore);
        }
        PlayerPrefs.SetString(PlayerPrefsKey, string.Join("|", entries));

        if (currentPlayer != null)
        {
            PlayerPrefs.SetString(CurrentPlayerKey, currentPlayer.playerName);
        }

        PlayerPrefs.Save();
    }

    public void AddPlayer(string name)
    {
        if (!players.Exists(p => p.playerName == name))
        {
            PlayerData newPlayer = new PlayerData(name);
            players.Add(newPlayer);
            SavePlayers();
        }
    }

    public void DeletePlayer(string name)
    {
        players.RemoveAll(p => p.playerName == name);
        if (currentPlayer != null && currentPlayer.playerName == name)
        {
            currentPlayer = null;
            PlayerPrefs.DeleteKey(CurrentPlayerKey);
        }
        SavePlayers();
    }

    public void DeleteAllPlayers()
    {
        players.Clear();
        currentPlayer = null;
        PlayerPrefs.DeleteKey(PlayerPrefsKey);
        PlayerPrefs.DeleteKey(CurrentPlayerKey);
    }

    public void SetCurrentPlayer(string name)
    {
        currentPlayer = players.Find(p => p.playerName == name);
        PlayerPrefs.SetString(CurrentPlayerKey, name);
        PlayerPrefs.Save();
    }

    public PlayerData GetPlayer(string name)
    {
        return players.Find(p => p.playerName == name);
    }

    public PlayerData GetCurrentPlayer()
    {
        return currentPlayer;
    }

    public bool HasCurrentPlayer()
    {
        return currentPlayer != null;
    }

    public void SavePlayer(PlayerData player)
    {
        int index = players.FindIndex(p => p.playerName == player.playerName);
        if (index != -1)
        {
            players[index] = player;
            SavePlayers();
        }
    }
    public List<PlayerData> GetAllPlayers()
    {
        return players;
    }
    public int GetScore(string playerName)
    {
        var player = players.Find(p => p.playerName == playerName);
        return player != null ? player.highScore : 0;
    }
    

}
