using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dan.Main;
using Dan.Models;
using TMPro;
using UnityEngine.Events;
using System;

public class ScoreSender : MonoBehaviour
{
    [SerializeField] string publicKey;
    [SerializeField] private TMP_InputField nickname;
    [SerializeField] private TextMeshProUGUI score;

    [SerializeField] private Item playerData;
    [SerializeField] private GameObject sendButton;

    private int currentScore;
    private GameInput inputs;

    public UnityEvent OnScoreSend;

    private void Awake() {
        nickname.text = PlayerPrefs.GetString("Nickname", "");
        currentScore = LocalDataStorage.Instance.PlayerData.PlayerStats.TimeAlive;
        
        score.text = SetTimeText(currentScore);

        inputs = new GameInput();
        inputs.Enable();

        //inputs.Player.Inte.performed += c => SendScore();
    }

    private string SetTimeText(int num) {
        TimeSpan time = TimeSpan.FromSeconds(num);

        string timeString = "";

        if(time.Hours > 0) {
            timeString += $"{time.Hours}h ";
        }
        if(time.Minutes > 0) {
            timeString += $"{time.Minutes}m ";
        }
        if(time.Seconds > 0 || timeString == "") {
            timeString += $"{time.Seconds}s";
        }

        return timeString.Trim();
    }

    public void SendScore() {
        inputs.Disable();
        sendButton.SetActive(false);

        PlayerPrefs.SetString("Nickname", nickname.text);
        string name = nickname.text != "" ? nickname.text : "Anonym";
        nickname.text = name;
        /*playerData.Name.text = name;
        playerData.Score.text = score.text;*/

        LeaderboardCreator.UploadNewEntry(publicKey, name, currentScore, OnScoreUploaded);
    }

    private void OnScoreUploaded(bool done) {
        OnScoreSend.Invoke();
        LeaderboardCreator.ResetPlayer();
        gameObject.SetActive(false);
    }

}
