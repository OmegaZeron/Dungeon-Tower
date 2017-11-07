using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsableItem : MonoBehaviour
{
	[SerializeField] protected float animationSpeed = 100.0f;

	protected void Start () {
		
	}

	public virtual void StartUsingItem()
	{

	}

	public virtual void UsingItem()
	{

	}

	public virtual void StopUsingItem()
	{

	}

}
