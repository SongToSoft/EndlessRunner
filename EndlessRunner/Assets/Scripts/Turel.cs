using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turel : MonoBehaviour
{
    private Bullet bullet;
    private float Rate = 2.0F;
    private void Awake()
    {
        //Получаем ссылку на пулю
        bullet = Resources.Load<Bullet>("Bullet");
    }
    private void Shoot()
    {
        //Место начала стрельбы
        Vector3 position = transform.position;
        position.x -= 1.4F;
        position.y -= 0.055F;
        Bullet newBullet = Instantiate(bullet, position, bullet.transform.rotation);
    }
    void Start ()
    {
        InvokeRepeating("Shoot", Rate, Rate);
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
