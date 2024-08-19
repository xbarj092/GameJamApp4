using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*using Dan.Main;
using Dan.Models;*/
using UnityEngine.Events;
using TMPro;
public class LeaderboardManager : MonoBehaviour
{
    [SerializeField] string publicKey;
    [SerializeField] private List<Item> items;
    public UnityEvent OnHighScoresGet;

    private void Awake() {
        foreach(var i in items) {
            i.Name.text = "XXX";
            i.Score.text = "0";
        }
    }

    private void Start() {
        GetHighScores();
    }

    public void ShowLeaderboard() {
        GetHighScores();
    }

    public void GetHighScores() {
        //LeaderboardCreator.GetLeaderboard(publicKey, OnGetHighScores);
    }

    /*private void OnGetHighScores(Entry[] entries) {
        int i = 0;
        foreach(var entry in entries) {
            if(i == 10)
                return;
            items[i].Name.text = entry.Username;
            items[i].Score.text = entry.Score.ToString();
            i++;
        }
        OnHighScoresGet.Invoke();
    }*/


}