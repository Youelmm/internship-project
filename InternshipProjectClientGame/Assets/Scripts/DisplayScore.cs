using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayScore : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        // To display the score
        GetComponent<Text>().text = $"SCORE: {PlayerPrefs.GetString("score")}";
    }
}
