using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saw : MonoBehaviour
{
    private float Speed = 20.0F;
    private float pos = 0;
    void Update ()
    {
        Vector3 vec3 = new Vector3(0, 0, pos);
        pos += Speed;
        transform.rotation = Quaternion.Euler(vec3);
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
