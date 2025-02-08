using UnityEngine;
using System.Collections.Generic;
using Newtonsoft.Json;

public class Attack
{
 	[JsonProperty]
	private string name;
	[JsonProperty]
	private List<Vector3> attackVertices;
	[JsonProperty]
	private float damage;
	[JsonProperty]
	private float duration; //How long the attack should be active
	[JsonProperty]
	private float waitDuration; //Time to wait before the nex attack is fired.
	//Note - attacks fire after this duration and the next one is parsed right after the one before it is fired.
	[JsonProperty]
	private string type;
	[JsonProperty]
	private float length;
	[JsonProperty]
	private float width;
	[JsonProperty]
	private Vector3 position;
	[JsonProperty]
	private bool repeat;
	[JsonProperty]
	private bool repeat2;
	[JsonProperty]
	private float angle; 
	
	//indicator info
	[JsonProperty]
	private string variation; 
	[JsonProperty]
	private string attackType; 




	public string Name => name;
	public List<Vector3> AttackVertices => attackVertices;
	public float Damage => damage;
	public float Duration => duration;
	public float WaitDuration => waitDuration;
	public string Type => type;
	public float Length => length;
	public float Width => width;
	public Vector3 Position => position;
	public bool Repeat => repeat;
	public bool Repeat2 => repeat2;
	public float Angle => angle; 
	public string Variation => variation;
	public string AttackType => attackType;

	public void SetVariation(string val){
		variation = val;
	}

	public void SetAttackType(string val){
		attackType = val;
	}

	public List<Vector3> CreateAttackVertices(Vector3 origin){
		
		if (type == "rectangle"){
			
			attackVertices = new List<Vector3>();

			Vector3 modPosition = new Vector3(origin.x + position.x , 0.0f, origin.z + position.z);
			
			Vector3 modOrigin = new Vector3(origin.x, 0.0f, origin.z);

			Vector3 direction = (modPosition - modOrigin).normalized;
			Vector3 endPoint = direction * length;

			Vector3 perpendicular = Vector3.Cross(direction, Vector3.up).normalized;
			Vector3 widthEndPoint = perpendicular * (width/2.0f);


			attackVertices.Add(widthEndPoint);

			attackVertices.Add(widthEndPoint + endPoint);

			attackVertices.Add(-widthEndPoint + endPoint);
			attackVertices.Add(-widthEndPoint);




			return attackVertices;


		} else if (type == "cone"){

			attackVertices = new List<Vector3>();

			Vector3 modPosition = new Vector3(origin.x + position.x , 0.0f, origin.z + position.z);
			Vector3 modOrigin = new Vector3(origin.x, 0.0f, origin.z);

			Vector3 direction = (modPosition - modOrigin).normalized;
			//Vector3 endPoint = direction * length;

			Vector3 left = Quaternion.AngleAxis(-angle/2.0f, Vector3.up) * direction;
			Vector3 right = Quaternion.AngleAxis(angle/2.0f, Vector3.up) * direction;
			
			Vector3 leftVertex = modOrigin + left * length;
			Vector3 rightVertex = modOrigin + right * length;
			//Vector3 widthEndPoint = perpendicular * (width/2.0f);


			attackVertices.Add(modOrigin);

			attackVertices.Add(leftVertex);

			attackVertices.Add(rightVertex);
			//attackVertices.Add(modOrigin - widthEndPoint);
			return attackVertices;


		} else if (type == "cardinals"){

			attackVertices = new List<Vector3>();

			Vector3 modPosition = new Vector3(origin.x + position.x , 0.0f, origin.z + position.z);
			Vector3 modOrigin = new Vector3(origin.x, 0.0f, origin.z);

			Vector3 direction = (modPosition - modOrigin).normalized;
			//Vector3 endPoint = direction * length;

			for (int i = 0; i < 4; i++){
				Vector3 left = Quaternion.AngleAxis(-angle/2.0f + i*90.0f, Vector3.up) * direction;
				Vector3 right = Quaternion.AngleAxis(angle/2.0f + i*90.0f, Vector3.up) * direction;
				
				Vector3 leftVertex =  left * length;
				Vector3 rightVertex = right * length;
				//Vector3 widthEndPoint = perpendicular * (width/2.0f);


				attackVertices.Add( Vector3.zero);

				attackVertices.Add(leftVertex);

				attackVertices.Add(rightVertex);
			}

			
			//attackVertices.Add(modOrigin - widthEndPoint);
			return attackVertices;


		} else if (type == "indicator"){



			return null;
		}

		return null;

	}

}

