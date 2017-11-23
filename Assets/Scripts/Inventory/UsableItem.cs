using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsableItem : MonoBehaviour
{
	[SerializeField] protected float animationSpeed = 100.0f;
    public string name;

    public virtual void StartUsingItem()
	{
		Debug.Log ("BaseStartUsing");
	}

	public virtual void UsingItem()
	{

	}

	public virtual void StopUsingItem()
	{

	}

}
