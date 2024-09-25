using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    //플레이어의 움직임 속도를  설정하는 변수
    [Header("Player Movement")]
    public float moveSpeed = 5.0f;
    public float jumpForce = 5.0f;
    // Start is called before the first frame update

    //카메라 설정변수
    [Header("Camera Settings")]
    public Camera firstPersonCamera;
    public Camera thirdPersonCamera;
    public float mouseSenesitivity = 2.0f;

    public float radius = 5.0f;
    public float minRadius = 1.0f;
    public float maxRadius = 10.0f;

    public float yMinLimit = -90;
    public float yMaxLimit = 90;

    public float theta = 0.0f;
    private float phi = 0.0f;
    private float tragetVericalRotation = 0;
    private float verticalRoatationSpeed = 240f;

    //내부 변수들
    private bool isFirstPerson = true;
    private bool isGrounded;
    private Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        Cursor.lockState = CursorLockMode.Locked;
        SetupCameras();
    }

    // Update is called once per frame
    void Update()
    {
        HandleJump();
        HandleMovement();
        HandleRotation();
        HandleCameraToggle();
    }

    //활성화할 카메라를 설정하는 함수
    void SetActiveCamera()
    {
        firstPersonCamera.gameObject.SetActive(isFirstPerson);
        thirdPersonCamera.gameObject.SetActive(!isFirstPerson);
    }

    //카메라 초기 위치 및 회전을 설정하는 함수
    void SetupCameras()
    {
        firstPersonCamera.transform.localPosition = new Vector3(0.0f, 0.6f, 0.0f);
        firstPersonCamera.transform.localRotation = Quaternion.identity;
    }

    //카메라 및 캐릭터 회전 처리하는 함수
    void HandleRotation()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSenesitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSenesitivity;

        //수평 회전
        theta += mouseX;
        theta = Mathf.Repeat(theta, 360.0f);

        //수직 회전 처리
        tragetVericalRotation -= mouseY;
        tragetVericalRotation = Mathf.Clamp(tragetVericalRotation, yMinLimit, yMaxLimit); //수직 회전 제한
        phi = Mathf.MoveTowards(phi, tragetVericalRotation, verticalRoatationSpeed * Time.deltaTime);

        //플레이거 회전
        transform.rotation = Quaternion.Euler(0.0f, theta, 0.0f);

        if(isFirstPerson)
        {
            firstPersonCamera.transform.localRotation = Quaternion.Euler(phi, 0.0f, 0.0f);
        }
        else
        {
            float x = radius * Mathf.Sin(Mathf.Deg2Rad * phi) * Mathf.Cos(Mathf.Deg2Rad * theta);
            float y = radius * Mathf.Cos(Mathf.Deg2Rad * phi);
            float z = radius * Mathf.Sin(Mathf.Deg2Rad * phi) * Mathf.Sin(Mathf.Deg2Rad * theta);

            thirdPersonCamera.transform.position = transform.position + new Vector3(x, y, z);
            thirdPersonCamera.transform.LookAt(transform);

            radius = Mathf.Clamp(radius - Input.GetAxis("Mouse ScrollWheel") * 5,minRadius,maxRadius);

        }

        firstPersonCamera.transform.localRotation = Quaternion.Euler(phi, 0.0f, 0.0f);
    }

    void HandleCameraToggle()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            isFirstPerson = !isFirstPerson;
            SetActiveCamera();
        }
    }

    //플레이어 점프를 처리하는 함수
    void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    //플레이어의 이동을 처리하는 함수
    void HandleMovement()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        if (!isFirstPerson)
        {
            Vector3 cameraForward = thirdPersonCamera.transform.forward;
            cameraForward.y = 0.0f;
            cameraForward.Normalize();

            Vector3 cameraRight = thirdPersonCamera.transform.right;
            cameraRight.y = 0.0f;
            cameraRight.Normalize();

            Vector3 movement = cameraRight * moveHorizontal + cameraForward * moveVertical;
            rb.MovePosition(rb.position + movement * moveSpeed*Time .deltaTime);
        }
        else
        {

            //캐릭터 기준으로 이동
            Vector3 movement = transform.right * moveHorizontal + transform.forward * moveVertical;
            rb.MovePosition(rb.position + movement * moveSpeed * Time.deltaTime);
        }
    }

    //플레이어가 땅에 닿아 있는지 감지
    private void OnCollisionStay(Collision collision)
    {
        isGrounded = true;
    }
}
