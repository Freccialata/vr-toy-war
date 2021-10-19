using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class toyIA : MonoBehaviour {

    toyBehavor m_characterBehaviour;
    Transform m_goal;
    Animator m_animator;

    public float hitDelay = 1;
    private float timePassed = 0;

    void Awake()
    {
        m_characterBehaviour = GetComponent<toyBehavor>();

        m_animator = GetComponent<Animator>();

        m_goal = GameObject.FindGameObjectWithTag("MainCamera").transform;
    }

    void Update()
    {
        Vector2 directionVector;
        directionVector = new Vector2(m_goal.position.x - transform.position.x, m_goal.position.z - transform.position.z);

        float goalDistance = directionVector.magnitude;

        directionVector = directionVector.normalized;

        bool IsMoving = false;

        bool dead = m_animator.GetBool("IsDead");

        /*bool run = false;
        if (goalDistance < 12 && goalDistance > 2) run = true;*/

        bool IsNear = false;
        if (goalDistance < 0.5f)
        {
            directionVector = Vector2.zero;
            IsNear = true;

            if (timePassed == 0)
                m_characterBehaviour.hurtPlayer();

            timePassed += Time.deltaTime;

            if (timePassed >= hitDelay)
                timePassed = 0;

            
        }

        if (goalDistance > 0.05f && dead == false)
        {
            IsMoving = true;
        }

        //moving
        m_characterBehaviour.moveCharacter(directionVector, IsNear, IsMoving);
    }
}
