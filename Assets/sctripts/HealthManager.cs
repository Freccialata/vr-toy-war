using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{

    public int healthAmount = 5;
    private int leftHP;
    private bool dead = false;
    Vector3 graveYard = new Vector3(-10, -10, -10);
    private Animator m_animator;

    //initiating respawn
    public float respawnTime = 5;
    private static HealthManager corout;
    //Object pf;
    public Vector3 spawnloc = new Vector3(-3.9f, 0, 1);
    private Quaternion spawnrot = Quaternion.identity;
    private string pfn;
    public float DestroyTime = 5;

    void Awake()
    {
        corout = this;
        Debug.Log("Loaded " + this.name);
        pfn = this.name;
        m_animator = GetComponent<Animator>();
        //pf = PrefabUtility.GetPrefabParent(this) ; // levelManager.character(this.name);
    }

    void Start()
    {
        leftHP = healthAmount;

    }

    public void loseHealth()
    {
        leftHP--;
        Debug.Log("HP left " + leftHP);

        if (leftHP < 1)
        {
            Debug.Log("You just killed " + pfn);
            dead = true;
            m_animator.SetBool("IsDead", true);
            m_animator.SetBool("IsMoving", true);
            corout.StartCoroutine(enemyRespawn());
            corout.StartCoroutine(enemyDestroy());


        }
    }

    IEnumerator enemyRespawn()
    {
        yield return new WaitForSeconds(respawnTime);
        if (dead == true)
        {
            Debug.Log("EnemyRespawned!");
            if (pfn.Contains("clown"))
            {
                Debug.Log("Instantiating Clown");
                //GameObject newChar = Instantiate(Resources.Load("clown_res", typeof(GameObject))) as GameObject;
                levelManager.respawnClown();
            }
            else if (pfn.Contains("doudou"))
            {
                Debug.Log("Instantiating Dou");
                //GameObject newChar = Instantiate(Resources.Load("doudou_res", typeof(GameObject))) as GameObject;
                levelManager.respawnDoudou();
            }
            else if (pfn.Contains("robottino"))
            {
                Debug.Log("Instantiating Rob");
                //GameObject newChar = Instantiate(Resources.Load("robottino_res", typeof(GameObject))) as GameObject;
                levelManager.respawnRobot();
            }
            dead = false;
        }
    }

    IEnumerator enemyDestroy()
    {
        yield return new WaitForSeconds(DestroyTime);
        gameObject.transform.position = graveYard;
        yield return new WaitForSeconds(respawnTime);
        Destroy(gameObject);
    }
}

