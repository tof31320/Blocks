using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

    private static GameController _instance = null;
    public static GameController instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
            }
            return _instance;
        }
    }

    public Grille grille;

    public void Start()
    {
        ResetGrille();
    }

    private void ResetGrille()
    {
        grille.Build();
    }
}
