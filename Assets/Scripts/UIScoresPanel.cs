using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UIScoresPanel : MonoBehaviour {

    public List<UIScore> scores = new List<UIScore>();

    void Awake()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            UIScore score = transform.GetChild(i).GetComponent<UIScore>();
            if (score != null)
            {
                scores.Add(score);
            }
        }
    }
}
