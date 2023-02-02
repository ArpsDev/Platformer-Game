using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quit : MonoBehaviour
{
   public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Application.Quit();
            Debug.Log("Quit");
        }
    }
}
