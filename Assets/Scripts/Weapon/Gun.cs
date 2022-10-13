using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour
{
    public float speed = 40f;
    public GameObject bullet;
    public Transform aimPoint;
    
    
    //Basic Features
    public float spread, reloadTime, timeBetweenShots, timeBetweenShooting;
    public int magSize, tapSize, bulletsLeft, bulletsShot;
    public bool allowHold, isReloading;
    private bool isShooting, isReadyToShoot;
    public float range = 100.0f;
    public int _damage = 10;

    //Player
    public GameObject player;

    //Visual 
    public GameObject muzzleFlash;

    //Audio 
    //public AudioSource audioSource;
    //public AudioClip audioClip;
    

    public void Awake()
    {
        bulletsLeft = magSize;
        isReadyToShoot = true;
        
    }

    private void Update()
    {

    }


    public void Shoot()
    {
        isReadyToShoot = false;
        isShooting = true;

        Vector3 directionSpread = aimPoint.forward;
        directionSpread.x += Random.Range(-spread /2.0f, spread / 2.0f);
        directionSpread.y += Random.Range(-spread, spread);
        GameObject spawnBullet = Instantiate(bullet, aimPoint.position, aimPoint.rotation);
        spawnBullet.GetComponent<Rigidbody>().velocity = speed * directionSpread.normalized;
        spawnBullet.GetComponent<Bullet>().SetDamage(_damage);

        bulletsLeft--;
        bulletsShot++;

        if (muzzleFlash != null)
        {
            //muzzleFlash.Emit(100);
        }
        
        //audioSource.PlayOneShot(audioClip);
    }

    private void ResetShoot()
    {
        isReadyToShoot = true;
    }

    //private IEnumerator Reload()
    //{
    //    isReloading = true;

    //    ReloadComplete();
    //}

    private void ReloadComplete()
    {
        bulletsLeft = magSize;
        isReloading = false;
    }

    public void IncreaseMagSize(int amount)
    {
        magSize += amount;
    }

    public void IncreaseDamage(int amount)
    {
        _damage += amount;
    }

    public int GetDamage()
    {
        return _damage;
    }

    public int GetMagSize()
    {
        return magSize;
    }
}
