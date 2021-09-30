using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject bulletPrefab;
    public GameObject shooter;
    private Transform _muzzle;

    void Awake()
    {
        _muzzle = transform.Find("Muzzle");
    }
    void Start()
    {
        //Invoke("Shoot", 1f);
        //Invoke("Shoot", 2f);
        //Invoke("Shoot", 3f);
    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }

    public void Shoot()
    {
        if(bulletPrefab != null && _muzzle != null && shooter )
        {
            GameObject myBullet = Instantiate(bulletPrefab, _muzzle.position, Quaternion.identity) as GameObject;
            Bullet bulletComponent = myBullet.GetComponent<Bullet>();

            if(shooter.transform.localScale.x < 0f)
            {
                // Left
                bulletComponent.direction = Vector2.left; // vectpr(-1x, 0)
            } else
            {
                // Right
                bulletComponent.direction = Vector2.right; // vectpr(1x, 0)
            }
        }
    }
}
