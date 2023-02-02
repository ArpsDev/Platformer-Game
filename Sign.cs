using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sign : MonoBehaviour
{

    public GameObject dialogueBox;
    public Text dialogueText;
    public string dialogue;
    public bool PlayerinRange;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && PlayerinRange)
        {
            if (dialogueBox.activeInHierarchy)
            {
                dialogueBox.SetActive(false);
            }
            else
            {
                dialogueBox.SetActive(true);
                dialogueText.text = dialogue;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //Debug.Log("Player in range");

            PlayerinRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //Debug.Log("Player left range");

            PlayerinRange = false;
            dialogueBox.SetActive(false);
        }
    }
}
