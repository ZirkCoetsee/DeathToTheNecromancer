using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

[RequireComponent (typeof(CharacterController))]
[RequireComponent (typeof(WeaponController))]
public class Player : DamageableEntity
{
    [SerializeField] public float dashTime = 0.3f;
    [SerializeField] public float dashSpeed = 10f;
    [SerializeField] LayerMask aimLayerMask;
    //Have camera follow playerContainer but only move the container do not rotate
    [SerializeField] GameObject playerContainer;
    [SerializeField] GameObject additionalPlane;

    private bool isDashing;
    // How you initiate dash
    private float doubleTapTime;
    KeyCode lastKeyCode;
    float horizontalInput;
    float verticalInput;

    public enum State { Active, Stopped};
    public State currentState;

    Animator _animator;
    CharacterController characterController;
    WeaponController weaponController;
  

    void Awake() => _animator = GetComponent<Animator>();

    // Start is called before the first frame update
    //NB OVERRIDE!!!!! or this start will override the DamageableEnity start and thus the DamageableEntity will never start haha

    protected override void Start()
    {
        base.Start ();
        characterController = GetComponent<CharacterController>();
        weaponController = GetComponent<WeaponController>();
        currentState = State.Active;


    }

    // Update is called once per frame
    void Update()
    {
        if (currentState == State.Active)
        {
            AimCharacter();

            horizontalInput = Input.GetAxis("Horizontal");
            verticalInput = Input.GetAxis("Vertical");
            Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput);

            // Moving 
            if (movement.magnitude > 0)
            {
                //This clips through objects
                //Keep this for animations for now
                movement.Normalize();
                movement *= this.maxSpeed * Time.deltaTime;
                //transform.Translate(movement,Space.World);

                //Uses Unity Character controller instead
                characterController.Move(new Vector3(horizontalInput * this.maxSpeed, 0, verticalInput * this.maxSpeed) * Time.deltaTime);
            }
            //playerContainer.transform.position = new Vector3(transform.position.x, 0, transform.position.z);

            //Dashing
            PlayerDash();

            // Animating
            float velocityZ = Vector3.Dot(movement.normalized, transform.forward);
            float velocityX = Vector3.Dot(movement.normalized, transform.right);

            // _animator.SetFloat("VelocityZ", velocityZ, 0.1f, Time.deltaTime);
            // _animator.SetFloat("VelocityX", velocityX, 0.1f, Time.deltaTime);

            //Weapon Input
            //0 refers to left mouse button
            //Can improve this to check only when user lifts the button as to charge the attack??

            //if (Input.GetMouseButton(0))
            if (Input.GetMouseButtonUp(0))
            {
                weaponController.Shoot();
            }

        }

    }

    void AimCharacter(){
   
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
           // Debug.Log("Aiming at: " + Input.mousePosition);
            //Debug.Log("Camera Position: " + Camera.main.transform.position);
            //Debug.DrawRay(ray.origin, ray.direction * 20, Color.red);


        if (Physics.Raycast(ray,out var hitInfo, Mathf.Infinity, aimLayerMask))
            {
           // Debug.Log("Hit");

                var direction = hitInfo.point - transform.position;
                direction.y = 0f;
                direction.Normalize();
                transform.forward = direction;
            }
    }

     private void PlayerDash(){
        
             // Dashing One click
             if(Input.GetKeyDown(KeyCode.LeftShift)){
                 // playerSound.PlayOneShot(playerClips[2]);

                 StartCoroutine(DashCoroutine());
             }
     }

     private IEnumerator DashCoroutine()
     {
             float startTime = Time.time; // need to remember this to know how long to dash
             while(Time.time < startTime + dashTime)
             {
                 characterController.Move(new Vector3(horizontalInput * this.maxSpeed,0,verticalInput * this.maxSpeed) * Time.deltaTime);
                 // transform.Translate(transform.forward * _dashSpeed * Time.deltaTime);
                 // or controller.Move(...), dunno about that script
                 yield return null; // this will make Unity stop here and continue next frame
            }
    }
}
