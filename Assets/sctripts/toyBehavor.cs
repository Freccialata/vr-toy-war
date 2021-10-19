using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class toyBehavor : MonoBehaviour {

    GameObject player;
    private CharacterController m_characterController;
    public float m_walkSpeed = 1.246f;
    //public float m_runSpeed = 4.65408f;
    public float m_acceleration = 1f;
    //public float m_jumpStrenght = 1f;

    public float m_gravity = 1f;
    //public float m_terminalVelocity = 3f;
    public float m_turnSpeed = 360f;

    private Vector3 m_velocity;
    private Animator m_animator;
    private float m_speed;

    public int damage = 1;


    void Awake()
    {
        //m_instance = this;
        m_velocity = Vector3.zero;
        m_characterController = GetComponent("CharacterController") as CharacterController;
        m_animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Start()
    {
        m_animator.SetBool("Grounded", checkGrounding());
    }
    
    public void moveCharacter(Vector2 goalDirection, bool IsNear, bool IsMoving)
    {
        goalDirection.Normalize();

        m_animator.SetBool("Grounded", checkGrounding());

        if (checkGrounding() && m_velocity.y <= 0)
        {
            m_velocity.y = 0f; //reset vertical velocity

            float goalSpeed = 0;

            //moving
            if (IsMoving == true)
            {
                goalSpeed = m_walkSpeed * Time.fixedDeltaTime;
                /*walking
                if (!run)
                {
                    goalSpeed = m_walkSpeed * Time.fixedDeltaTime;

                }*/
                
                Vector3 moveVector = new Vector3(goalDirection.x, 0, goalDirection.y) * goalSpeed;
                Vector3 velocityOffset = moveVector - m_velocity;

                if (velocityOffset.magnitude / Time.fixedDeltaTime > m_acceleration)
                    m_velocity += velocityOffset.normalized * m_acceleration * Time.fixedDeltaTime;
                else
                    m_velocity = moveVector;

                m_speed = m_velocity.magnitude / Time.fixedDeltaTime;

                if (m_velocity.magnitude > m_walkSpeed * Time.fixedDeltaTime)
                {
                    float angle = Mathf.Atan2(m_velocity.x, m_velocity.z) * Mathf.Rad2Deg;
                    transform.rotation = Quaternion.Euler(0, angle, 0);
                }
                else
                {
                    //rotation delay
                    Vector2 currentDirection = new Vector2(transform.forward.x, transform.forward.z);
                    float sinAngle = Vector3.Cross(currentDirection, goalDirection).z;
                    float cosAngle = Vector2.Dot(currentDirection, goalDirection);
                    float angle = Mathf.Atan2(sinAngle, cosAngle) * Mathf.Rad2Deg;
                    if (Mathf.Abs(angle) > m_turnSpeed * Time.deltaTime)
                    {
                        if (angle < 0)
                            transform.Rotate(0, m_turnSpeed * Time.deltaTime, 0);
                        else
                            transform.Rotate(0, -m_turnSpeed * Time.deltaTime, 0);
                    }
                    else
                    {
                        transform.Rotate(0, -angle, 0);
                    }
                }
                m_velocity.y = -0.1f; //keep character grounded
                m_characterController.Move(m_velocity);
            }

        }
        else
        {
            m_velocity.y -= m_gravity * Time.deltaTime;
        }

        animateCharacter(IsNear, IsMoving);
    }

    public void animateCharacter(bool IsNear, bool IsMoving)
    {
        m_animator.SetFloat("Speed", m_speed);

        if (IsNear)
        {
            m_animator.SetBool("IsNear", true);
        }
        else {
            m_animator.SetBool("IsNear", false);
        }

        if (IsMoving)
        {
            m_animator.SetBool("IsMoving", true);
        }

        else {
            m_animator.SetBool("IsMoving", false);
        }
    }

    bool checkGrounding()
    {
        float threshold = 0.1f;
        float originOffset = 0.1f;
        return (Physics.Raycast(transform.position + (Vector3.up * originOffset), Vector3.down, originOffset + threshold));
    }

    public void hurtPlayer()
    {
        player.GetComponent<playerHealthManager>().loseHealth(damage);
    }
}