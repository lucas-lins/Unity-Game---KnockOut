using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{

    public int _score;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "Player")
        {
            GameController._instance._totalCoins += _score;
            GameController._instance.UpdateScoreText();
            Destroy(gameObject);

        }
    }



}
