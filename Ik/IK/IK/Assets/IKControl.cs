using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ik_type
{
    hand,
    chest,
    foot,
    footrot
}

public class IKControl : MonoBehaviour
{
    public Vector3 correctionOffset;

    public ik_type type;

    protected Animator animator;
    public GameObject Target;

    CharacterController cc;
    public GameObject LeftIkObj , RightIkObj;

    RaycastHit hit;

    void Start()
    {
        animator = GetComponent<Animator>();
        cc = GetComponent<CharacterController>();
    }

    //a callback for calculating IK
    void OnAnimatorIK()
    {
        if (animator)
        {
            switch (type)
            {
                case ik_type.chest:
                    animator.SetLookAtWeight(1,1,1,1,1);
                    animator.SetLookAtPosition(Target.transform.position); // 허리를 이용해서 타겟을바라보게한다.
                    break;
                case ik_type.hand:
                    animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
                    animator.SetIKPosition(AvatarIKGoal.RightHand, Target.transform.position);

                    animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
                    animator.SetIKPosition(AvatarIKGoal.LeftHand, Target.transform.position);
                    break;
                case ik_type.foot:
                    cc.Move(Vector3.down * Time.deltaTime);
                    bool checkL = Physics.Raycast(LeftIkObj.transform.position, LeftIkObj.transform.up * -1, out hit, 1);
                    // 왼쪽발 ik
                    if (checkL)
                    {
                        animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 1);
                        animator.SetIKPosition(AvatarIKGoal.LeftFoot, hit.point + correctionOffset);
                    }
                    bool checkR = Physics.Raycast(RightIkObj.transform.position, RightIkObj.transform.up * -1, out hit, 1);
                    //오른발 ik
                    if (checkR)
                    {
                        animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, 1);
                        animator.SetIKPosition(AvatarIKGoal.RightFoot, hit.point + correctionOffset);
                    }
                    break;
                case ik_type.footrot:
                    cc.Move(Vector3.down * Time.deltaTime);
                    bool ischeckL = Physics.Raycast(LeftIkObj.transform.position, LeftIkObj.transform.up * -1, out hit, 1);
                    // 왼쪽발 ik
                    if (ischeckL)
                    {
                        animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 1);
                        animator.SetIKPosition(AvatarIKGoal.LeftFoot, hit.point + correctionOffset);
                        animator.SetIKRotationWeight(AvatarIKGoal.LeftFoot, 1);
                        animator.SetIKRotation(AvatarIKGoal.LeftFoot, Quaternion.Euler(hit.transform.eulerAngles));
                    }
                    bool ischeckR = Physics.Raycast(RightIkObj.transform.position, RightIkObj.transform.up * -1, out hit, 0.2f);
                    //오른발 ik
                    if (ischeckR)
                    {
                        animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, 1);
                        animator.SetIKPosition(AvatarIKGoal.RightFoot, hit.point + correctionOffset);
                        animator.SetIKRotationWeight(AvatarIKGoal.RightFoot, 1);
                        animator.SetIKRotation(AvatarIKGoal.RightFoot, Quaternion.Euler(hit.transform.eulerAngles));
                    }
                    break;
            }
        }
    }
}
/*
animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
animator.SetIKPosition(AvatarIKGoal.RightHand, target);

animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
animator.SetIKPosition(AvatarIKGoal.LeftHand, leftHandObj.position);

if(animator.GetFloat("ShootingAngle") <= -0.4f) // L
{
    animator.SetLookAtWeight(0.2f, 0.8f, 0.2f,0.3f,0.5f); // 머리 , 허리
    animator.SetLookAtPosition(target - offset);
}
else if(animator.GetFloat("ShootingAngle") >= 0.4f) // R
{
    animator.SetLookAtWeight(0.2f, 0.8f, 0.2f, 0.3f, 0.5f); // 머리 , 허리
    animator.SetLookAtPosition(target + offset1);
}
else // F
{

    animator.SetLookAtWeight(0.2f, 0.8f, 0.2f, 0.3f, 0.5f); // 머리 , 허리
    animator.SetLookAtPosition(target + offset2);
}
*/

//animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1); // 팔 다리
//animator.SetIKPosition(AvatarIKGoal.RightHand, target);
//animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 0.5f); // 팔 다리
//animator.SetIKPosition(AvatarIKGoal.LeftHand, leftHandObj.position);

