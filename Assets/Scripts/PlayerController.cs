using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{

  [SerializeField]
  private float speed = 15f;

  [SerializeField]
  private float lookSensitivity = 2f;

  private PlayerMotor motor;

  void Start()
  {
    motor = GetComponent<PlayerMotor>();
  }

  void Update()
  {
    float xAxis = Input.GetAxisRaw("Horizontal");
    float zAxis = Input.GetAxisRaw("Vertical");

    Vector3 moveHorizontal = transform.right * xAxis;
    Vector3 moveVertical = transform.forward * zAxis;

    Vector3 velocity = (moveHorizontal + moveVertical).normalized * speed;

    motor.Move(velocity);

    float yRot = Input.GetAxisRaw("Mouse X");

    Vector3 rotation = new Vector3(0f, yRot, 0f) * lookSensitivity;

    motor.Rotate(rotation);

    float xRot = Input.GetAxisRaw("Mouse Y");

    float _cameraRotationX = xRot * lookSensitivity;

    motor.RotateCamera(_cameraRotationX);

  }

}
