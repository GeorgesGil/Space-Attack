using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dodging : MonoBehaviour
{
    public float speed;
    public Boundary boundary;
    private Rigidbody2D rb;
    private float target;
    public float dodgeValue;
    public Vector2 startWait;  
    public Vector2 dodgeTime;
    public Vector2 dodgewait;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(Dodge());
    }

    private void FixedUpdate()
    {
        float dodgingbehaviour = Mathf.MoveTowards(rb.velocity.x, target, speed);
        rb.velocity = new Vector2(dodgingbehaviour, rb.velocity.y);

        rb.position = new Vector2(Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax),
                                   Mathf.Clamp(rb.position.y, boundary.yMin, boundary.yMax));

    }

    IEnumerator Dodge()
    {
        yield return new WaitForSeconds(Random.Range(startWait.x, startWait.y));
        while(true)
        {
            target = Random.Range(1, dodgeValue) * -Mathf.Sign(transform.position.x);
            yield return new WaitForSeconds(Random.Range(dodgeTime.x, dodgeTime.y));
            target = 0;
            yield return new WaitForSeconds(Random.Range(dodgewait.x, dodgewait.y));
        }
        

    }
}
