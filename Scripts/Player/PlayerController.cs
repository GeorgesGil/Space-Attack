using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundary
{
    public float xMin, xMax, yMin, yMax;
}

public enum ItemEffect
{
    shield,levelup,rocket
}

public class PlayerController : MonoBehaviour
{
    public int level;
    public int maxLevel;
    public int upgradeCost;
    public int fireLevel = 1;
    public float fireRate;
    public float speed;
    public int rocketAmount;
    private Animator anim;
    private Rigidbody2D rig;
    public GameObject playerBullet;
    public GameObject playerRocket;
    public GameObject shild;
    public Transform[] firePoints;
    public int lives = 1;
    private bool isDead = false;
    private SpriteRenderer sprite;
    private Vector3 startPosition;
    public float spawnTime;
    public float invincibilityTime;
    
    private float nextFire;
    public Boundary boundary;

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //get the value of player controller lives and send that info to gameManager function

        GameManager.instance.SetLivesText(lives);
        GameManager.instance.SetRocketText(rocketAmount);
        GameManager.instance.SetUpgradeCostText(upgradeCost);

        rig.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * speed;

        rig.position = new Vector2(Mathf.Clamp(rig.position.x, boundary.xMin, boundary.xMax),
                                   Mathf.Clamp(rig.position.y, boundary.yMin, boundary.yMax));

        if(!isDead)
        {
            //when we push left mouse button then we should shoot
            if (Input.GetButton("Fire1") && Time.time > nextFire)
            {
                AudioManager.instance.PlaySFX(10);
                nextFire = Time.time + fireRate;
                if (fireLevel >= 1)
                {
                    //copy of object from prefab and send it from firepoint position
                    Instantiate(playerBullet, firePoints[0].position, firePoints[0].rotation);
                }

                if (fireLevel >= 2)
                {
                    //copy of object from prefab and send it from firepoint position
                    Instantiate(playerBullet, firePoints[0].position, firePoints[0].rotation);
                    Instantiate(playerBullet, firePoints[1].position, firePoints[1].rotation);
                }

                if (fireLevel >= 3)
                {
                    //copy of object from prefab and send it from firepoint position
                    Instantiate(playerBullet, firePoints[0].position, firePoints[0].rotation);
                    Instantiate(playerBullet, firePoints[1].position, firePoints[1].rotation);
                    Instantiate(playerBullet, firePoints[2].position, firePoints[2].rotation);
                }

            }

            if (Input.GetMouseButtonDown(1) && Time.time > nextFire && rocketAmount > 0)
            {
                Instantiate(playerRocket, firePoints[0].position, firePoints[0].rotation);
                rocketAmount--;
                AudioManager.instance.PlaySFX(8);
            }
            if(Input.GetKeyDown(KeyCode.U))
            {
                AddLevel();
            }
        }
        
    }

    public void Respawn()
    {
        //reduce 1 from lives after each death
        lives --;
        if(lives > 0)
        {
            //call the corotine as long as lives didnt reach to zero
            StartCoroutine(Spawning());
        }else
        {
            lives = 0;
            isDead = true;
            sprite.enabled = false;
        }       
    }

    IEnumerator Spawning()
    {
        //bool is dead become true
        isDead = true;
        //player sprite going to get disable
        sprite.enabled = false;
        //make the fire level to 0 so we cant shot anymore during dead time
        fireLevel = 0;
        //put player layer to enemy layer so we dont get damage or die again
        gameObject.layer = 8;
        //after spawntime delay we want to...
        yield return new WaitForSeconds(spawnTime);
        //make that isdead bool to become false
        isDead = false;
        //spawn our player at start position
        transform.position = startPosition;
        //make loop time and during  invincibility time we want to...
        for(float i= 0; i< invincibilityTime; i+=0.1f)
        {
            //turn on and of the sprite every 0.1f
            sprite.enabled = !sprite.enabled;
            yield return new WaitForSeconds(0.1f);

        }
        //when loop finished we want put back player layer to player again
        gameObject.layer = 6;
        //activate the sprite 
        sprite.enabled = true;
        //put fire level back to 1
        fireLevel = 1;
    }

    public void SetItemEffect(ItemEffect effect)
    {
        if(effect == ItemEffect.levelup)
        {
            AudioManager.instance.PlaySFX(7);
            fireLevel++;
            if (fireLevel >= 3)
                fireLevel = 3;
        }

        else if (effect == ItemEffect.rocket)
        {
            rocketAmount++;
            AudioManager.instance.PlaySFX(7);
        }

        else if(effect == ItemEffect.shield)
        {
            Instantiate(shild, transform);
            AudioManager.instance.PlaySFX(7);
        }
    }
    
    public void AddLevel()
    {
        //check if we have enough money and our level is not max
        if (upgradeCost <= GameManager.instance.money && level < maxLevel)
        {
            //add one level to ship
            level++;
            fireRate-= 0.1f;
            rocketAmount++;
            lives++;
            speed += 0.5f;
            //reduce money and save it in playerprefs
            GameManager.instance.money -= upgradeCost;
            PlayerPrefs.SetInt("Money", GameManager.instance.money);
            //multiply upgrade cost
            upgradeCost *= 2;
            //add sound effect
            AudioManager.instance.PlaySFX(10);
            anim.SetTrigger("UG");

        }
       
    }
}
