using UnityEngine;
using System.Collections;

public class BunkerClickController : MonoBehaviour {

	private CharacterMovementController PlayerMover;

	private GameObject PlayerObject;

	// Use this for initialization
	void Start () {
		PlayerObject = GameObject.FindWithTag ("Player");
		PlayerMover = PlayerObject.GetComponent < CharacterMovementController > ();

	}

	void OnMouseDown(){
		PlayerMover.destination = rigidbody2D.position;

	}

}
