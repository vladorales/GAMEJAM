using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterController))]
public class TwinStickMovement : MonoBehaviour
{
    public S_Shoot shoot;
    public Animator anim;
	#region Data Types
	[Header("Player Stats")]
	[SerializeField] public float playerSpeed = 10f;
	[SerializeField] public float sideSpeed = 0;
	[SerializeField] public float playerHealth = 1f;
	[SerializeField] private readonly float gravityValue = -9.81f;
	[SerializeField] private readonly float controllerDeadzone = .1f;
	[SerializeField] private readonly float RotateSmoothing = 1000f;
    public Slider HPBAR;
    public GameObject deadCanv;

	[SerializeField] public AudioSource dodgeSound;
	private float ogSpeed;

	[SerializeField] private bool isGamepad;

	private CharacterController controller;

	private Vector2 movement;
	private Vector2 aim;

	[Header("Shooting Stats")]
	public float fireRate = 1f;
	private float NextTimetoFire = 1f;
	public float dodgeTiming = 5f;
	public float dashDistance = 100f;
	public float refreshTiming = 5.0f;

	private float elapsedTime;
	[SerializeField] bool canDodge = true;


	private Vector3 playerVelocity;

	private PlayerControls playerControls;
	private PlayerInput playerInput;

	private Rigidbody rb;
	private Collider col;
	public Transform particleSpwn;

	public GameObject dodgeParticle;

	float percentageComplete;
	#endregion

	#region Initializing
	private void Awake()
	{
		controller = GetComponent<CharacterController>();
		playerControls = new PlayerControls();
		playerInput = GetComponent<PlayerInput>();
		rb = GetComponent<Rigidbody>();
		col = GetComponent<Collider>();
		ogSpeed = playerSpeed;


		
	}

	private void OnEnable()
	{
		playerControls.Enable();
	}

	private void OnDisable()
	{
		playerControls.Disable();
	}
	// Start is called before the first frame update
	#endregion
	// Update is called once per frame
	void Update()
    {
		HandleInput();
		HandleMovement();
		HandleRotation();
		MouseInput();
		DodgeInput();
		CheckHealth();
        animSetup();
		elapsedTime += Time.deltaTime;
		percentageComplete = elapsedTime / dashDistance;
		
    }
	#region Button Input
	private void DodgeInput()
	{
		if (playerControls.Controls.Dodge.ReadValue<float>() > .1f && canDodge == true)
		{
			
			Dodge();
		}
	}

	private void Dodge()
	{
		
		
		/*GameObject particle = Instantiate(dodgeParticle);
		particle.transform.position = gameObject.transform.position;
		Vector3 move = new Vector3(movement.x, 0, movement.y);
		
		controller.Move(move * dashDistance * Time.deltaTime);*/
		dodgeSound.Play();
		dodgeParticle.SetActive(true);
		StartCoroutine(DodgeTiming(dodgeTiming));
		Physics.IgnoreLayerCollision(3, 6);
		sideSpeed = 10;
		playerSpeed = playerSpeed + 10;
		canDodge = false;
		Debug.Log("Dodge");
		

	}

	private void MouseInput()
	{
		bool isMouseButtonHeld = playerControls.Controls.Shooting.ReadValue<float>() > .1f;

		if (isMouseButtonHeld)
		{
            anim.SetBool("swordATT", true);
			
		}

        if (Mouse.current.rightButton.isPressed)
        {
            shoot.tripShot = true;
        }
        else
        {
            shoot.tripShot = false;
        }
	}
    private void animSetup()
    {
        if ((Keyboard.current.wKey.isPressed) || (Keyboard.current.sKey.isPressed) || (Keyboard.current.aKey.isPressed) || (Keyboard.current.dKey.isPressed))
        {
            anim.SetBool("isRun", true);
        }
      
        else
        {
            anim.SetBool("isRun", false);
        }
    }



	IEnumerator DodgeTiming (float delay)
	{
		yield return new WaitForSeconds(delay);
		dodgeSound.Stop();
		Physics.IgnoreLayerCollision(3, 6, false);
		sideSpeed = 0;
		playerSpeed = ogSpeed;
		dodgeParticle.SetActive(false);
		StartCoroutine(DodgeRefresh(refreshTiming));
	}
	IEnumerator DodgeRefresh (float delay)
	{
		yield return new WaitForSeconds(delay);
		canDodge = true;
	}
	#endregion
	#region Movement

	void HandleRotation()
		{
			if (isGamepad)
			{
			
				if(Mathf.Abs(aim.x) > controllerDeadzone || Mathf.Abs(aim.y)> controllerDeadzone)
				{
					Vector3 playerDirection = Vector3.right * aim.x + Vector3.forward * aim.y;
					if (playerDirection.sqrMagnitude > 0.0f)
					{
						Quaternion newRotation = Quaternion.LookRotation(playerDirection, Vector3.up);
						transform.rotation = Quaternion.RotateTowards(transform.rotation, newRotation, RotateSmoothing * Time.deltaTime);
						
					}
				}
			}
			else
			{
				Ray ray = Camera.main.ScreenPointToRay(aim);
				Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
			

				if (groundPlane.Raycast(ray, out float rayDistance))
				{
					Vector3 point = ray.GetPoint(rayDistance);
					LookAt(point);
				
				}
			}
		}
	private void LookAt(Vector3 lookPoint)
	{
		Vector3 heightCorrectedPoint = new Vector3(lookPoint.x, transform.position.y, lookPoint.z);
		transform.LookAt(heightCorrectedPoint);
	}


	 void HandleMovement()
	{
		Vector3 move = new Vector3(movement.x, 0, movement.y);
		controller.Move(move * Time.deltaTime * playerSpeed);

		playerVelocity.y += gravityValue * Time.deltaTime;
		controller.Move(playerVelocity * Time.deltaTime);
	}

	 void HandleInput()
	{
		movement = playerControls.Controls.Movement.ReadValue<Vector2>();
		aim = playerControls.Controls.Aim.ReadValue<Vector2>();
	}

	public void OnDeviceChange (PlayerInput pi)
	{
		isGamepad = pi.currentControlScheme.Equals("Gamepad") ? true : false;
	}
    #endregion

    public void LoseHealth(float healthLoss)
    {
        playerHealth = playerHealth - healthLoss;
    }

	void CheckHealth()
	{
        HPBAR.value = playerHealth;
		if (playerHealth <= 0)
		{
            Time.timeScale = 0.1f;
			Debug.Log("You're Dead");
            deadCanv.SetActive(true);
		}
	}

  
}
