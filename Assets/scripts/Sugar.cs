using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sugar : MonoBehaviour {

	public int sugar = 5;

	void OnCollisionEnter (Collision col) {
		col.gameObject.GetComponent<Player> ().addSugar(sugar);
		Destroy (this.gameObject);
	}
}
