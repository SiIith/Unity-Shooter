using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttack : MonoBehaviour
{
    private float firerate = 1f;

    private float damage = 2f;
    public Image damageBar;
    public Image fireRateBar;

    [SerializeField]
    private ParticleSystem muzzleParticle;
    public GameObject hitParticle;

    private float timer;

    //Laser visualisation
    private LineRenderer laserLine;
    private WaitForSeconds shotDuration = new WaitForSeconds(0.07f);
    public Transform Firepoint;
    public float weaponRange = 50f;

    void Start()
    {
        damageBar.fillAmount = (float)damage/(float)3;
        fireRateBar.fillAmount = 1.0f - (firerate - 0.2f)/1.5f;

        laserLine = GetComponent<LineRenderer>();
    }

 
    // Update is called once per frame
    void Update()
    {
        
        timer += Time.deltaTime;
        if(timer >= firerate)
        {
            if (Input.GetButton("Fire1"))
            {
                muzzleParticle.Play();
                timer = 0f;
                FireGun();
            }
        }
        
        fireRateBar.fillAmount = 1.0f - (firerate-0.1f)/1.5f;
        damageBar.fillAmount = (float)damage/(float)5;
        
    }

    private void FireGun()
    {
 
        muzzleParticle.Play();
        FindObjectOfType<AudioManager>().Play("FireLaser");

        Ray ray = Camera.main.ViewportPointToRay(Vector3.one * 0.5f);
        Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 2f);
        StartCoroutine(ShotEffect());

        laserLine.SetPosition(0, Firepoint.position);

        RaycastHit hitInfo;

        if(Physics.Raycast(ray, out hitInfo, 100))
        {
            laserLine.SetPosition(1, hitInfo.point);
            var health = hitInfo.collider.GetComponent<Health>();
            GameObject impact = Instantiate(hitParticle, hitInfo.point,Quaternion.LookRotation(hitInfo.normal));
            Destroy(impact,2f);
            if (health != null)
            {
                health.TakeDamage(damage);
            }
        }
        else
        {
            laserLine.SetPosition(1, ray.origin + (Camera.main.transform.forward * weaponRange));
        }
    }

    public void IncreaseFireRate(){
        if(firerate >= 0.2){
            firerate -= 0.2f;
        }
    }

    public void Increasdamage() {
        if(damage<=5){
            damage += 1f;
        }
    }

    public float getDamage(){
        return damage;
    }

    public float getFireRate(){
        return firerate;
    }

    private IEnumerator ShotEffect()
    {

        // Turn on our line renderer
        laserLine.enabled = true;

        //Wait for .07 seconds
        yield return shotDuration;

        // Deactivate our line renderer after waiting
        laserLine.enabled = false;
    }

}
