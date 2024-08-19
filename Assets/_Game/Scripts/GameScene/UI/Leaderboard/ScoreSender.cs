using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*using Dan.Main;
using Dan.Models;*/
using TMPro;
using UnityEngine.Events;

public class ScoreSender : MonoBehaviour
{
    [SerializeField] string publicKey;
    [SerializeField] private TMP_InputField nickname;
    [SerializeField] private TextMeshProUGUI score;
    [SerializeField] private TextMeshProUGUI startScore;
    [SerializeField] private Item playerData;
    [SerializeField] private GameObject sendButton;

    private int currentScore;
    private int newStartScore;
    private GameInput inputs;

    public UnityEvent OnScoreSend;

    private void Awake() {
        nickname.text = PlayerPrefs.GetString("Nickname", "");
        currentScore = (int)PlayerPrefs.GetFloat("Score", 0);
        newStartScore = currentScore/2;
        
        score.text = currentScore.ToString();
        startScore.text = newStartScore.ToString();

        PlayerPrefs.SetFloat("Score", newStartScore);

        inputs = new GameInput();
        inputs.Enable();

        //inputs.Player.Inte.performed += c => SendScore();
    }

    public void SendScore() {
        inputs.Disable();
        sendButton.SetActive(false);

        PlayerPrefs.SetString("Nickname", nickname.text);
        string name = nickname.text != "" ? nickname.text : "Anonym";
        nickname.text = name;
        playerData.Name.text = name;
        playerData.Score.text = score.text;

        //LeaderboardCreator.UploadNewEntry(publicKey, name, currentScore, OnScoreUploaded);
    }

    private void OnScoreUploaded(bool done) {
        OnScoreSend.Invoke();
        //LeaderboardCreator.ResetPlayer();
        gameObject.SetActive(false);
    }

}
