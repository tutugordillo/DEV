using UnityEngine;
using System.Collections;

using Pada1.BBCore;           // Code attributes
using Pada1.BBCore.Tasks;     // TaskStatus
using BBUnity.Actions;        // GOAction


[Action("MyActions/GetOwnPosition")]
[Help("Return the position of the object that is executing this actions")]
public class GetOwnPosition : GOAction {

	[OutParam("position")]
	public Vector3 pos;


	// Main class method, invoked by the execution engine.
	public override TaskStatus OnUpdate()
	{
		pos = gameObject.transform.position;
		return TaskStatus.COMPLETED;
		} 
}
