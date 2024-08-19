using UnityEngine;
using TMPro;
using Dan.Main;

public class Leaderboard : MonoBehaviour
{
    [SerializeField] private TMP_Text[] _entryTextObjects;
    [SerializeField] private TMP_InputField _usernameInputField;

    private void Start()
    {
        LoadEntries();
    }

    private void LoadEntries()
    {
        Leaderboards.ProtectTheCoreLeaderboard.GetEntries(entries =>
        {
            foreach (TMP_Text text in _entryTextObjects)
            {
                text.text = "";
            }

            int length = Mathf.Min(_entryTextObjects.Length, entries.Length);
            for (int i = 0; i < length; i++)
            {
                _entryTextObjects[i].text = $"{entries[i].Rank}. {entries[i].Username} - {entries[i].Score}";
            }
        });
    }

    public void UploadEntry()
    {
        Leaderboards.ProtectTheCoreLeaderboard.UploadNewEntry(_usernameInputField.text,
            LocalDataStorage.Instance.PlayerData.PlayerStats.TimeAlive, isSuccessful =>
        {
            if (isSuccessful)
                LoadEntries();
        });
    }
}
