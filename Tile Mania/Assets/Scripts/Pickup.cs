using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] AudioClip coinSFX;
    [SerializeField] int pickUpAmount = 5;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        AudioSource.PlayClipAtPoint(coinSFX, Camera.main.transform.position);
        FindObjectOfType<GameSession>().AddScore(pickUpAmount);
        Destroy(gameObject);
    }
}
