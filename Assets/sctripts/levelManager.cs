using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelManager : MonoBehaviour {

    //initiating enemies
    public GameObject Robot;
    public GameObject Doudou;
    public GameObject Clown;
    public GameObject Player;
    private static Vector3 spawnrobot = new Vector3(-2, 0, 1.5f);
    private static Vector3 spawndoudou = new Vector3(-3.4f, 0, 1.2f);
    private static Vector3 spawnclown = new Vector3(-6.3f, 0, 4);
    private static Quaternion rotation = Quaternion.identity;


    public static GameObject pfRob, pfClown, pfDou;
    //GameObject deadTarget;

	void Start () {
        GameObject newRobot = Instantiate(Robot, spawnrobot, rotation);
        GameObject newDoudou = Instantiate(Doudou, spawndoudou, rotation);
        GameObject newClown = Instantiate(Clown, spawnclown, rotation);

        pfRob = Robot;
        pfClown = Clown;
        pfDou = Doudou;

        //deadTarget = gameObject.GetComponent<gunShoot>().deadTarget;
    }

    public static void respawnRobot()
    {
        GameObject newRobot = Instantiate(pfRob, spawnrobot, rotation);
        Debug.Log("Robot Respawned!");
    }

    public static void respawnClown()
    {
        GameObject newClown = Instantiate(pfClown, spawnclown, rotation);
        Debug.Log("Clown Respawned!");
    }

    public static void respawnDoudou()
    {
        GameObject newDoudou = Instantiate(pfDou, spawndoudou, rotation);
        Debug.Log("Doudou Respawned!");
    }
}
