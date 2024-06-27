using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    	//Player Movement Vars
	private CharacterController characterController;
	public float walkSpeed = 5f;
	public float runSpeed = 10f;
	public float strafeSpeed = 5f;
	public float rotationalSpeed = 5f;
	//Player Camera stuff
	public float cameraRotation;
	public float tiltSpeed = 5f;
	public float maxTiltAngle = 45f;
	// Raycasting stuff
	public Transform playerCamera;
	// Health stuff
	//TODO add health
	//public float playerHealth = 100f;
#region UnityEvents

	private void Awake()
	{
		characterController = GetComponent<CharacterController>();
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}

	private void Update()
	{
		Move();
		if (Input.GetKey(KeyCode.Space))
		{
			Shoot();
		}
	}
	
	
#endregion
 #region Movement
 public void Move()
	{
		float currentSpeed = IsRunning()
			? runSpeed
			: walkSpeed;
		float forwardSpeed = ForwardDirection() * currentSpeed;
		Vector3 movementDirection = (transform.forward * forwardSpeed) + (transform.right * SidewaysDirection() * strafeSpeed);
		characterController.Move(movementDirection * Time.deltaTime);
		//Camera rotation
		transform.rotation *= Quaternion.Euler(0, RotationY() * rotationalSpeed, 0);
		cameraRotation += TiltCamera() * tiltSpeed;
		cameraRotation = Mathf.Clamp(cameraRotation, -maxTiltAngle, maxTiltAngle);
		playerCamera.transform.localRotation = Quaternion.Euler(cameraRotation, 0, 0);
	}
 public float onScreenUpValue;
	public float onScreenDownValue;
	public float onScreenLeftValue;
	public float onScreenRightValue;
	private float OnScreenHorizontal
	{
		get { return onScreenRightValue - onScreenLeftValue; }
	}
	private float OnScreenVertical
	{
		get { return onScreenUpValue - onScreenDownValue; }
	}
	
	float ForwardDirection()
	{
		if(Input.GetKey(KeyCode.W))
		{
			return 1;
		}
		if(Input.GetKey(KeyCode.S))
		{
			return -1;
		}

		return OnScreenVertical;
	}
	
	float SidewaysDirection()
	{
		if(Input.GetKey(KeyCode.D))
		{
			return 1;
		}
		if(Input.GetKey(KeyCode.A))
		{
			return -1;
		}

		return OnScreenHorizontal;
	}
	
	bool IsRunning()
	{
		if(Input.GetKey(KeyCode.LeftShift))
		{
			return true;
		}

		return false;
	}
	
#endregion
#region Camera Stuff

	float RotationY()
	{
		return Mathf.Clamp(Input.GetAxis("Mouse X"),-1,1);
	}

	float TiltCamera()
	{
		return Mathf.Clamp(-Input.GetAxis("Mouse Y"), -1, 1);
	}
#endregion
#region Weapon Use
 void Shoot()
{
	Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
	RaycastHit hit;
	if(Physics.Raycast(ray, out hit))
	{
		Debug.Log(hit.point);
	}
}

public void PickUp()
{
	
}

public void Reload()
{
	
}

#endregion

#region Health

void HealthDown()
{
	
}

void HealthUp()
{
	
}


#endregion


}
