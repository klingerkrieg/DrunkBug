using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {


	public float sugar = 5000;
	public Animator anim;
	public Rigidbody rb;
	public float flySens = 0.4f;
	public int flyMaxSpeed = 50;
	public int walkMaxSpeed = 30;
	public float flyAcceleration = 25.0f;
	private float walkingAcceleration = 50.0f;
	public float walkRotate = 1.0f;

	private bool flying = false;

	private float foward = 0;
	private float xMovement = 0;

	private float drasticAjust = 3.0f;
	private float fineAjust = 1.0f;

	private Text sugarCount;

	void Start() {
		anim = GetComponent<Animator>();
		rb = GetComponent<Rigidbody>();

		sugarCount = GameObject.Find ("sugarCount").GetComponent<Text> ();

	}


	void Update () {
		sugarCount.text = "Sugar:" + (int)sugar;

		if (Input.GetKeyDown (KeyCode.Space)) {
			anim.SetBool ("flying", true);
			rb.useGravity = false;
			rb.velocity = Vector3.zero;
			transform.position += Vector3.up;
			flying = true;
		} if (Input.GetKeyUp (KeyCode.Space)) {
			anim.SetBool ("flying", false);
			rb.useGravity = true;
			flying = false;
		}

		if (flying) {

			sugar -= 0.5f;

			if (Input.GetKey (KeyCode.LeftShift)) {
				transform.position += new Vector3 (0, flySens, 0);
			}
			if (Input.GetKey (KeyCode.W)) {
				//Debug.Log (transform.forward);
				//transform.position += new Vector3 (0, 0, -flySens);
				foward = -flyAcceleration;
				if (transform.rotation.x > -0.2f)
					transform.Rotate (new Vector3 (-1.0f, 0, 0));
			} else if (Input.GetKey (KeyCode.S)) {
				foward = flyAcceleration;
				if (transform.rotation.x < 0.2f)
					transform.Rotate (new Vector3 (1.0f, 0, 0));
			} else {
				foward = 0;
				rb.velocity = Vector3.zero;

				if (transform.rotation.x > 0.3f) {
					transform.Rotate (new Vector3 (-drasticAjust, 0, 0));
				} else if (transform.rotation.x < -0.3f) {
					transform.Rotate (new Vector3 (drasticAjust, 0, 0));
				} else if (transform.rotation.x > 0.01f) {
					transform.Rotate (new Vector3 (-fineAjust, 0, 0));
				} else if (transform.rotation.x < -0.01f) {
					transform.Rotate (new Vector3 (fineAjust, 0, 0));
				}
			}
		
			/*if (Input.GetKey (KeyCode.A)) {
				xMovement = -1;
				//transform.localRotation
				//transform.position += new Vector3 (flySens, 0, 0);
				if (transform.rotation.z > -0.4f)
					transform.Rotate (new Vector3 (0, 0, -1.0f));
			} else if (Input.GetKey (KeyCode.D)) {
				//transform.position += new Vector3 (-flySens, 0, 0);
				xMovement = 1;
				if (transform.rotation.z < 0.4f)
					transform.Rotate (new Vector3 (0, 0, 1.0f));
			} else {
				xMovement = 0;
				if (transform.rotation.z > 0.3f) {
					transform.Rotate (new Vector3 (0, 0, -drasticAjust));
				} else if (transform.rotation.z < -0.3f) {
					transform.Rotate (new Vector3 (0, 0, drasticAjust));
				} else if (transform.rotation.z > 0.01f) {
					transform.Rotate (new Vector3 (0, 0, -fineAjust));
				} else if (transform.rotation.z < -0.01f) {
					transform.Rotate (new Vector3 (0, 0, fineAjust));
				}
			}*/

			if (Input.GetKey (KeyCode.A)) {
				transform.Rotate (new Vector3 (0, -walkRotate, 0));
				if (transform.rotation.z > -0.4f)
					transform.Rotate (new Vector3 (0, 0, -1.0f));
			} else if (Input.GetKey (KeyCode.D)) {
				transform.Rotate (new Vector3 (0, walkRotate, 0));
				if (transform.rotation.z < 0.4f)
					transform.Rotate (new Vector3 (0, 0, 1.0f));
			} else {

				if (transform.rotation.z > 0.3f) {
					transform.Rotate (new Vector3 (0, 0, -drasticAjust));
				} else if (transform.rotation.z < -0.3f) {
					transform.Rotate (new Vector3 (0, 0, drasticAjust));
				} else if (transform.rotation.z > 0.01f) {
					transform.Rotate (new Vector3 (0, 0, -fineAjust));
				} else if (transform.rotation.z < -0.01f) {
					transform.Rotate (new Vector3 (0, 0, fineAjust));
				}

			}


		} else {
			
			if (Input.GetKey (KeyCode.W)) {
				//Debug.Log (transform.forward);
				//transform.position += new Vector3 (0, 0, -flySens);
				foward = -walkingAcceleration;
				sugar -= 0.01f;
			} else if (Input.GetKey (KeyCode.S)) {
				foward = walkingAcceleration;
				sugar -= 0.01f;
			} else {
				foward = 0;
			}

			if (Input.GetKey (KeyCode.A)) {
				transform.Rotate (new Vector3 (0, -walkRotate, 0));
				sugar -= 0.01f;
			} else if (Input.GetKey (KeyCode.D)) {
				transform.Rotate (new Vector3 (0, walkRotate, 0));
				sugar -= 0.01f;
			}

		}




	}

	void FixedUpdate(){


		if (foward != 0 && flying && rb.velocity.magnitude < flyMaxSpeed || !flying && rb.velocity.magnitude < walkMaxSpeed) {
			rb.AddForce (transform.forward * foward);
		}
		if (xMovement == 1) {
			transform.Translate(-flySens,0,0);
		} else
		if (xMovement == -1) {
			transform.Translate(flySens,0,0);
		}
	}

	public void addSugar(int add){
		sugar += add;
		sugarCount.text = "Sugar:" + sugar;
	}

}
