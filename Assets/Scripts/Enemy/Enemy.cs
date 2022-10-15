using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    private string currentState = "idle";
    private Transform target;
    public float chaseRange = 5;
    public Animator animator;
    public float speed = 3;
    public float attackRange = 2;

    public int health;
    public int maxHealth;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(transform.position, target.position);

        if (currentState == "idle")
        {

            if (distance < chaseRange)
                currentState = "chase";

        }
        else if (currentState == "chase")
        {
            animator.SetTrigger("chase");
            animator.SetBool("isAttacking", false);

            if (distance < attackRange)
            {
                currentState = "attack";
            }
            if (target.position.x > transform.position.x)
            {
                //move right
                transform.Translate(transform.right * speed * Time.deltaTime);
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            else
            {
                //move left
                transform.Translate(-transform.right * speed * Time.deltaTime);
                transform.rotation = Quaternion.identity;
            }
        }
        else if (currentState == "attack")
        {
            animator.SetBool("isAttacking", true);
            if (distance > attackRange)
            {
                currentState = "chase";
            }
        }
    }
    private void Parish()
    {
        //play animation
        animator.SetTrigger("isParished");
        //disable the script and the collider
        GetComponent<CapsuleCollider>().enabled = false;
        this.enabled = false;
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
        currentState = "chase";
        if (health < 0)
        {
            Parish();
        }
    }
}
