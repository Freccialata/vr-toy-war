using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class buttonSelection : MonoBehaviour
{

    /*public GameObject startSound;
    bool playing = false;*/

    public static buttonSelection instance;

    void Awake()
    {
        instance = this;

    }

    void Update()
    {

    }

    //generic sound starter
    /*public void playSound()
    {
        if (playing == false)
        {
            playing = true;
            startSound.GetComponent<AudioSource>().Play();
        }

        if (startSound.GetComponent<AudioSource>().isPlaying == false)
        {
            playing = false;
        }
    }*/


    //Start playable scene
    public static void startGame(string scene)
    {
        //instance.StartCoroutine(waitSecs(scene));
        Debug.Log("Loading scene: " + scene);
        SceneManager.LoadScene(scene);
    }

    IEnumerator waitSecs(string scene)
    {
        /*playSound();
        float secs = startSound.GetComponent<AudioSource>().clip.length;
        Debug logs to get the duration and the time passing*/
        float secs = 0.4f;
        Debug.Log("Waiting...");
        yield return new WaitForSeconds(secs);
        SceneManager.LoadScene(scene);
    }

}
