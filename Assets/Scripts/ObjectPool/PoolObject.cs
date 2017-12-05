using UnityEngine;
using System.Collections;

public class PoolObject : MonoBehaviour, iPoolableObject 	//used this Class if the Script inheriting from it does not need to inherit from anything classes other than MonoBehaviour 
{
	protected PoolManager poolManager;
	[SerializeField] protected ObjectPool myObjectPool = null;

	public ObjectPool objectPool
	{
		get { return myObjectPool; }
		set { myObjectPool = value; }
	}

	protected void Start ()
	{
		poolManager = PoolManager.instance; 
	}

	public virtual void Initialize ()
	{

	}

	public virtual void OnExitPool ()
	{

	}

	public virtual void OnEnterPool ()
	{

	}

	protected void ReturnToPool()
	{
		objectPool.ReturnObject (gameObject);
	}
}
