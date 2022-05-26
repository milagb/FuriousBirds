using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] private GameObject _cloudParticlePrefab;
    private void OnCollisionEnter2D(Collision2D col)
    {
        //bool didHitBird = col.collider.GetComponent<Bird>() != null;
        Bird bird = col.collider.GetComponent<Bird>();
        if (bird != null)
        {
            Instantiate(_cloudParticlePrefab, transform.position,
                quaternion.identity);
            Destroy(gameObject);
            return;
        }

        Enemy enemy = col.collider.GetComponent<Enemy>();
        if (enemy != null)
        {
            return;
        }

        if (col.contacts[0].normal.y < -0.3)
        {
            Instantiate(_cloudParticlePrefab, transform.position,
                quaternion.identity);
            Destroy(gameObject);
        }

    }
}
