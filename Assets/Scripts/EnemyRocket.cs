using UnityEngine;
using System.Collections;

[AddComponentMenu("MyGame/EnemyRocket")]
public class EnemyRocket : Rocket
{
    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        Destroy(this.gameObject);
    }
}
