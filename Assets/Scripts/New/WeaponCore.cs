using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCore : MonoBehaviour
{
    public int damage = 25;
    public int magazineSize = 30;
    public int bulletsPerTap;
    public int allBullets = 100;

    public int bulletsLeft;
    private int bulletsShot;


    public float timeBetweenShooting = 1f;
    public float spread;
    public float range;
    public float reloadTime;
    public float timeBetweenShots;

    private bool shooting;
    private bool readyToShoot;
    private bool reloading;
    public bool allowButtonHold;


    public Camera fpsCam;
    public RaycastHit rayHit;
    public LayerMask whatIsEnemy;

    public MouseLook mLook;

    private Animator _animator;


    public Texture2D crosshair;
    public float crosshairSize = 25f;

    public AudioClip shootSound;
    public AudioClip reloadSound;
    private AudioSource _source;
    public ParticleSystem muzzleFlash;


    void Awake()
    {
        bulletsLeft = magazineSize;
        readyToShoot = true;
        fpsCam = GetComponentInParent<Camera>();
        _animator = GetComponent<Animator>();
        // mLook = FindAnyObjectByType<MouseLook>();
        _source = GetComponent<AudioSource>();
        
    }

    void Update()
    {
        InputHandle();
    }

    private void InputHandle()
    {
        if(allowButtonHold) shooting = Input.GetKey(KeyCode.Mouse0);
        else shooting = Input.GetKeyDown(KeyCode.Mouse0);

        if(Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !reloading) Reload();

        if(readyToShoot && shooting && !reloading && bulletsLeft > 0)
        {
            bulletsShot = bulletsPerTap;
            Shoot();
            _source.PlayOneShot(shootSound);
            Debug.Log("Shoot");
        }
    }

    private void Reload()
    {
        reloading = true;

        if(allBullets <= 0) return;

        int bulletsNeeded = magazineSize - bulletsLeft;
        int bulletsToAdd = Mathf.Min(bulletsNeeded, allBullets);

        bulletsLeft += bulletsToAdd;
        allBullets -= bulletsToAdd;

        _source.PlayOneShot(reloadSound);
        _animator.SetBool("Reload", true);

        Invoke("ReloadFinish", reloadTime);


    }

    public void ReloadFinish()
    {
        
        reloading = false;
        _animator.SetBool("Reload", false);
    }

    private void Shoot()
    {
        readyToShoot = false;



        if(Physics.Raycast(fpsCam.transform.position, transform.forward, out rayHit, range, whatIsEnemy))
        {
            Debug.Log(rayHit.collider.name);
            if(rayHit.collider.tag == "Labubu")
            {
                rayHit.collider.GetComponent<BotHPManager>().TakeDamage(damage);
            }
        }

        mLook.RecoilAdd();
        _animator.SetTrigger("Shoot");
        muzzleFlash.Play();

        bulletsLeft--;
        bulletsShot--;
        Invoke("ResetShoot", timeBetweenShooting);

        if(bulletsShot > 0 && bulletsLeft > 0) Invoke("Shoot", timeBetweenShots);
        
    }

    private void ResetShoot()
    {
        readyToShoot = true;
    }

    void OnGUI()
    {
        float centerX = Screen.width / 2;
        float centerY = Screen.height / 2;

        GUI.DrawTexture(new Rect(centerX - crosshairSize / 2, centerY - crosshairSize / 2, crosshairSize, crosshairSize), crosshair);
    }
}
