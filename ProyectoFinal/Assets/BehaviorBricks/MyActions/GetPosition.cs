using UnityEngine;
using System.Collections;


using Pada1.BBCore; // Code attributes
using Pada1.BBCore.Tasks; // TaskStatus 
using Pada1.BBCore.Framework; // BasePrimitiveAction

[Action("MyActions/GetPosition")] 
[Help("Given a GameObject it returns its position.")] 

public class GetPosition : BasePrimitiveAction {

	[OutParam("position")]
	public Vector3 position;

	[InParam("GameObject")]
	public GameObject obj;

	public override void OnStart()
	{
		position = this.obj.transform.position;
	}

	public override TaskStatus OnUpdate(){
		return TaskStatus.COMPLETED;
	}
}