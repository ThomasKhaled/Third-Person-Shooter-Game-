using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterScript : MonoBehaviour
{
    [SerializeField] float rateOfFire;
    [SerializeField] Projectile projectile;
    [SerializeField] Transform hand;
    [SerializeField] Audiocontroller audioReload;
    [SerializeField] Audiocontroller audioFire;
    [SerializeField] Transform aimTarget;

    [HideInInspector]
    public Transform muzzle;

    private WeaponReloader reloader;
    private ParticleSystem muzzleFireParticleSystem;


    float nextFireAllowed;
    public bool canFire;

    private void Awake()
    {
        muzzle = transform.Find("Muzzle");
        reloader = GetComponent<WeaponReloader>();
        muzzleFireParticleSystem = muzzle.GetComponent<ParticleSystem>();

        transform.SetParent(hand);
    }

    public void Reload()
    {
        if (reloader == null)
            return;
        if (reloader.Reload())
            audioReload.Play();
    }

    void FireEffect()
    {
        if (muzzleFireParticleSystem == null)
            return;
        muzzleFireParticleSystem.Play();
    }

    public virtual void Fire()
    {
        canFire = false;
        if (Time.time < nextFireAllowed)
            return;

        if (reloader != null)
        {
            if (reloader.IsReloading)
                return;
            if (reloader.RoundsRemainingInClip == 0)
                return;

            reloader.TakeFromClip(1);
        }

        nextFireAllowed = Time.time + rateOfFire;

        bool isLocalPlayerControlled = aimTarget == null;
        if (!isLocalPlayerControlled)
            muzzle.LookAt(aimTarget);

        Projectile newBullet = (Projectile)Instantiate(projectile, muzzle.position, muzzle.rotation);

        if (isLocalPlayerControlled)
        {
            Ray ray = Camera.main.ViewportPointToRay(new Vector3(.5f, .5f, 0));
            RaycastHit hit;
            Vector3 targetPosition = ray.GetPoint(500);
            if (Physics.Raycast(ray, out hit))
            {
                targetPosition = hit.point;
            }
            newBullet.transform.LookAt(targetPosition);
        }

        FireEffect();
        audioFire.Play();
        canFire = true;
    }
}
