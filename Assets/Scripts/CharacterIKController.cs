using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterIKController : MonoBehaviour
{
    private Animator animator;
    private List<Vector3> position = new List<Vector3>();
    public csvReader csvreader;
    public GameObject csvReaderGameObject;
    private int frameCount = 0;
    //public GameObject refCube3;

    void Start()
    {
        Debug.Log("Start called");
        animator = GetComponent<Animator>();
        csvreader = csvReaderGameObject.GetComponent<csvReader>();

    }

    void OnAnimatorIK()
    {
        if (csvreader.GetMediapipeOrXsense())
        {
            if (csvreader != null && csvreader.runExercice)
            {
                frameCount = 0;
                position = csvreader.GetPositions();
                Vector3 side1;
                Vector3 side2;

                // Set the left hand IK
                Vector3 pointLH = ((position[19] + position[17]) / 2.0f);
                Vector3 fdDirectionLH = (pointLH - position[15]).normalized;
                side1 = position[19] - position[15];
                side2 = position[17] - position[15];
                Vector3 upDirectionLH = Vector3.Cross(side2, side1).normalized;

                Quaternion rotationLH = Quaternion.LookRotation(fdDirectionLH, upDirectionLH);

                animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 0.5f);
                animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 0.5f);
                animator.SetIKPosition(AvatarIKGoal.LeftHand, position[15]);
                animator.SetIKRotation(AvatarIKGoal.LeftHand, rotationLH);


                // Set the right hand IK
                Vector3 pointRH = ((position[20] + position[18]) / 2.0f);
                Vector3 fdDirectionRH = (pointRH - position[16]).normalized;
                side1 = position[20] - position[16];
                side2 = position[18] - position[16];
                Vector3 upDirectionRH = Vector3.Cross(side1, side2).normalized;

                Quaternion rotationRH = Quaternion.LookRotation(fdDirectionRH, upDirectionRH);

                animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 0.5f);
                animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 0.5f);
                animator.SetIKPosition(AvatarIKGoal.RightHand, position[16]);
                animator.SetIKRotation(AvatarIKGoal.RightHand, rotationRH);

                // Set the left foot IK
                Vector3 fdDirectionLF = (position[31] - position[29]).normalized;
                Vector3 upDirectionLF = (position[25] - position[27]).normalized;
                Quaternion rotationLF = Quaternion.LookRotation(fdDirectionLF, upDirectionLF);

                animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 0.5f);
                animator.SetIKRotationWeight(AvatarIKGoal.LeftFoot, 0.5f);
                animator.SetIKPosition(AvatarIKGoal.LeftFoot, position[27]);
                animator.SetIKRotation(AvatarIKGoal.LeftFoot, rotationLF);

                // Set the right foot IK
                Vector3 fdDirectionRF = (position[32] - position[30]).normalized;
                Vector3 upDirectionRF = (position[26] - position[28]).normalized;
                Quaternion rotationRF = Quaternion.LookRotation(fdDirectionRF, upDirectionRF);

                animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, 0.5f);
                animator.SetIKRotationWeight(AvatarIKGoal.RightFoot, 0.5f);
                animator.SetIKPosition(AvatarIKGoal.RightFoot, position[28]);
                animator.SetIKRotation(AvatarIKGoal.RightFoot, rotationRF);

                // Set the head IK
                animator.SetLookAtWeight(1.0f);
                animator.SetLookAtPosition(position[0]);

                // Set the body  
                side1 = position[11] - position[12];
                side2 = position[23] - position[12];

                Vector3 upDirection = Vector3.Cross(side1, side2).normalized;
                Vector3 bustPosition = Vector3.Lerp((position[23] + position[24]) / 2.0f, (position[11] + position[12]) / 2.0f, 0.5f);
                Vector3 upFromCenterPoint = bustPosition + upDirection;
                Vector3 upDirectionFromCenter = (upFromCenterPoint - bustPosition).normalized;
                Vector3 CenterToHead = (((position[11] + position[12]) / 2.0f) - bustPosition).normalized;

                Quaternion desiredRotation = Quaternion.LookRotation(upDirectionFromCenter, CenterToHead);
                Vector3 spineOffset = animator.GetBoneTransform(HumanBodyBones.Spine).position - animator.transform.position;

                animator.transform.position = bustPosition - spineOffset;
                animator.transform.rotation = desiredRotation;
            }
        }
        else
        {
            if (csvreader != null && csvreader.runExercice)
            {
                position = csvreader.GetPositions();
                Vector3 side1;
                Vector3 side2;

                // left hand
                Vector3 pointLH = position[14];
                Vector3 fdDirectionLH = (position[13] - position[14]).normalized;
                side1 = position[13] - pointLH;
                side2 = position[12] - position[13];
                Vector3 upDirectionLH = Vector3.Cross(side2, side1).normalized;

                Quaternion rotationLH = Quaternion.LookRotation(fdDirectionLH, upDirectionLH);

                animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 0.5f);
                animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 0.5f);
                animator.SetIKPosition(AvatarIKGoal.LeftHand, position[14]);
                animator.SetIKRotation(AvatarIKGoal.LeftHand, rotationLH);

                // right hand
                /*Vector3 pointRH = CalculateOppositePoint(position[19], position[10]);
                Vector3 fdDirectionRH = (pointLH - position[10]).normalized;
                side1 = pointLH - position[10];
                side2 = position[9] - position[10];
                Vector3 upDirectionRH = Vector3.Cross(side2, side1).normalized;

                Quaternion rotationRH = Quaternion.LookRotation(fdDirectionLH, upDirectionLH);*/

                animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 0.5f);
                animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 0.5f);
                animator.SetIKPosition(AvatarIKGoal.RightHand, position[10]);
                //animator.SetIKRotation(AvatarIKGoal.RightHand, rotationRH);

                // left foot
                Vector3 fdDirectionLF = (position[22] - position[21]).normalized;
                Vector3 upDirectionLF = (position[20] - position[21]).normalized;
                Quaternion rotationLF = Quaternion.LookRotation(fdDirectionLF, upDirectionLF);

                animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 0.5f);
                animator.SetIKRotationWeight(AvatarIKGoal.LeftFoot, 0.5f);
                animator.SetIKPosition(AvatarIKGoal.LeftFoot, position[21]);
                animator.SetIKRotation(AvatarIKGoal.LeftFoot, rotationLF);

                // right foot
                Vector3 fdDirectionRF = (position[18] - position[17]).normalized;
                Vector3 upDirectionRF = (position[16] - position[17]).normalized;
                Quaternion rotationRF = Quaternion.LookRotation(fdDirectionRF, upDirectionRF);

                animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, 0.5f);
                animator.SetIKRotationWeight(AvatarIKGoal.RightFoot, 0.5f);
                animator.SetIKPosition(AvatarIKGoal.RightFoot, position[17]);
                animator.SetIKRotation(AvatarIKGoal.RightFoot, rotationRF);

                // Set the head
                animator.SetLookAtWeight(0.5f);
                animator.SetLookAtPosition(CalculateOppositePoint(position[5], position[6]));

                // Set the body  
                side1 = position[12] - position[8]; // between the two elbows
                side2 = position[1] - position[5]; // along the spine

                Vector3 upDirection = Vector3.Cross(side1, side2).normalized; 
                Vector3 bustPosition = position[1];
                Vector3 upFromCenterPoint = bustPosition + upDirection;
                Vector3 upDirectionFromCenter = (upFromCenterPoint - bustPosition).normalized;
                Vector3 CenterToHead = (position[6] - bustPosition).normalized;

                Quaternion desiredRotation = Quaternion.LookRotation(upDirectionFromCenter, CenterToHead);
                Vector3 spineOffset = animator.GetBoneTransform(HumanBodyBones.Spine).position - animator.transform.position;

                animator.transform.position = bustPosition - spineOffset;
                animator.transform.rotation = desiredRotation;
            }
        }
    }

    Vector3 CalculateOppositePoint(Vector3 point1, Vector3 point2)
    {
        Vector3 direction = point2 - point1;
        Vector3 opposite = point2 + direction;
        return opposite;
    }
}