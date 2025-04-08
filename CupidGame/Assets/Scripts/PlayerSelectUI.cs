using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerSelectUI : MonoBehaviour
{
    public GameObject playerSlotPrefab;
    public Transform playerListContainer;
    public Button continueButton;
    public Button deleteAllDataButton;

    private List<PlayerSlotUI> slots = new();
    private int slotCount = 5;

    private void Start()
    {
        GenerateSlots();
        continueButton.onClick.AddListener(ContinueGame);
        deleteAllDataButton.onClick.AddListener(DeleteAllData);
    }
    void OnEnable()
    {
        Debug.Log("PlayerSelectPanel Enabled - refreshing UI");
        ReloadUI();
    }

    void GenerateSlots()
    {
        //  CLEAR OLD SLOTS FIRST
        foreach (Transform child in playerListContainer)
        {
            Destroy(child.gameObject);
        }
        slots.Clear();

        List<PlayerData> players = PlayerProfileManager.Instance.GetAllPlayers();

        for (int i = 0; i < slotCount; i++)
        {
            GameObject obj = Instantiate(playerSlotPrefab, playerListContainer);
            PlayerSlotUI slot = obj.GetComponent<PlayerSlotUI>();

            string name = i < players.Count ? players[i].playerName : "";
            int score = i < players.Count ? players[i].highScore : 0;

            slot.Initialize(this, name, score); //  All good now
            slots.Add(slot);
        }

    }



    public void OnPlayerSelected(PlayerData data)
    {
        PlayerProfileManager.Instance.SetCurrentPlayer(data.playerName);
    }

    public void OnPlayerDeleted(PlayerSlotUI slot)
    {
        PlayerProfileManager.Instance.DeletePlayer(slot.GetName());
        ReloadUI();
    }

    public void DeleteAllData()
    {
        PlayerProfileManager.Instance.DeleteAllPlayers();

        foreach (Transform child in playerListContainer)
        {
            Destroy(child.gameObject);
        }

        slots.Clear();
        GenerateSlots();
    }

    void ContinueGame()
    {
        foreach (Transform child in playerListContainer)
        {
            PlayerSlotUI slot = child.GetComponent<PlayerSlotUI>();
            if (slot != null)
            {
                string name = slot.GetName();
                if (!string.IsNullOrEmpty(name))
                {
                    PlayerProfileManager.Instance.SetCurrentPlayer(name);
                    PlayerData data = new PlayerData(name)
                    {
                        highScore = PlayerProfileManager.Instance.GetScore(name)
                    };
                    PlayerProfileManager.Instance.SavePlayer(data);
                    PlayerProfileManager.Instance.SetCurrentPlayer(name);
                    SceneManager.LoadScene("GameScene");
                    return;
                }
            }
        }

        Debug.LogWarning("No player selected!");
    }

    void ReloadUI()
    {
        foreach (Transform child in playerListContainer)
        {
            Destroy(child.gameObject);
        }
        slots.Clear();
        GenerateSlots();
    }
}
