using UnityEngine;
using DG.Tweening;

public class RideControl : MonoBehaviour
{
    public Camera centerCamera;
    public OVRCameraRig cameraRig;
    public float moveSpeed ;
    public float rollSpeed = 30f;
    public float stabilizeSpeed = 3f;
    public float MoveUpDown = 5f;
    public float PlusMove = 0f;
    public Transform2 Ts2;

    private Rigidbody rigid;
    private bool keyboardMode = true;
    private Vector2 debugModeCamAngle = Vector2.zero;
    private Vector3 debugModeMousePos = Vector3.zero;

    private bool hoge = true;

    void Start()
    {
        rigid = GetComponent<Rigidbody>();

        // HMDのトラッキングが有効化した
        OVRManager.TrackingAcquired += () =>
        {
            keyboardMode = false;
            Vector3 camPos = centerCamera.transform.position;
            cameraRig.transform.position = new Vector3(-camPos.x, -camPos.y, -camPos.z);
        };
    }

    void Update()
    {
        if (Ts2.GrabItem == null)
        {
            if (OVRInput.GetUp(OVRInput.Button.PrimaryThumbstickUp, OVRInput.Controller.LTouch))
            {
                DOTween.To(() => 0.4f, (x) => PlusMove = x, 0f, 2).SetEase(Ease.OutQuad);
            }

            if (OVRInput.GetUp(OVRInput.Button.PrimaryThumbstickDown, OVRInput.Controller.LTouch))
            {
                DOTween.To(() => -0.2f, (x) => PlusMove = x, 0f, 2).SetEase(Ease.OutQuad);
            }

            float yaw = OVRInput.Get(OVRInput.RawAxis2D.RThumbstick).x;
            float yaw2 = OVRInput.Get(OVRInput.RawAxis2D.LThumbstick).x;
            float pitch = OVRInput.Get(OVRInput.RawAxis2D.RThumbstick).y;
            float throttle = OVRInput.Get(OVRInput.RawAxis2D.LThumbstick).y + PlusMove;
            float ThisYaw = this.gameObject.transform.localEulerAngles.y;

            // キーボード操作
            if (keyboardMode == true)
            {
                yaw = Input.GetAxis("Horizontal");
                pitch = Input.GetAxis("Vertical");
                throttle = Input.GetAxis("Throttle");
                if (Input.GetMouseButtonDown(0))
                {
                    debugModeCamAngle = cameraRig.transform.localEulerAngles;
                    debugModeMousePos = Input.mousePosition;
                }
                else if (Input.GetMouseButton(0))
                {
                    debugModeCamAngle.y += (Input.mousePosition.x - debugModeMousePos.x);
                    debugModeCamAngle.x += (debugModeMousePos.y - Input.mousePosition.y);
                    debugModeMousePos = Input.mousePosition;
                    cameraRig.transform.localEulerAngles = debugModeCamAngle;
                }
            }

            Vector3 euler = transform.rotation.eulerAngles;
            euler.x += -pitch * rollSpeed * Time.deltaTime;
            euler.y += (yaw + yaw2) * rollSpeed * Time.deltaTime;

            // 水平方向の安定化
            if (throttle == 0f && !OVRInput.Get(OVRInput.RawButton.RHandTrigger))
            {
                euler.x = Mathf.MoveTowardsAngle(euler.x, 0f, stabilizeSpeed * Time.deltaTime);
                euler.z = Mathf.MoveTowardsAngle(euler.z, 0f, stabilizeSpeed * Time.deltaTime);
            }

            //スピードブースト
            if (OVRInput.Get(OVRInput.RawButton.RHandTrigger))
            {
                moveSpeed = 15f;
            }

            else
            {
                moveSpeed = 8f;
            }

            // 回転と移動を反映
            transform.rotation = Quaternion.Euler(euler);
            transform.Translate(Vector3.forward * throttle * moveSpeed * Time.deltaTime);

            if (OVRInput.Get(OVRInput.RawButton.LIndexTrigger))
            {
                this.gameObject.transform.Translate(Vector3.up * MoveUpDown * Time.deltaTime);
            }

            if (OVRInput.Get(OVRInput.RawButton.LHandTrigger))
            {
                this.gameObject.transform.Translate(Vector3.down * MoveUpDown * Time.deltaTime);
            }
        }
    }
}
