using UnityEngine;
using System.Collections;

public class BunkerClickController : MonoBehaviour {

	private PlayerMovementController PlayerMover;

	private GameObject PlayerObject;

	// Use this for initialization
	void Start () {
		PlayerObject = GameObject.FindWithTag ("Player");
		PlayerMover = PlayerObject.GetComponent < PlayerMovementController > ();

	}

	void OnMouseDown(){
		PlayerMover.destination = rigidbody2D.position;

	}

}
