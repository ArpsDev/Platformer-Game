using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level_Loader : MonoBehaviour
{
    public Animator transition;

    public float transitiontime;

    // Update is called once per frame
    void Update()
    {

    }

    public IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitiontime);

        SceneManager.LoadScene(levelIndex);
    }
}