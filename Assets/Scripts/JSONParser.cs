using UnityEngine;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class JSONParser : MonoBehaviour
{
    
	public TextAsset attackJson;
	public TextAsset attackVariationJson;


	//Parse an array of attack names and return it as a list of attacks
	public List<Attack> ParseList(string[] attackInput){

		List<Attack> attackList = new List<Attack>();


		Attack newAttack = new Attack();


		JObject jsonObject = JObject.Parse(attackJson.text);  

		for (int i = 0; i < attackInput.Length; i++){
		//foreach(string input in attackInput){

			string input = attackInput[i];
			if (null != jsonObject[input]){
				newAttack = jsonObject[input].ToObject<Attack>();

				if (input == "indicator"){ //Add information for indicator
					newAttack.SetVariation(attackInput[i+1]); //variation is first attack, which should be the index after the indicator(index 0)
					newAttack.SetAttackType(GetAttackType(attackInput[i+1])); //attack type is inferred from the variation
				}


			}



			attackList.Add(newAttack);

		}
		return attackList;

	} 

	//Given an attack variation, select an attack variation and return it as an array of attack names
	public string[] ParseAttackVariation(string attack){

		List<string> attackList = new List<string>();

		JObject jsonObject = JObject.Parse(attackVariationJson.text);  

		

		if (null != jsonObject[attack]){
			List<string[]> attackVariations = jsonObject[attack].ToObject<List<string[]>>();
			
			int index = Random.Range(0, attackVariations.Count);

			return attackVariations[index]; 

		}

		
		return null;
	}

	private string GetAttackType(string attackName){

		if (attackName == "cardinal" || attackName == "intercardinal"){
			return "cross";
		} else if (attackName == "left" || attackName == "right"){
			return "directional";
		}

		return "noIndicator";
	}

}

