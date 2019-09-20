using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    Rigidbody2D rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsFacingRight())
        {
            rigidbody.velocity = new Vector2(moveSpeed, 0f);
            //Debug.Log("Right: " + Time.frameCount + rigidbody.velocity);
        }
        else
        {
            rigidbody.velocity = new Vector2(-moveSpeed, 0f);
            //Debug.Log("Left: " + Time.frameCount + rigidbody.velocity);
        }
    }

    private bool IsFacingRight()
    {
        return transform.localScale.x > 0;
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        transform.localScale = new Vector2(-Mathf.Sign(rigidbody.velocity.x), 1f);
        //Debug.Log("Ontrigger: " + Time.frameCount + " " + Mathf.Sign(rigidbody.velocity.x));
    }
}
