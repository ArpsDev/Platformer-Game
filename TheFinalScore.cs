using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TheFinalScore : MonoBehaviour
{

    public TextMeshProUGUI finalscoredisplay;

    // Update is called once per frame
    void Update()
    {
        finalscoredisplay.text =
            Collectables.theScore.ToString("D2");
    }
}
