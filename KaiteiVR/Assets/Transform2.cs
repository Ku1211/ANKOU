using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Transform2 : MonoBehaviour
{
    [SerializeField]
    private Transform RightHandAnchor;      // 右コントローラ
    [SerializeField]
    private LineRenderer LaserPointerForGrab;     // 掴む用レーザーポインタ
    [SerializeField]
    private float MaxDistance = 1f;         // レーザーの最大距離
    public Transform GrabItem;
    public GameObject Arm;
    public GameObject YubiT;
    public Transform Home;
    public bool armAnim;
    public bool isHit;
    public RaycastHit hit;
    public int GetBattery;
    public bool Vibe = false;
    public float ArmHit;
    public float ArmRange;

    // Start is called before the first frame update
    private void Start()
    {
        GetBattery = 0;
        armAnim = false;
        LaserPointerForGrab.enabled = true;
        Ray ray = new Ray(RightHandAnchor.position, RightHandAnchor.forward);
        LaserPointerForGrab.SetPosition(0, ray.origin);
        LaserPointerForGrab.SetPosition(1, ray.origin);
    }

    // Update is called once per frame

    private void Update()
    {


        // 右ハンドトリガーを押している時

        // コントローラからRayを出す→コントローラーの先機体より外側から出す
        Ray ray = new Ray(RightHandAnchor.position, RightHandAnchor.forward);

        // レーザーの起点
        LaserPointerForGrab.SetPosition(0, ray.origin);

        //RaycastHit[] hits;
        
        isHit = Physics.BoxCast(RightHandAnchor.transform.position, Vector3.one * ArmHit, RightHandAnchor.transform.forward, out hit, RightHandAnchor.transform.rotation, ArmRange);

        if (isHit)
        {
            if ((Vibe == false) && hit.collider.CompareTag("Grab"))
            {
                Vibe = true;

                Debug.Log(hit.distance);

                OVRInput.SetControllerVibration(0.1f, 0.1f, OVRInput.Controller.RTouch);

                DOVirtual.DelayedCall(0.1f, () => { OVRInput.SetControllerVibration(0, 0); });
            }

            if (OVRInput.Get(OVRInput.RawButton.RIndexTrigger) && hit.collider.CompareTag("Grab") && (GrabItem == null))
            {
                GrabItem = hit.collider.transform;

                Vector3 YubiY = GrabItem.position;

                YubiY.y += 1.0f;

                DOTween.Sequence()
                    .Append(Arm.transform.DOMove(YubiY, 1.0f).SetEase(Ease.OutQuad)/*.OnComplete(() => { GrabItem.parent = Arm.transform; })*/)
                    .AppendCallback(() => { GrabItem.parent = Arm.transform; })
                    .Append(YubiT.transform.DOMove(new Vector3(0, -0.6f, 0), 1f).SetRelative(true).SetEase(Ease.OutQuad)/*.OnComplete(() => { armAnim = true; })*/)
                    .Append(Arm.transform.DOMove(Home.position, 1.0f).SetEase(Ease.OutQuad)/*.OnComplete(() => { GrabItem.gameObject.SetActive(false); GrabItem = null; })*/)//モニターにアイテムゲットの画面を写す
                    .AppendCallback(() => { EnergyCount(); })
                    .Append(YubiT.transform.DOMove(new Vector3(0, 0.6f, 0), 1f).SetRelative(true).SetEase(Ease.OutQuad).SetDelay(1f)/*.OnComplete(() => { armAnim = false; Debug.LogError("tojimasu"); })*/)
                    .AppendCallback(() => { GrabItem = null; })
                    ;

                Vibe = false;
            }
        }

        else
        {
            Vibe = false;
        }

        Debug.Log(GetBattery);

    }

    private void EnergyCount()
    {
        GrabItem.gameObject.SetActive(false);
        GetBattery++;
        //Debug.LogError("Transform2 : " + GetBattery) ;
    }
    private void OnDrawGizmos()
    {

        if (isHit)
        {
            //Gizmos.DrawRay(RightHandAnchor.transform.position, RightHandAnchor.transform.forward * hit.distance);
            Gizmos.DrawWireCube(RightHandAnchor.transform.position + RightHandAnchor.transform.forward * hit.distance, Vector3.one * 0.09f);
            //Debug.LogError("raydeteru");
        }
        else
        {
            //Gizmos.DrawRay(RightHandAnchor.transform.position, RightHandAnchor.transform.forward * MaxDistance);
        }
    }
}

    


