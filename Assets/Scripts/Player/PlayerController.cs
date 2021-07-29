using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private bool isInvincible = false;
    [SerializeField] private float invincibilityDurationSeconds;
    [SerializeField] private float invincibilityDeltaTime;
    
    public float baseMoveSpeed = 10f;
    private float currentMoveSpeed;

    public Rigidbody2D rb;
    public Animator animator;
    Vector2 movement;

    public DeathMenu deathMenu;
    public GameObject Canvas;
    public bool tutorial;

    public int currentHealth = 60;
    public int maximumHealth = 60;
    public HealthBar hearts;

    private CharacterInventory characterInventory;
    [SerializeField] private Inventory inventory;

    // Vector3 originalPos;

    // public static PlayerController instance;

    void Start()
    {
    //     if(tutorial == true)
    //     {
    //         Debug.Log("Tutorial True");
    //     }
    //     else
    //     {
    //          if (instance == null)
    //         instance = this;
    //         else
    //         {
    //         Destroy(gameObject);
    //         return;

            
    //     }
    //        DontDestroyOnLoad(gameObject);
        
    // }
        

        // originalPos = new Vector3();
        // Debug.Log(originalPos);

        characterInventory = new CharacterInventory();
        inventory.setInventory(characterInventory);
        characterInventory.setInv(inventory);
        //delete when calcstats works, when player projectiles work
        currentMoveSpeed = 10;
        //delete when calcstats works, when player projectiles work
    }

    void Update()
    {
        //code to test if inventory updates, and removes
        if(Input.GetKeyDown(KeyCode.Space))
        {
            characterInventory.listOfItems();
        }
        else if(Input.GetKeyDown(KeyCode.Q))
        {
            heal(10);
        }
        // else if(Input.GetKeyDown(KeyCode.Q))
        // {
        //     characterInventory.removeItem("IncreaseDamage", this.gameObject);
        // }
        // else if(Input.GetKeyDown(KeyCode.E))
        // {
        //     characterInventory.removeItem("MovementUp", this.gameObject);
                
        // }
        //end of test code


        Inputs();
        Animate();
    }


    void FixedUpdate()
    {
        Move();
    }

    private void Inputs()
    {
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");
        if (movement.magnitude > 1.0f)
        {
            movement.Normalize();
        } 
    }

    private void Move()
    {
        rb.MovePosition(rb.position + movement * currentMoveSpeed * Time.fixedDeltaTime);
    }


    private void Animate()
    {
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Magnitude", movement.magnitude);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Item") && collision.GetComponent<Collectables>().item.itemName == "Health")
        {
            heal((int)collision.GetComponent<Collectables>().item.getStats());
            Destroy(collision.gameObject);
            AudioManager.instance.Play("Item");
        }
        else if(collision.CompareTag("Item") && !characterInventory.isFull())
        {
            add2Inventory(collision.GetComponent<Collectables>().item);
            Destroy(collision.gameObject);
            gameObject.GetComponent<CalculateCharacterStats>().calcAll();
            inventory.refreshInventory();
        }
        
    }

    public void heal(int healAmount)
    {
        Debug.Log("Old health: " + currentHealth);
        if(currentHealth >= maximumHealth)
        {
            currentHealth = maximumHealth;
        }
        else
        {
            currentHealth += healAmount;
        }
        hearts.SetFill((float)currentHealth/maximumHealth);
        Debug.Log("New Health " + currentHealth);
    }

    public CharacterInventory getInventory()
    {
        return characterInventory;
    }

    public void setMovementSpeed(float newMoveSpeed)
    {
        
        currentMoveSpeed = newMoveSpeed;
        Debug.Log("New ms " + currentMoveSpeed);
    }

    public float getMovementSpeed()
    {
        return baseMoveSpeed;
    }

    public void add2Inventory(Item itemGameObject)
    {
        characterInventory.addItem(itemGameObject);
        AudioManager.instance.Play("Item");
    }

    public IEnumerator waiting(GameObject gO)
    {
        int waitTime = 3;
        Vector2 randomDir = Random.insideUnitCircle;
        gO.GetComponent<BoxCollider2D>().enabled = false;
        gO.GetComponent<Rigidbody2D>().AddForce(randomDir * 5f, ForceMode2D.Impulse);
        yield return new WaitForSeconds(waitTime);
        gO.GetComponent<BoxCollider2D>().enabled = true;
    }
    
    
    //Damage and Death
    public void ApplyDeath()
    {
        animator.SetBool("Death", true);
        StartCoroutine(DeathEndEvent());
        //Destroy(gameObject);
    }

    IEnumerator DeathEndEvent()
    {
        yield return new WaitForSeconds(2f);
        deathMenu.EndGame();
        Canvas.transform.GetChild(0).gameObject.SetActive(false);
        Canvas.transform.GetChild(1).gameObject.SetActive(false);
    }


    private bool checkDeath()
    {
        if (currentHealth <= 0)
        {
            ApplyDeath();
            return true;
        }
        return false;
    }

    private IEnumerator InvincibilityFrames()
    {
        Debug.Log("Player is invincible");
        isInvincible = true;

        for (float i = 0; i < invincibilityDurationSeconds; i += invincibilityDeltaTime)
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(255f,0f,0f,0.7f);
            yield return new WaitForSeconds(invincibilityDeltaTime);
            gameObject.GetComponent<SpriteRenderer>().color = new Color(255f,255f,255f,1f);
            yield return new WaitForSeconds(invincibilityDeltaTime);
        }
        isInvincible = false;
    }

    public void takeDamage(int amount)
    {
        if (isInvincible) return;

        currentHealth -= amount;
        hearts.SetFill((float)currentHealth/maximumHealth);
        if (checkDeath())
            return;
        StartCoroutine(InvincibilityFrames());

    }
    
    // void OnTriggerEnter(Collider other)
    // {
    //   if(other.gameObject.tag == "End")
    //   {
    //       gameObject.transform.position = originalPos;
    //   }
    
    // }
}
