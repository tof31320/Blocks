using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIScore : MonoBehaviour {

    public Text txtScore;

    public string playerName = "Joueur";

    public int score = 0;

    public Color playerColor = Color.white;

    public bool selected = false;

	// Use this for initialization
	void Start () {
        txtScore = GetComponent<Text>();
        txtScore.color = playerColor;
	}
	
	// Update is called once per frame
	void Update () {
        txtScore.text = playerName + " : " + score;
        if (selected)
        {
            txtScore.fontStyle = FontStyle.Bold;
        }
        else
        {
            txtScore.fontStyle = FontStyle.Normal;
        }
	}
}
