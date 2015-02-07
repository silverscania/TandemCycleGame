using UnityEngine;
using System.Collections;

public class AvatarIK : MonoBehaviour {

	protected Animator animator;
	
	public bool ikActive = false;
	public Transform rightHandObj = null;
	
	void Start () 
	{
		animator = GetComponent<Animator>();
	}
	
	//a callback for calculating IK
	void OnAnimatorIK()
	{
		if(animator) {
			Debug.Log ("Animator");
			//if the IK is active, set the position and rotation directly to the goal. 
			if(ikActive) {
				Debug.Log("ik active");
				//weight = 1.0 for the right hand means position and rotation will be at the IK goal (the place the character wants to grab)
				animator.SetIKPositionWeight(AvatarIKGoal.RightFoot,1.0f);
				animator.SetIKRotationWeight(AvatarIKGoal.RightFoot,1.0f);
				
				//set the position and the rotation of the right hand where the external object is
				if(rightHandObj != null) {
					animator.SetIKPosition(AvatarIKGoal.RightFoot,rightHandObj.position);
					animator.SetIKRotation(AvatarIKGoal.RightFoot,rightHandObj.rotation);
				}                   
				
			}
			
			//if the IK is not active, set the position and rotation of the hand back to the original position
			else {          
				animator.SetIKPositionWeight(AvatarIKGoal.RightFoot,0);
				animator.SetIKRotationWeight(AvatarIKGoal.RightFoot,0);             
			}
		}
	}    
}
