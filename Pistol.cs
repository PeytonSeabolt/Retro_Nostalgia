using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class Pistol : MonoBehaviour {

    public GameObject bulletHole;
    public Sprite idlePistol;
    public Sprite shotPistol;
    public float pistolDamage;
    public float pistolRange;
    public AudioClip shotSound;
    public AudioClip reloadSound;
    public AudioClip emptyGunSound;
    public Text ammoText;
    public GameObject blood;

    public int ammoCount;
    public int ammoClipSize;

    bool isReloading;
    bool isShot;
    int ammoLeft;
    int ammoClipLeft;
    AudioSource source;

    void Awake()
    {
        source = GetComponent<AudioSource>();
        ammoLeft = ammoCount;
        ammoClipLeft = ammoClipSize;
    }

    private void OnEnable()
    {
        isReloading = false;
    }

    private void Update()
    {
        ammoText.text = ammoClipLeft + " / " + ammoLeft;

        if (Input.GetButtonDown("Fire1") && isReloading == false)
            isShot = true;
        if(Input.GetKeyDown(KeyCode.R) && isReloading == false && ammoClipLeft != ammoClipSize)
        {
            Reload();
        }
   
    }


    // the hitting method
     void FixedUpdate()
    {
        Vector2 bulletOffset = Random.insideUnitCircle * DynamicCrosshair.spread; // making our bullets stray, if spread = 0 then your shot is perf, otherwise can offset it!
        //adding the offset to our ray
        Vector3 randomTarget = new Vector3(Screen.width / 2 + bulletOffset.x, Screen.height/2 + bulletOffset.y, 0 );
        Ray ray = Camera.main.ScreenPointToRay(randomTarget);
        //returns a point when the ray hits an object
        RaycastHit hit;
        if(isShot == true && ammoClipLeft > 0 && isReloading == false)
        {
            ammoClipLeft--;
            isShot = false;
            DynamicCrosshair.spread += DynamicCrosshair.PISTOL_SHOOTING_SPREAD; //we use our const here! from Crosshairs
            source.PlayOneShot(shotSound);
            // corutine is so important, fuck threads
            StartCoroutine("Shot");
            //our ray, our hit coordinate, and our ray's range if we hit a collider
            if (Physics.Raycast(ray, out hit, pistolRange))    
            {
                if (hit.collider.CompareTag("Enemy"))
                {
                    Instantiate(blood, hit.point, Quaternion.identity); //identity reads the blood splatter prefab rotation.
                    if (hit.collider.gameObject.GetComponent<EnemyStates>().currentState == hit.collider.gameObject.GetComponent<EnemyStates>().patrolState ||
                        hit.collider.gameObject.GetComponent<EnemyStates>().currentState == hit.collider.gameObject.GetComponent<EnemyStates>().alertState)
                    {
                        hit.collider.gameObject.SendMessage("HiddenShot", transform.parent.transform.position, SendMessageOptions.DontRequireReceiver);
                    }
                    Debug.Log("I've collided with: " + hit.collider.gameObject.name);
                    hit.collider.gameObject.SendMessage("AddDamage", pistolDamage, SendMessageOptions.DontRequireReceiver);
                }
                else
                {
                    //fromtrotation sets the bullet holes rotation tothe object we hit.  Remember peyton that Quaternions are for rotating objects, vecttor3 cause the bullet was originally on the ground
                    Instantiate(bulletHole, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal)).transform.parent = hit.collider.gameObject.transform;
                }

            }
        }//if
        else if(isShot == true && ammoClipLeft <= 0 && isReloading == false)
        {
            isShot = false;
            Reload();
        }

    }
    void Reload()
    {

        int bulletsToReload = ammoClipSize - ammoClipLeft;
        if(ammoLeft>= bulletsToReload )
        {
            StartCoroutine("ReloadWeapon");
            ammoLeft -= bulletsToReload;
            ammoClipLeft = ammoClipSize;
        }
        else if (ammoLeft < bulletsToReload && ammoLeft > 0)
        {
            StartCoroutine("ReloadWeapon");
            ammoClipLeft += ammoLeft;
            ammoLeft = 0;
        }
        else if(ammoLeft<=0)
        {
            source.PlayOneShot(emptyGunSound);
        }
    }

    IEnumerator ReloadWeapon()
    {
        isReloading = true;
        source.PlayOneShot(reloadSound);
        yield return new WaitForSeconds(2);
        isReloading = false;
    }

    IEnumerator Shot()
    {
        GetComponent<SpriteRenderer>().sprite = shotPistol;
        //animation time!  pistols shoot fast
        yield return new WaitForSeconds(0.1f);
        GetComponent<SpriteRenderer>().sprite = idlePistol;

    }

    public void AddAmmo(int value)
    {
        ammoLeft += value;
   
    }
    
    }
