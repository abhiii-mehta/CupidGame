using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSlotUI : MonoBehaviour
{
    public TMP_InputField playerInputField;
    public TMP_Text playerNameText;
    public TMP_Text highScoreText;
    public Button deleteButton;

    private PlayerSelectUI selectUI;
    private string playerName;

    public void Initialize(PlayerSelectUI ui, string name, int score)
    {
        selectUI = ui;
        playerName = name;

        if (!string.IsNullOrEmpty(playerName))
        {
            playerInputField.gameObject.SetActive(false);
            playerNameText.gameObject.SetActive(true);
            playerNameText.text = playerName;
        }
        else
        {
            playerInputField.gameObject.SetActive(true);
            playerNameText.gameObject.SetActive(false);
        }

        highScoreText.text = score.ToString();

        // Listen for name entry only once
        playerInputField.onEndEdit.RemoveAllListeners();
        playerInputField.onEndEdit.AddListener(OnNameEntered);

        // Hook up delete button
        deleteButton.onClick.RemoveAllListeners();
        deleteButton.onClick.AddListener(OnDelete);
    }

    public void OnNameEntered(string name)
    {
        if (string.IsNullOrWhiteSpace(name)) return;

        playerName = name.Trim();
        playerNameText.text = playerName;
        playerNameText.gameObject.SetActive(true);
        playerInputField.gameObject.SetActive(false);

        PlayerProfileManager.Instance.AddPlayer(playerName);
        PlayerProfileManager.Instance.SetCurrentPlayer(playerName);
    }

    public void OnDelete()
    {
        if (!string.IsNullOrEmpty(playerName))
        {
            selectUI.OnPlayerDeleted(this);
        }
    }

    public string GetName()
    {
        return playerName;
    }
}
