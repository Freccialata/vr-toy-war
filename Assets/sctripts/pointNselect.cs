using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class pointNselect : MonoBehaviour
{

    public Transform gun;
    private Button button;
    GameObject buttonPoint;
    GameObject buttonExit;
    private static pointNselect corout;

    private SteamVR_TrackedObject trackedObj;
    private SteamVR_Controller.Device device
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }

    public GameObject nerfGun;
    bool playing = false;

    void Start()
    {
        corout = this;
        trackedObj = GetComponent<SteamVR_TrackedObject>();
        button = GetComponent<Button>();
        buttonPoint = GameObject.FindGameObjectWithTag("Button");
        buttonExit = GameObject.FindGameObjectWithTag("Button_exit");
    }

    IEnumerator loadLevel(string scene)
    {
        // buttonSelection.startGame(levelName);

        float secs = 5f;
        SceneManager.LoadSceneAsync(scene);
        Debug.Log("Waiting...");
        yield return new WaitForSeconds(secs);
        SceneManager.UnloadSceneAsync("StartGame");
    }

    // Update is called once per frame
    void Update()
    {
        //Make a ray
        Ray ray = new Ray(gun.position, gun.forward);
        RaycastHit hit = new RaycastHit();
        //device.TriggerHapticPulse();

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject == buttonPoint && device.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
            {
                corout.StartCoroutine(loadLevel("Light_test"));
            }

            //hard coded
            if (hit.collider.gameObject == buttonExit && device.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
            {
                Application.Quit();
            }
        }    
        
        if (device.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
        {
            playSound();
        }
            
    }

    public void playSound()
    {
        if (playing == false)
        {
            playing = true;
            nerfGun.GetComponent<AudioSource>().Play();
        }

        if (nerfGun.GetComponent<AudioSource>().isPlaying == false)
        {
            playing = false;
        }
    }
}