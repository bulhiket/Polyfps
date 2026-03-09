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
    public bool canSwitch = true;


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
    public ParticleSystem hitEffect;
    


    void Awake()
    {
        bulletsLeft = magazineSize;
        readyToShoot = true;
        fpsCam = GetComponentInParent<Camera>();
        _animator = GetComponent<Animator>();
        _source = GetComponent<AudioSource>();
        
        
    }

    void OnEnable()
    {
        UpdateUI();
        canSwitch = true;
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
        canSwitch = false;
        reloading = true;

        if(allBullets <= 0) return;

        int bulletsNeeded = magazineSize - bulletsLeft;
        int bulletsToAdd = Mathf.Min(bulletsNeeded, allBullets);

        

        _source.PlayOneShot(reloadSound);
        _animator.SetBool("Reload", true);

        Invoke("ReloadFinish", reloadTime);

        bulletsLeft += bulletsToAdd;
        allBullets -= bulletsToAdd;


    }

    public void ReloadFinish()
    {
        
        reloading = false;
        _animator.SetBool("Reload", false);
        UpdateUI();
        canSwitch = true;
    }

    private void Shoot()
    {
        
        readyToShoot = false;



        if(Physics.Raycast(fpsCam.transform.position, transform.forward, out rayHit, range, whatIsEnemy))
        {
            Debug.Log(rayHit.collider.name);
            if(rayHit.collider.CompareTag("Labubu"))
            {
                rayHit.collider.GetComponent<BotHPManager>().TakeDamage(damage);

                UIManager.Instance.SetHitWithDelay();
                
            }
        }

        mLook.RecoilAdd();
        _animator.SetTrigger("Shoot");
        muzzleFlash.Play();

        bulletsLeft--;
        bulletsShot--;
        Invoke("ResetShoot", timeBetweenShooting);

        if(bulletsShot > 0 && bulletsLeft > 0) Invoke("Shoot", timeBetweenShots);

        UpdateUI();
        
    }

    private void ResetShoot()
    {
        readyToShoot = true;
        
    }

    public void UpdateUI()
    {
        UIManager.Instance.SetAmmoUI(allBullets, bulletsLeft);
    }

    
}
