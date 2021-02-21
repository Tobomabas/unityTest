using UnityEngine;

namespace FlexibleCameraDemo
{
	public class BallControl : MonoBehaviour
	{
		// This script is composed of Unity standard assets and altered for use in the demo.

		[SerializeField] float MovePower = 22000; // The force added to the ball to move it.
		[SerializeField] bool UseTorque = true; // Whether or not to use torque to move the ball.
		[SerializeField] float MaxAngularVelocity = 25; // The maximum velocity the ball can rotate at.
		[SerializeField] float JumpPower = 1000; // The force added to the ball when it jumps.
		[SerializeField] float GroundRayLength = 5; // The maximum distance the ball can jump.
		
		Rigidbody m_Rigidbody;
		
		void Start ()
		{
			m_Rigidbody = GetComponent<Rigidbody> ();
			GetComponent<Rigidbody> ().maxAngularVelocity = MaxAngularVelocity;
		}

		void Update ()
		{
			float h = Input.GetAxis ("Horizontal");
			float v = Input.GetAxis ("Vertical");
			bool jump = Input.GetButton ("Jump");
			Vector3 move = (v * Vector3.forward + h * Vector3.right).normalized;

			if (UseTorque)
			{
				m_Rigidbody.AddTorque (new Vector3 (move.z, 0, -move.x) * MovePower * Time.deltaTime * 60);
			}
			else
			{
				m_Rigidbody.AddForce (move * MovePower * Time.deltaTime * 60);
			}

			if (Physics.Raycast (transform.position, -Vector3.up, GroundRayLength) && jump)
			{
				m_Rigidbody.AddForce (Vector3.up * JumpPower * Time.deltaTime * 60, ForceMode.Impulse);
			}
		}
	}
}
