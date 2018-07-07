using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float Speed = 10.0F;
    void Start ()
    {
        Destroy(gameObject, 2F);
    }
	void Update ()
    {
        transform.position = Vector3.MoveTowards(transform.position, transform.position - transform.right, Speed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        Character character = collider.GetComponent<Character>();
        if (character != null)
        {
            Destroy(character.gameObject);
        }
    }
}
