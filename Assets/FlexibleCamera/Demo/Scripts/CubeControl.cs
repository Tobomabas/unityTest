using UnityEngine;

namespace FlexibleCameraDemo
{
	public class CubeControl : MonoBehaviour
	{
		// This script is composed of Unity standard assets and altered for use in the demo.

		[SerializeField] float MovePower = 70000; // The force added to the cube to move it.
		[SerializeField] float DispositionPower = 3; // The disposition magnitude in case no physics are used for movement.
		[SerializeField] bool UsePhysics = false; // Whether to use force to move the cube.
		[SerializeField] float JumpPower = 2000; // The force added to the cube when it jumps.
		[SerializeField] float GroundRayLength = 5; // The maximum distance the cube can jump.

		Rigidbody m_Rigidbody;
		
		void Start ()
		{
			m_Rigidbody = GetComponent<Rigidbody> ();
		}

		void Update ()
		{
			float h = Input.GetAxis ("Horizontal");
			float v = Input.GetAxis ("Vertical");
			bool jump = Input.GetButton ("Jump");
			Vector3 move = (v * Vector3.forward + h * Vector3.right).normalized;

			if (UsePhysics)
			{
				m_Rigidbody.AddForce (move * MovePower * Time.deltaTime * 60);
			}
			else
			{
				gameObject.transform.position = gameObject.transform.position + move * Time.deltaTime * 60 * DispositionPower / 10;
			}

			if (Physics.Raycast (transform.position, -Vector3.up, GroundRayLength) && jump)
			{
				m_Rigidbody.AddForce (Vector3.up * JumpPower * Time.deltaTime * 60, ForceMode.Impulse);
			}
		}
	}
}
