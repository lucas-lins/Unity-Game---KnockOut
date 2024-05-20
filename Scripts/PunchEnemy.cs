using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchEnemy : MonoBehaviour{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Destroy(gameObject);
        if(collision.gameObject.TryGetComponent<Player>(out Player playerComponent))
        {
            playerComponent.PlayerTakeDamage(1);

        }
    }

}
