using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PoolManager : MonoBehaviour
{
	private static PoolManager poolManager = null;

	[SerializeField] private int totalPoolSize = 100;
	[SerializeField] List<ObjectPool> pools = new List<ObjectPool>();

	//TODO ?? Handle the dynamic creation of ObjectPools, when a PoolableObject Return or is requested, but does not already exist.

	public static PoolManager instance
	{
		get
		{
			if (poolManager == null)
				poolManager = new PoolManager();
			return poolManager;
		}
	}

	void Awake()
	{
		poolManager = this;
	}

	void Start()
	{
		foreach(ObjectPool pool in pools) 
		{
			GameObject newPool = new GameObject();
			newPool.transform.position = this.transform.position;
			newPool.transform.rotation = this.transform.rotation;

			newPool.transform.SetParent (this.transform);

			pool.SetTransform = newPool.transform;

			pool.CreatePool ();
		}
	}

	//Search through pools to find a prefab that is the same as objectPrefab
	public GameObject GetObject(GameObject objectPrefab) 
	{
		GameObject objectToReturn = null;

		foreach (ObjectPool pool in pools)
		{
			if (objectPrefab == pool.PoolPrefab) 
			{
				objectToReturn = pool.GetObject ();
				break;
			} 
		}

		return objectToReturn;	//TODO ??? Create new object if pool limits haven't been reached ???
	}
	public GameObject GetObject(string getObjectName)
	{
		GameObject sendObject = null;

		foreach (ObjectPool pool in pools) 
		{
			if (string.Equals(getObjectName, pool.PoolName)) 
			{
				sendObject = pool.GetObject();
				break;
			} 
		}

		return sendObject;	//TODO ??? Create new object if pool limits haven't been reached ???
	}

	public void ReturnObject( GameObject returningObject )
	{
		if (returningObject == null)
			return;
			
		PoolObject po = returningObject.GetComponent<PoolObject> ();

		if(po != null)
		{
			po.objectPool.ReturnObject (returningObject);
		}
		else
		{
			foreach(ObjectPool pool in pools)
			{
				if (pool.Contains (returningObject)) 
				{
					pool.ReturnObject (returningObject);
					break;
				}
			}
		}
	}

	public int TotalPoolSize()
	{
		int total = 0;

		foreach (ObjectPool pool in pools)
		{
			total += pool.PoolSize;
		}
		return total;
	}

}
