using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class playerHealthManager : MonoBehaviour {

    public int healthAmount = 100;
    private int leftHP;
    private static playerHealthManager corout;
    public float DestroyTime = 5.0f;
    private Image image;
    public GameObject ScreenDeath;
    

    // Use this for initialization
    void Awake () {
        corout = this;
        leftHP = healthAmount;
        image = ScreenDeath.GetComponent<Image>();
        image.enabled = false;
        
    }

    public void loseHealth(int damage)
    {
        if (leftHP >= 0)
            leftHP -= damage;
        
        Debug.Log("HP left " + leftHP);

        if (leftHP < 1)
        {
            Debug.Log("You just died!");
            // corout.StartCoroutine(playerDead());
            image.enabled = true;
            corout.StartCoroutine(waitRestart("StartGame"));


        }
    }

    IEnumerator playerDead()
    {
        yield return new WaitForSeconds(DestroyTime);
        Debug.Log("Back to the menu!");
    }

    IEnumerator waitRestart(string scene)
    {
        float secs = 3;
        Debug.Log("Restarting...");
        yield return new WaitForSeconds(secs);
        SceneManager.LoadScene("StartGame");
    }

}
