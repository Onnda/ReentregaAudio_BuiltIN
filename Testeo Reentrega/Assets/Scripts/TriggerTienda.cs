using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerTienda : MonoBehaviour
{
    public AudioSource audioSource; // Arrastra aqu� el AudioSource desde el Inspector
    private string playerTag = "Player"; // Aseg�rate de que el GameObject del jugador tenga esta etiqueta

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            if (audioSource != null && !audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
    }
}