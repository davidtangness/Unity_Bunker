using UnityEngine;
using System.Collections;

public class PlayerMovementController : MonoBehaviour {

	public Vector3 destination;
	public float moveSpeed;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void FixedUpdate () {

		float xDifference = destination.x - rigidbody2D.position.x;
		float yDifference = destination.y - rigidbody2D.position.y;
		float moveHorizontal = 0.0f;
		float moveVertical = 0.0f;

	

		if(Mathf.Abs (xDifference) >= 0.01){
			moveHorizontal = Mathf.Clamp (xDifference, -moveSpeed, moveSpeed);
		}
		if(Mathf.Abs (yDifference) >= 0.01){
			moveVertical = Mathf.Clamp (yDifference, -moveSpeed, moveSpeed);
		
		}

		Vector3 movement = new Vector3 (moveHorizontal, moveVertical, 0.0f);

		rigidbody2D.velocity = movement; 

	}



}
