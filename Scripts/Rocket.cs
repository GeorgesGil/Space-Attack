using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    public float speed;
    public Transform closestEnemy;
    public GameObject[] multipleEnemies;

    // Update is called once per frame
    void Update()
    {   
        closestEnemy = GetClosestEnemy();

        if (closestEnemy != null)
        {
            ChasingEnemy();
        }
    }

    public void ChasingEnemy()
    {
        Vector2 lookDirection = (closestEnemy.transform.position - transform.position);
        transform.up = new Vector2(lookDirection.x, lookDirection.y);

        transform.position = Vector2.MoveTowards(transform.position, closestEnemy.position, speed * Time.deltaTime);
    }

    public Transform GetClosestEnemy()
    {
        //enemies are those objects that have a tag of Enemy
        multipleEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        //get the closest value
        float closestDistance = Mathf.Infinity;
        //transform of enemies are null
        Transform enemyPos = null;

        //search through enemies to find closest enemy and put it as target
        foreach (GameObject enemies in multipleEnemies)
        {
            //get float value if current distance
            float currentDistance;
            currentDistance = Vector3.Distance(transform.position, enemies.transform.position);

            //if current distance less than closest distance
            if (currentDistance < closestDistance)
            {
                //closest distance become current distance
                closestDistance = currentDistance;
                //get value transform of that enemy
                enemyPos = enemies.transform;
            }
        }

        return enemyPos;
    }

}
