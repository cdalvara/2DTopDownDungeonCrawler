using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public float speed;
    private Transform player;
    private Vector2 target;
    public int damage = 10;
    public float lifeTime;
    private Vector3 direction;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        direction = (transform.position - player.position).normalized;
        Invoke("DestroyProjectile", lifeTime);
 
    }

    // Update is called once per frame
    void Update()
    {   
        //The below can make proj follow
        //transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        transform.position -= direction * (speed * Time.deltaTime);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player is colliding");
            collision.gameObject.GetComponent<PlayerController>().takeDamage(damage);
            DestroyProjectile();
        }
        if(collision.CompareTag("Environment"))
        {
            DestroyProjectile();
        }
    }

    void DestroyProjectile()
    {
        Destroy(gameObject);
    }

}
