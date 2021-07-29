using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Enemy : MonoBehaviour
{
    public AIPath aiPath;
    public int health;
    public GameObject deathEffect;
    public bool IsMoving;
    public Animator animator;
    public int damage = 10;
    public float targetRange;
    GameObject player;

    public bool flyer;
    public bool melee;
    public bool ranged;

    public bool notInRoom = true;

    bool hit = false;
    private float timeBtwShots;
    public float startTimeBtwShots;
    public GameObject projectile;

    void Start()
    {
        aiPath.canSearch = false;
        player = GameObject.FindGameObjectWithTag("Player");
        timeBtwShots = startTimeBtwShots;
        StartCoroutine(EnemySearch());
    }

    void Update()
    {
        if (aiPath.desiredVelocity.x >= 0.01f)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
            animator.SetBool("IsMoving", true);
        }
        else if (aiPath.desiredVelocity.x <= -0.01f)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);

            animator.SetBool("IsMoving", true);
        }
        else if (aiPath.desiredVelocity.x == 0f)
        {
            animator.SetBool("IsMoving", false);
        }
        if (health <=0)
        {
            gameObject.GetComponent<RandomDrop>().dropRandomItem();
            Instantiate(deathEffect, transform.position, Quaternion.identity);
            Debug.Log("Enemy Dead");
            RoomController.instance.UpdateRooms();
            Death();
        }  

        if(notInRoom == false)
        {
            //Debug.Log("Player in the room");
            aiPath.canMove = true;
            aiPath.canSearch = true;
        }
        else
        {
            //Debug.Log("Player not the room");
            aiPath.canMove = false;
            aiPath.canSearch = false;
        }

        if(ranged == true)
        {
            FindTarget();
        }
        
        if(melee == true)
        {
            MeleeAttack();
        }
        
    }

    public IEnumerator EnemySearch()
    {
        yield return new WaitForSeconds(1.5f);
        aiPath.canSearch = true;
    }

    private void FindTarget()
    {
        //Debug.Log(Vector3.Distance(transform.position, player.position));
        float targetRange = 14f;
        if(Vector3.Distance(transform.position, player.transform.position) < targetRange)
        {   
            aiPath.canSearch = true;
        }
        if((Vector3.Distance(transform.position, player.transform.position) < targetRange - 7f) && aiPath.canMove == true)
        {
            if(timeBtwShots <= 0)
            {
                Instantiate(projectile, transform.position, Quaternion.identity);
                AudioManager.instance.Play("EnemyProj");
                timeBtwShots = startTimeBtwShots;
            }
            else
            {
                timeBtwShots -= Time.deltaTime;
            }
        }
    }

    private void MeleeAttack()
    {
        //Debug.Log(Vector3.Distance(transform.position, player.transform.position));
        float targetRange = 2f;
        if(Vector3.Distance(transform.position, player.transform.position) < targetRange)
        {
            AudioManager.instance.Play("ShrimpAttack");
            animator.SetFloat("Range", Vector3.Distance(transform.position, player.transform.position));
            StartCoroutine(Waiting());
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if(!hit)
        {
            hit = true;
            StartCoroutine("SwitchColor");
        }   
    }

    public void Death()
    {
        RoomController.instance.StartCoroutine(RoomController.instance.RoomCoroutine());
        AudioManager.instance.Play("EnemyDeath");
        Destroy(gameObject);
        Destroy(transform.parent.gameObject);
        PlayerScore scoreKeeper = player.GetComponent<PlayerScore>();
        scoreKeeper.AddKilled();

    }

    IEnumerator SwitchColor()
    {
        gameObject.GetComponent<SpriteRenderer>().color = new Color(255f,0f,0f,0.7f);
        yield return new WaitForSeconds(.25f);
        gameObject.GetComponent<SpriteRenderer>().color = new Color(255f,255f,255f,1f);
        hit = false;
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Debug.Log("Player is colliding");
            collision.gameObject.GetComponent<PlayerController>().takeDamage(damage);
        }
        if(collision.CompareTag("Enemy"))
        {
            Debug.Log("Enemy is colliding");
            aiPath.repathRate = Random.Range(1f, 2.0f);
        }
    }
    IEnumerator EnemiesCollision()
    {
        yield return new WaitForSeconds(1f);
        aiPath.canMove = true;
    }


    IEnumerator Waiting()
    {
        aiPath.repathRate = 2f;
        animator.SetBool("Waiting", true);
        animator.SetBool("InRange", false);
        yield return new WaitForSeconds(1f);
        animator.SetBool("Waiting", false);
        animator.SetBool("Searching", true);
        yield return new WaitForSeconds(1f);
        animator.SetBool("Searching", false);
    }


}
