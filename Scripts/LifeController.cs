using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeController : MonoBehaviour
{
    public int health;
    public int moneyToGive;
    public int scoreToGive;
    public GameObject explosion;
    public Color damageColor;
    public bool isDead = false;
    private SpriteRenderer sprite;
    public GameObject[] dropItem;
    private static int chanceToDropItem = 0;

    // Start is called before the first frame update
    void Start()
    {
        //attach spriterenderer automaticaly to our sprite 
        sprite = GetComponent<SpriteRenderer>();
    }

    

    public void TakeDamage(int damage)
    {
        if(!isDead)
        {
            //reduce the health depend on damage amount
            health -= damage;

            if(health <= 0)
            {
                AudioManager.instance.PlaySFX(2);
                if(explosion != null)
                {
                    Instantiate(explosion, transform.position, transform.rotation);
                }
                

                if(this.GetComponent<PlayerController>() != null)
                {
                    GetComponent<PlayerController>().Respawn();

                }else
                {
                    //increase the chance of getting items by each kills
                    chanceToDropItem+=3;
                    //store value info between 0 and 100 in random
                    int random = Random.Range(0, 100);
                    //if random value is less than chance of drop and length of our items
                    if(random < chanceToDropItem && dropItem.Length > 0)
                    {
                        //create a random item between 0 index and our drop item list where ever enemy is going to die
                        Instantiate(dropItem[Random.Range(0, dropItem.Length)], transform.position, Quaternion.identity);
                        //reset chance of drop item back to 0 again
                        chanceToDropItem = 0;
                    }

                    //enemies going to destroy
                    isDead = true;
                    GameManager.instance.money += moneyToGive;
                    GameManager.instance.score += scoreToGive;
                    PlayerPrefs.SetInt("Money", GameManager.instance.money);
                    PlayerPrefs.SetInt("Score", GameManager.instance.score);
                    Destroy(gameObject);
                }
            }else
            {
                StartCoroutine(TakingDamage());
            }
        }
    }

    

    IEnumerator TakingDamage()
    {
        //change the color of sprite when it get damage 
        sprite.color = damageColor;

        //after 0.1 second delay turn the sprite color back to orginal
        yield return new WaitForSeconds(0.1f);
        sprite.color = Color.white;
    }
}
