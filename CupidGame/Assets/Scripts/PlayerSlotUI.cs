using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerSlotUI : MonoBehaviour
{
    public TMP_InputField nameInput;
    public TMP_Text highScoreText;
    public Button deleteButton;

    private PlayerSelectUI manager;
    private string playerName;

    public void Initialize(PlayerSelectUI parent, string name, int score)
    {
        manager = parent;
        playerName = name;

        nameInput.text = playerName;
        highScoreText.text = "" + score;

        nameInput.onEndEdit.AddListener(OnNameChanged);
        deleteButton.onClick.AddListener(() => manager.OnPlayerDeleted(this));
    }

    void OnNameChanged(string newName)
    {
        if (string.IsNullOrWhiteSpace(newName)) return;

        PlayerData data = new PlayerData(newName);
        PlayerProfileManager.Instance.SavePlayer(data);
        manager.OnPlayerSelected(data);
    }


    public string GetName() => nameInput.text.Trim();
}
