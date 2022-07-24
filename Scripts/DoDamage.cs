using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoDamage : MonoBehaviour
{
    public int damage = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        //we pass the info of object that script lifecontroller attached to it to life
        LifeController life = other.GetComponent<LifeController>();
        //check if object with lifecontroller script in it exist, if yes then do .... 
        if(life != null)
        {
            //give damage amount to the life
            life.TakeDamage(damage);
            //destroy the game object after that
            Destroy(gameObject);
        }
    }
}
