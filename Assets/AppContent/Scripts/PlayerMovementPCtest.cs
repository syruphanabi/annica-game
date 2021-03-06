using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody), typeof(CapsuleCollider))]
public class PlayerMovementPCtest : MonoBehaviour
{
	public float OnGroundSpeed = 4f;
	public float OnJumpSpeed = 2f;
	public float sensitivity = 0.007f;

	private float speed;
	public Transform pcCamera;
	public bool isGrounded;
	Vector3 movement;
	Rigidbody playerRigidbody;
	Animator anim;
	// public float jumpableGroundNormalMaxAngle = 45f;
	// public bool closeToJumpableGround;
	//	public float jumpableGroundNormalMaxAngle = 45f;
	//	public bool closeToJumpableGround;
	private bool inAdjust;
	private bool mouseMove;
	private Vector3 beginCharacterToMouse;
	private Vector3 currentCharacterToMouse;
	private Vector3 beginMousePosition;
	private Vector3 currentMousePosition;
	private Vector3 offset;
	private bool W;
	private bool S;
	private bool A;
	private bool D;


	public AudioSource jumpSound;
	//Animator anim;
	//int floorMask;
	//float camRayLength = 100f;

	void Awake() {
		//floorMask = LayerMask.GetMask ("Floor");
		//anim = GetComponent <Animator> ();
		playerRigidbody = GetComponent <Rigidbody> ();
		anim = GetComponent <Animator> ();
		speed = OnGroundSpeed;
	}

	void Start() {
		// inAdjust = false;
		mouseMove = false;
		inAdjust = true;
		beginCharacterToMouse = pcCamera.position - transform.position;
		beginMousePosition = Input.mousePosition;
		isGrounded = true;
		offset = pcCamera.position - transform.position;
		W = false;
		S = false;
		A = false;
		D = false;
	}

	void Update(){
		if (Input.GetKeyDown (KeyCode.W) || Input.GetKeyDown (KeyCode.UpArrow)) {
			W = true;
		}
		if (Input.GetKeyUp (KeyCode.W) || Input.GetKeyUp (KeyCode.UpArrow)) {
			W = false;
		}

		if (Input.GetKeyDown (KeyCode.S) || Input.GetKeyDown (KeyCode.DownArrow)) {
			S = true;
		}
		if (Input.GetKeyUp (KeyCode.S) || Input.GetKeyUp(KeyCode.DownArrow)) {
			S = false;
		}

		if (Input.GetKeyDown (KeyCode.A) || Input.GetKeyDown (KeyCode.LeftArrow)) {
			A = true;
		}
		if (Input.GetKeyUp (KeyCode.A) || Input.GetKeyUp (KeyCode.LeftArrow)) {
			A = false;
		}

		if (Input.GetKeyDown (KeyCode.D) || Input.GetKeyDown (KeyCode.RightArrow)) {
			D = true;
		}
		if (Input.GetKeyUp (KeyCode.D) || Input.GetKeyUp (KeyCode.RightArrow)) {
			D = false;
		}

		if (Input.GetMouseButtonDown (0)) {
			mouseMove = true;
		}

		if (Input.GetMouseButtonUp (0)) {
			mouseMove = false;
		}

		if ((Input.GetKeyDown (KeyCode.Space) || Input.GetMouseButtonUp (1)) && isGrounded) {
			//Debug.Log ("get space down");
			playerRigidbody.AddForce (new Vector3 (0f, 330f, 0f));
			jumpSound.Play ();
			isGrounded = false;
			speed = OnJumpSpeed;
			//Debug.Log ("jump");
		}
	}
		
