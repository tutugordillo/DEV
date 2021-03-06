using UnityEngine;
using System.Collections;


using Pada1.BBCore; // Code attributes
using Pada1.BBCore.Tasks; // TaskStatus 
using Pada1.BBCore.Framework; // BasePrimitiveAction

[Action("MyActions/SetPosition")] 
[Help("Given a GameObject and a position, the gameobject is located in this position.")] 

public class SetPosition : BasePrimitiveAction {

	[InParam("position")]
	public Vector3 position;

	[InParam("GameObject")]
	public GameObject obj;

	public override TaskStatus OnUpdate(){
		this.obj.transform.position = this.position;
		return TaskStatus.COMPLETED;
	}
}
