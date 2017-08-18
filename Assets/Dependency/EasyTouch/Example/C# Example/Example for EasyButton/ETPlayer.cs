using UnityEngine;
using System.Collections;

public class ETPlayer : MonoBehaviour {
	
	public GameObject bullet;
	
	private Transform model;
	private Transform gun;
	
	void OnEnable(){
		EasyJoystick.On_JoystickMove += On_JoystickMove;	
		EasyJoystick.On_JoystickMoveEnd += On_JoystickMoveEnd;
		EasyButton.On_ButtonUp += On_ButtonUp;	
	}

	void Fire(){
		Instantiate( bullet, gun.transform.position, gun.rotation);
	}


	void OnDisable(){
		EasyJoystick.On_JoystickMove -= On_JoystickMove;	
		EasyJoystick.On_JoystickMoveEnd -= On_JoystickMoveEnd;
		EasyButton.On_ButtonUp -= On_ButtonUp;	
	}
		
	void OnDestroy(){
		EasyJoystick.On_JoystickMove -= On_JoystickMove;	
		EasyJoystick.On_JoystickMoveEnd -= On_JoystickMoveEnd;
		EasyButton.On_ButtonUp -= On_ButtonUp;	
	}
	
	void Start(){
		model = transform.FindChild("Model").transform;	
		gun = transform.FindChild("Gun").transform;	
	}
	
	void On_JoystickMove( MovingJoystick move){
	
		float angle = move.Axis2Angle(true);
		transform.rotation  = Quaternion.Euler( new Vector3(0,angle,0));
		transform.Translate( Vector3.forward * move.joystickValue.magnitude * Time.deltaTime);	
		
		model.GetComponent<Animation>().CrossFade("Run");

	}
	
	void On_JoystickMoveEnd (MovingJoystick move)
	{
		model.GetComponent<Animation>().CrossFade("idle");
	}
	
	void On_ButtonUp (string buttonName)
	{
		
	}	
}
