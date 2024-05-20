using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public int _totalCoins;
    public Text _scoreText;
    public static GameController _instance;
    // Start is called before the first frame update
    void Start()
    {
        _instance = this; 
    }

    public void UpdateScoreText()
    {
        _scoreText.text = _totalCoins.ToString();
    }

}
