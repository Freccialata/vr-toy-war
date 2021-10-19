using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class gunShoot : MonoBehaviour
{

    private Transform gun;
    bool playing = false;

    private GameObject RightController;
    private Animator shootAnim;

    private SteamVR_TrackedObject trackedObj = null;
    private SteamVR_TrackedController controller;
    private SteamVR_Controller.Device device
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }

    void Start()
    {
        RightController = GameObject.FindGameObjectWithTag("gun");

        Debug.Log("rightctrl= " + RightController);
        gun = RightController.GetComponent<Transform>();

        trackedObj = GetComponent<SteamVR_TrackedObject>();
        Debug.Log(trackedObj + " is trackedObj");

        controller = RightController.GetComponent<SteamVR_TrackedController>();
        Debug.Log(controller + " is controller");

        shootAnim = RightController.GetComponent<Animator>();
        shootAnim.SetBool("shootBool", false);
    }

    void Update()
    {
        if (device.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
        {
            shootAnim.SetBool("shootBool", true);
            playSound();

            //Make a raycast
            Ray ray = new Ray(gun.position, gun.forward);

            RaycastHit hit = new RaycastHit();

            device.TriggerHapticPulse();

            //What has been hit
            if (Physics.Raycast(ray, out hit))
            {
                GameObject hitEnemy = hit.collider.gameObject;

                if (hitEnemy.tag == "Enemy")
                {
                    Debug.Log("You hit a toy! " + hit.collider.gameObject.name);

                    hitEnemy.GetComponent<HealthManager>().loseHealth();
                }
            }
        }
        else
        {
            shootAnim.SetBool("shootBool", false);
        }
    }

    public void playSound()
    {
        if (playing == false)
        {
            playing = true;
            RightController.GetComponent<AudioSource>().Play();
        }

        if (RightController.GetComponent<AudioSource>().isPlaying == false)
        {
            playing = false;
        }
    }
}
