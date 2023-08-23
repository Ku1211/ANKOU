using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformMover : MonoBehaviour
{
    [SerializeField]
    private Transform RightHandAnchor;      // �E�R���g���[��
    [SerializeField]
    private LineRenderer LaserPointerForGrab;     // �͂ޗp���[�U�[�|�C���^
    [SerializeField]
    private float MaxDistance = 1f;         // ���[�U�[�̍ő勗��
    public Transform grabItem;
    private GrabitemTP grabitemTP;
    public GameObject armhand;
    public GameObject armcollider;
    public bool triggerOrder;
    public Example example;
    public Transform pointer;

    // Start is called before the first frame update
    private void Start()
    {
        triggerOrder = false;
        LaserPointerForGrab.enabled = true;
        Ray ray = new Ray(RightHandAnchor.position, RightHandAnchor.forward);
        LaserPointerForGrab.SetPosition(0, ray.origin);
        LaserPointerForGrab.SetPosition(1, ray.origin);
    }

    // Update is called once per frame
    private void OnDrawGizmos()
    {
        // �E�n���h�g���K�[�������Ă��鎞
        if (OVRInput.Get(OVRInput.RawButton.RHandTrigger))
        {
            // �R���g���[������Ray���o�����R���g���[���[�̐�@�̂��O������o��
            Ray ray = new Ray(RightHandAnchor.position, RightHandAnchor.forward);

            // ���[�U�[�̋N�_
            LaserPointerForGrab.SetPosition(0, ray.origin);

            //RaycastHit[] hits;
            RaycastHit hit;
            var isHit = Physics.BoxCast(RightHandAnchor.transform.position, Vector3.one * 0.15f, RightHandAnchor.transform.forward, out hit, RightHandAnchor.transform.rotation,50);
            if (isHit)
            {
                Gizmos.DrawRay(RightHandAnchor.transform.position, RightHandAnchor.transform.forward * hit.distance);
                Gizmos.DrawWireCube(RightHandAnchor.transform.position + RightHandAnchor.transform.forward * hit.distance, Vector3.one * 0.09f);
                example.targetPosition = hit.collider.transform.position;
            }
            else
            {
                Gizmos.DrawRay(RightHandAnchor.transform.position, RightHandAnchor.transform.forward * MaxDistance);
                example.targetPosition = RightHandAnchor.transform.position +( RightHandAnchor.transform.forward * MaxDistance);
            }

            

            if (isHit)
            {
               
                //Debug.LogError(hit.collider +" "+ hit.collider.CompareTag("Grab")+" "+grabItem);
                if (hit.collider.CompareTag("Grab") && (grabItem == null) && (OVRInput.Get(OVRInput.RawButton.RIndexTrigger)) && (triggerOrder == false))
                {
                    // �I�u�W�F�N�g��͂�
                    grabItem = hit.collider.transform;
                    grabitemTP = hit.collider.GetComponent<GrabitemTP>();
                    grabitemTP.GetComponent<GrabitemTP>().GrabTeleport();
                    grabItem.parent = armhand.transform;
                    example.m_speed = 0.5f;
                    //Debug.LogError("aaa");
                }
            }
                /*hits = Physics.RaycastAll(RightHandAnchor.transform.position, RightHandAnchor.transform.forward, MaxDistance);

                // Ray����������
                if (hits.Length > 0)
                {
                    foreach (var hit in hits)
                    {
                        // Ray�������������܂Ń��[�U�[���o��
                        Vector3 hitPoint = (grabItem == null) ? hit.point : ray.origin;     // �͂�ł��鎞�͒���0
                        LaserPointerForGrab.SetPosition(1, hitPoint);

                        if (armTP == null && !(OVRInput.Get(OVRInput.RawButton.RIndexTrigger)))
                        {
                            armTP = hit.collider.transform;
                            example.m_target = armTP;
                        }

                        if (hit.collider.CompareTag("Grab") &&(grabItem == null) &&(OVRInput.Get(OVRInput.RawButton.RIndexTrigger))&&(triggerOrder == false))
                            {
                            // �I�u�W�F�N�g��͂�
                                grabItem = hit.collider.transform;
                                example.m_target = pointer;
                                armTP = null;
                                grabitemTP = hit.collider.GetComponent<GrabitemTP>();
                                grabitemTP.GetComponent<GrabitemTP>().GrabTeleport();
                                grabItem.parent = armhand.transform;
                                //grabItem.GetComponent<Rigidbody>().useGravity = false;
                                //grabItem.GetComponent<Rigidbody>().isKinematic = true;
                                break;
                            }

                    }-
                }*/
            }

        triggerOrder = false;

        if ((OVRInput.Get(OVRInput.RawButton.RIndexTrigger)))
        {
            triggerOrder = true;
        }

        // �E�n���h�g���K�[�𗣂�����
        if ((OVRInput.GetUp(OVRInput.RawButton.RIndexTrigger))||(OVRInput.GetUp(OVRInput.RawButton.RHandTrigger)))
        {

            if((OVRInput.Get(OVRInput.RawButton.RHandTrigger)))
            {
                if (grabItem != null)
                {
                    // �I�u�W�F�N�g�𗣂�
                    grabItem.parent = null;
                    //grabItem.GetComponent<Rigidbody>().useGravity = true;
                    //grabItem.GetComponent<Rigidbody>().isKinematic = true;
                    grabItem = null;
                    example.m_speed = 3;
                    //Debug.LogError("bbb");
                }
            }

            // Ray������
            Ray ray = new Ray(RightHandAnchor.position, RightHandAnchor.forward);
            LaserPointerForGrab.SetPosition(0, ray.origin);
            LaserPointerForGrab.SetPosition(1, ray.origin);
        }
    }
}

