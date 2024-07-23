using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRShoot : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform bulletPosition;
    [SerializeField] private float shootDelay = 0.2f;
    [Range(0, 3000), SerializeField] private float bulletSpeed;
    [Space, SerializeField] private AudioSource audioSource;
    [SerializeField] private Transform barrelLocation;
    public GameObject muzzleFlashPrefab;
    [Tooltip("Specify time to destory the casing object")] [SerializeField] private float destroyTimer = 2f;

    private float lastShot;

    public void Shoot()

    {
        if (muzzleFlashPrefab)
        {
            //Create the muzzle flash
            GameObject tempFlash;
            tempFlash = Instantiate(muzzleFlashPrefab, barrelLocation.position, barrelLocation.rotation);

            //Destroy the muzzle flash effect
            Destroy(tempFlash, destroyTimer);
        }
        if (lastShot > Time.time) return;

        lastShot = Time.time + shootDelay;

        GunShotAudio();

        var bulletPrefab = Instantiate(bullet, bulletPosition.position, bulletPosition.rotation);
        var bulletRB = bulletPrefab.GetComponent<Rigidbody>();
        var direction = bulletPrefab.transform.TransformDirection(Vector3.forward);
        bulletRB.AddForce(direction * bulletSpeed);
        Destroy(bulletPrefab, 2f);

    }

    private void GunShotAudio()
    {
        var random = Random.Range(0.8f, 1.2f);
        audioSource.pitch = random;

        audioSource.Play();
    }
}
