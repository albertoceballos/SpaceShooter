using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour {
    private int highScore;

	// Use this for initialization
	void Start () {
        highScore = PlayerPrefs.GetInt("HighScore");
        var score = GameObject.Find("HighScore").GetComponent<Text>();
        score.text = highScore.ToString();
	}
	
}
