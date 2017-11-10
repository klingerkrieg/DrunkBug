using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntiBug : MonoBehaviour {

	void OnCollisionEnter (Collision col) {
		Vector3 vec = col.gameObject.transform.position;
		vec.y = 50;
		col.gameObject.transform.position = vec;
	}
}
