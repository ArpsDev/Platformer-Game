using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Collect_Coin : MonoBehaviour
{

    public AudioSource collectsound;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Collectables.theScore += 1;
        StartCoroutine(Die());
    }

    IEnumerator Die()
    {
        collectsound.Play();
        yield return new WaitForSeconds(0.2f);
        Destroy(gameObject);
    }
}
