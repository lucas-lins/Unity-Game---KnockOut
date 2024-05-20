using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Punch : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Destroy(gameObject);
        if(collision.gameObject.TryGetComponent<Enemy>(out Enemy enemyComponent))
        {
            enemyComponent.TakeDamage(1);

        }
    }

}
