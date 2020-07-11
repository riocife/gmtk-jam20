using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public Transform weapon;

    public float bulletForce = 20f;

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && weapon.gameObject.activeInHierarchy)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bullet.GetComponent<Rigidbody2D>().AddForce(-firePoint.up * bulletForce, ForceMode2D.Impulse);
    }
}
