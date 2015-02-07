using UnityEngine;
using System.Collections;

public class AvatarIK : MonoBehaviour {

	protected Animator animator;
	
	public bool ikActive = true;
	public Transform rightFootTarget = null;
	public Transform leftFootTarget = null;

	public Transform leftHandTarget = null;
	public Transform rightHandTarget = null;

	void Start () 
	{
		animator = GetComponent<Animator>();
	}
	
	//a callback for calculating IK
	void OnAnimatorIK()
	{
		if(animator) {
			//if the IK is active, set the position and rotation directly to the goal. 
			if(ikActive) {
				//weight = 1.0 for the right hand means position and rotation will be at the IK goal (the place the character wants to grab)
				animator.SetIKPositionWeight(AvatarIKGoal.RightFoot,1.0f);
				animator.SetIKRotationWeight(AvatarIKGoal.RightFoot,1.0f);
				animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot,1.0f);
				animator.SetIKRotationWeight(AvatarIKGoal.LeftFoot,1.0f);

				if(leftHandTarget != null)
				{
					animator.SetIKPositionWeight(AvatarIKGoal.LeftHand,1.0f);
					animator.SetIKRotationWeight(AvatarIKGoal.LeftHand,1.0f);
					animator.SetIKPosition(AvatarIKGoal.LeftHand,leftHandTarget.position);
					animator.SetIKRotation(AvatarIKGoal.LeftHand,leftHandTarget.rotation);
				}
				if(rightHandTarget != null)
				{
					animator.SetIKPositionWeight(AvatarIKGoal.RightHand,1.0f);
					animator.SetIKRotationWeight(AvatarIKGoal.RightHand,1.0f);
					animator.SetIKPosition(AvatarIKGoal.RightHand,rightHandTarget.position);
					animator.SetIKRotation(AvatarIKGoal.RightHand,rightHandTarget.rotation);
				}


				//set the position and the rotation of the right hand where the external object is
				if(rightFootTarget != null) {
					animator.SetIKPosition(AvatarIKGoal.RightFoot,rightFootTarget.position);
					animator.SetIKRotation(AvatarIKGoal.RightFoot,rightFootTarget.rotation);

				}                   
				if(leftFootTarget != null) {
					animator.SetIKPosition(AvatarIKGoal.LeftFoot,leftFootTarget.position);
					animator.SetIKRotation(AvatarIKGoal.LeftFoot,leftFootTarget.rotation);

				}    
			}
			
			//if the IK is not active, set the position and rotation of the hand back to the original position
			else {          
				animator.SetIKPositionWeight(AvatarIKGoal.RightFoot,0);
				animator.SetIKRotationWeight(AvatarIKGoal.RightFoot,0);        
				animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot,0);
				animator.SetIKRotationWeight(AvatarIKGoal.LeftFoot,0); 

			}
		}
	}    
}
