using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {


	public float sugar = 4000;
	public Animator anim;
	public Rigidbody rb;
	public int flyMaxSpeed = 100;
	public int walkMaxSpeed = 2;
	public float flyAcceleration = 25.0f;
	private float walkingAcceleration = 200.0f;
	public float walkRotate = 1.0f;

	private bool flying = false;
	private bool falling = false;

	private float walkingDrag = 40;

	private float flyUpForce = 250;
	private bool up = false;
	private float foward = 0;

	private float drasticAjust = 3.0f;
	private float fineAjust = 1.0f;

	private bool fallingForceApply = false;

	private Text sugarCount;
 
	void Start() {
		anim = GetComponent<Animator>();
		rb = GetComponent<Rigidbody>();

		sugarCount = GameObject.Find ("sugarCount").GetComponent<Text> ();

	}


	void Update () {
		sugarCount.text = "Sugar:" + (int)sugar;

		if (Input.GetKeyDown (KeyCode.Space)) {
			rb.useGravity = false;
			rb.velocity = Vector3.zero;
			transform.position += Vector3.up;
			flying = true;
			falling = false;
		} if (Input.GetKeyUp (KeyCode.Space)) {
			rb.useGravity = true;
			flying = false;
			falling = true;
		}

		if (flying || falling) {
			anim.SetBool ("flying", true);
		} else {
			anim.SetBool ("flying", false);
		}

		if (Input.GetKeyDown (KeyCode.LeftShift) && flying) {
			up = true;
		} else if (Input.GetKeyUp (KeyCode.LeftShift)){
			up = false;
		}

		if (flying) {
			fallingForceApply = false;
			sugar -= 0.5f;


			if (Input.GetKey (KeyCode.W)) {
				foward = flyAcceleration;

				if (transform.eulerAngles.x >= 330 || transform.eulerAngles.x < 40) {
					transform.RotateAround (transform.position, transform.right, fineAjust);
				} else if (transform.eulerAngles.x > 40 && transform.eulerAngles.x < 325 ){
					transform.RotateAround (transform.position, transform.right, -fineAjust);
				}
				
			
			} else if (Input.GetKey (KeyCode.S)) {
				foward = -flyAcceleration;

				if (transform.eulerAngles.x >= 320 || transform.eulerAngles.x < 30)
					transform.RotateAround (transform.position, transform.right, -fineAjust);
			} else {
				foward = 0;

				rb.angularVelocity = Vector3.zero;
				rb.velocity = Vector3.zero;

				if (transform.eulerAngles.x > 180 && transform.eulerAngles.x < 330) {
					transform.RotateAround (transform.position, transform.right, drasticAjust);
				} else if (transform.eulerAngles.x >= 330 && transform.eulerAngles.x <= 359) {
					transform.RotateAround (transform.position, transform.right, fineAjust);
				} else if (transform.eulerAngles.x > 30 && transform.eulerAngles.x <= 180) {
					transform.RotateAround (transform.position, transform.right, -drasticAjust);
				} else if (transform.eulerAngles.x > 1 && transform.eulerAngles.x <= 30) {
					transform.RotateAround (transform.position, transform.right, -fineAjust);
				}

			}
		

			if (Input.GetKey (KeyCode.A)) {
				
				transform.Rotate (transform.up * -walkRotate);

				if (transform.eulerAngles.z > 330 || transform.eulerAngles.z < 40)
					transform.RotateAround (transform.position, transform.forward, drasticAjust);
				else if (transform.eulerAngles.z > 40 && transform.eulerAngles.z < 325 ){
					transform.RotateAround (transform.position, transform.forward, -fineAjust);
				}
			
			} else if (Input.GetKey (KeyCode.D)) {
				transform.Rotate (transform.up * walkRotate);

				if (transform.eulerAngles.z > 320 || transform.eulerAngles.z < 30) {
					transform.RotateAround (transform.position, transform.forward, -drasticAjust);
				} else if (transform.eulerAngles.z > 35 && transform.eulerAngles.z < 325 ){
					transform.RotateAround (transform.position, transform.forward, fineAjust);
				}
			} else {
				
				if (transform.eulerAngles.z > 180 && transform.eulerAngles.z < 330) {
					transform.RotateAround (transform.position, transform.forward, drasticAjust);
				} else if (transform.eulerAngles.z >= 330 && transform.eulerAngles.z < 359) {
					transform.RotateAround (transform.position, transform.forward, fineAjust);
				} else if (transform.eulerAngles.z > 30 && transform.eulerAngles.z <= 180) {
					transform.RotateAround (transform.position, transform.forward, -drasticAjust);
				} else if (transform.eulerAngles.z > 1 && transform.eulerAngles.z <= 30) {
					transform.RotateAround (transform.position, transform.forward, -fineAjust);
				}

			}


		} else {

			if (!fallingForceApply) {
				rb.AddForce (transform.up * -1800);
				fallingForceApply = true;
			}


			if (Physics.Raycast (transform.position, transform.up * -2, 1)) {

			
				if (Input.GetKey (KeyCode.W)) {
				
					foward = walkingAcceleration;
					sugar -= 0.01f;
					anim.SetBool ("walking", true);
				} else if (Input.GetKey (KeyCode.S)) {
					foward = -walkingAcceleration;
					sugar -= 0.01f;
					anim.SetBool ("walking", true);
				} else {
					foward = 0;
					anim.SetBool ("walking", false);
				}

				if (Input.GetKey (KeyCode.A)) {
					transform.Rotate (new Vector3 (0, -walkRotate, 0));
					sugar -= 0.01f;
					anim.SetBool ("walking", true);
				} else if (Input.GetKey (KeyCode.D)) {
					transform.Rotate (new Vector3 (0, walkRotate, 0));
					sugar -= 0.01f;
					anim.SetBool ("walking", true);
				}

			}

		}



	}

	void FixedUpdate(){

		if (flying  && rb.velocity.magnitude > flyMaxSpeed) {
			rb.velocity = rb.velocity * 0.95f;
		}

		if (flying  && rb.velocity.magnitude < flyMaxSpeed || !flying && rb.velocity.magnitude < walkMaxSpeed) {

			if (up ) {
				if (foward == 0) {
					rb.AddForce (transform.up * (flyUpForce * 5 ));
				} else {
					rb.AddForce (transform.up * 10 );
				}
			}

			if (foward != 0 ) {
				rb.AddForce (transform.forward * foward);
			}

		}

		if (flying) {
			rb.drag = 0;
		} else
		if (falling) {
			//rb.AddForce (transform.up * 60);
			rb.drag = 4;
		}


	}

	public void addSugar(int add){
		sugar += add;
		sugarCount.text = "Sugar:" + sugar;
	}


	void OnCollisionEnter(){
		
	}

	void OnCollisionStay (Collision col) {
		if (Physics.Raycast (transform.position, transform.up * -2, 1)) {
			rb.drag = walkingDrag;
			falling = false;
		}

	}

	void OnCollisionExit (Collision col) {
		rb.drag = 0;
	}

}