	void FixedUpdate() {
		float h = Input.GetAxisRaw ("Horizontal");
		float v = Input.GetAxisRaw ("Vertical");

		Move (h, v);

		anim.SetFloat ("Speed", Mathf.Abs(h + v));


		// if (Input.GetMouseButtonDown (1)) {
		// 	inAdjust = true;
		// 	beginCharacterToMouse = pcCamera.position - transform.position;
		// 	beginMousePosition = Input.mousePosition;
		// }
			
		// if (Input.GetMouseButtonUp (1)) {
		// 	inAdjust = false;
		// }

		if (inAdjust) {

			float distance = Mathf.Pow (beginCharacterToMouse.x, 2) + Mathf.Pow (beginCharacterToMouse.y, 2) + Mathf.Pow (beginCharacterToMouse.z, 2);
			distance = Mathf.Sqrt (distance);

			currentMousePosition = Input.mousePosition;
			float horizonShift = - (currentMousePosition - beginMousePosition).x * sensitivity;

			Vector3 tempCtoM;
			tempCtoM.x = beginCharacterToMouse.x * Mathf.Cos (horizonShift) - beginCharacterToMouse.z * Mathf.Sin(horizonShift);
			tempCtoM.z = beginCharacterToMouse.z * Mathf.Cos (horizonShift) + beginCharacterToMouse.x * Mathf.Sin(horizonShift);
			tempCtoM.y = beginCharacterToMouse.y;

			currentCharacterToMouse = tempCtoM;
			offset = currentCharacterToMouse;
			Quaternion newRotation = Quaternion.LookRotation (currentCharacterToMouse);
			newRotation.x = 0f;
			newRotation.z = 0f;
			transform.rotation = newRotation;

			if (inAdjust && mouseMove) {
				Move (0f, 100f);
				anim.SetFloat ("Speed", Mathf.Abs(100f));
			}

		}
			
//		anim.SetFloat ("Speed", Mathf.Abs(h + v));
//		anim.SetBool ("isWalking", true);
//		Turning ();
//		Animating (h, v);
	}

	void Move (float h, float v) {

		if (W || mouseMove) {
			this.transform.Translate (-Vector3.forward * Time.deltaTime * speed);
		} 

		if (A) {
			this.transform.Translate (-Vector3.left * Time.deltaTime * speed);
		}

		if (S) {
			this.transform.Translate (-Vector3.back * Time.deltaTime * speed);
		}

		if (D) {
			this.transform.Translate (-Vector3.right * Time.deltaTime * speed);
		}

//		if (h != 0f || v != 0f) {
//			this.transform.Translate (-Vector3.forward * Time.deltaTime * speed);
//		}

//		float faceDirection = Mathf.Acos(transform.rotation.y) * 2f;
//
//		float h2 = - v * Mathf.Sin (faceDirection) + h * Mathf.Cos (faceDirection);
//		float v2 = v * Mathf.Cos (faceDirection) + h * Mathf.Sin (faceDirection);
//
//		movement.Set (h2, 0f, v2);
//		movement = movement.normalized * speed * Time.deltaTime;
//		//playerRigidbody.MovePosition (transform.position + movement);
//		transform.position += movement;

//		if (h != 0f || v != 0f) {
//			anim.SetBool ("isWalking", true);
//		} else {
//			anim.SetBool ("isWalking", false);
//		}
	}

	void OnTriggerEnter(Collider other){
		//Debug.Log ("enter");
		if (other.gameObject.layer == 11)
		{
			isGrounded = true;
			speed = OnGroundSpeed;
			//Debug.Log ("is Grounded");
		}
	}

//	void Turning() {
//		Ray camRay = Camera.main.ScreenPointToRay (Input.mousePosition);
//
//		RaycastHit floorHit;
//
//		if (Physics.Raycast (camRay, out floorHit, camRayLength, floorMask)) {
//			Vector3 playerToMouse = - floorHit.point + transform.position;
//			playerToMouse.y = 0f;
//
//			Quaternion newRotation = Quaternion.LookRotation (playerToMouse);
//			playerRigidbody.MoveRotation (newRotation);
//		}
//	}

//	void Animating(float h, float v) {
//		bool walking = h != 0f || v != 0f;
//		anim.SetBool ("IsWalking", walking);
//	}
}
