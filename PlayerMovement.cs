using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour {
    public FlashScreen flash;
    public float playerWalkingSpeed = 5f;
    public float playerRunningSpeed = 15f;
    public float jumpStrength = 5f;
    public float verticalRotationLimit = 80f;
    float verticalVelocity;
    float forwardMovement;
    float sidewaysMovement;
    float verticalRotation = 0;
    AudioSource source;
    CharacterController cc;
    public AudioClip pickupSound;
    private void Awake()
    {
        cc = GetComponent<CharacterController>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        //Looking Around

        // Horizontal
        float horizontalRotation = Input.GetAxis("Mouse X");
        transform.Rotate(0, horizontalRotation, 0);

        //Vertical
        verticalRotation -= Input.GetAxis("Mouse Y");
        //limiting the angle of the camera looking up
        verticalRotation = Mathf.Clamp(verticalRotation, -verticalRotationLimit, verticalRotationLimit);
        //moving the camera but not rotating the object.
        Camera.main.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);

        //walking
        if (cc.isGrounded)
        {
            forwardMovement = Input.GetAxis("Vertical") * playerWalkingSpeed;
            sidewaysMovement = Input.GetAxis("Horizontal") * playerWalkingSpeed;
            //left Shift
            if (Input.GetKey(KeyCode.LeftShift))
            {
        
                forwardMovement = Input.GetAxis("Vertical") * playerRunningSpeed;
                sidewaysMovement = Input.GetAxis("Horizontal") * playerRunningSpeed;
            }
            if(Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0)
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    DynamicCrosshair.spread = DynamicCrosshair.RUN_SPREAD;
                }
                else { DynamicCrosshair.spread = DynamicCrosshair.WALK_SPREAD; }
            }
        }

        else { DynamicCrosshair.spread = DynamicCrosshair.JUMP_SPREAD; }
        // Gravity
        verticalVelocity += Physics.gravity.y * Time.deltaTime;

        
        if (Input.GetButton("Jump") && cc.isGrounded)
        {
       
            verticalVelocity = jumpStrength;
        }

        Vector3 playerMovement = new Vector3(sidewaysMovement, verticalVelocity, forwardMovement);
        cc.Move(transform.rotation * playerMovement * Time.deltaTime);

    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("HpBonus"))
        {
            GetComponent<PlayerHealth>().AddHealth(20);
        }
        else if (other.CompareTag("ArmorBonus"))
        {
            GetComponent<PlayerHealth>().AddArmor(50);
        }
        else if(other.CompareTag("AmmoBonus"))
        {

            transform.Find("Weapons").transform.Find("PistolHand").GetComponent<Pistol>().AddAmmo(15);
            transform.Find("Weapons").transform.Find("RocketHand").GetComponent<RocketLauncher>().AddAmmo(3);
            transform.Find("Weapons").transform.Find("RifleHand").GetComponent<Rifle>().AddAmmo(30);
        }
        if (other.CompareTag("HpBonus") || other.CompareTag("ArmorBonus") || other.CompareTag("AmmoBonus"))
        {
            flash.PickedUpBonus();
            //source.PlayOneShot(pickupSound);
            Destroy(other.gameObject);
        }

    }

}


