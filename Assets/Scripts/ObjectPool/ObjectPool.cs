using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class ObjectPool
{

	[SerializeField] private string poolName = "";
	[SerializeField] private GameObject poolObject;
	[SerializeField] private int poolSize = 1;

	[SerializeField] private Transform poolTransform;

	[SerializeField] private List<GameObject> objectsInPool = new List<GameObject> ();
	[SerializeField] private List<GameObject> activePool = new List<GameObject> ();
	[SerializeField] private List<GameObject> inactivePool = new List<GameObject> ();

	public string PoolName
	{
		get { return poolName; }
		set { poolName = value; }
	}

	public GameObject PoolPrefab
	{
		get{ return poolObject; }
		set{ poolObject = value; }
	}

	public int PoolSize
	{
		get { return poolSize; }
		set { poolSize = value; }
	}

	public Transform SetTransform
	{
		get{ return poolTransform; }
		set{ poolTransform = value; }
	}

	public bool Contains(GameObject go)
	{
		if (objectsInPool.Contains (go))
			return true;
		else
			return false;
	}

	
	public GameObject GetObject()
	{
		if (inactivePool.Count > 0)
		{
			GameObject getObject = inactivePool[0];
			inactivePool.RemoveAt(0);
			activePool.Add (getObject);

			getObject.gameObject.SetActive(true);

			iPoolableObject ipObject = getObject.GetComponent<iPoolableObject>();
			if (ipObject != null)
			{
				ipObject.OnExitPool();
			}

			getObject.transform.SetParent (getObject.transform);
			return getObject;
		}
		else
		{
			return null;
		}
	}

	public void ReturnObject(GameObject retObject)
	{
		if (retObject == null)
			return;

		iPoolableObject ipObject = retObject.GetComponent<iPoolableObject>();
		if (ipObject != null)
		{
			ipObject.OnEnterPool();
		}

		if (activePool.Count > 0)
		{
			activePool.Remove(retObject);
			inactivePool.Add (retObject);
		}
		
		retObject.transform.SetParent (poolTransform);
		retObject.gameObject.SetActive(false);
	}


	public void CreatePool()
	{
		if (poolTransform == null)
		{
			poolTransform = new GameObject ().transform;
		}

		poolName = poolObject.name;
		poolTransform.gameObject.name = poolObject.name + " Pool";


		if (poolObject.GetComponent<iPoolableObject> () == null) 
		{
			Debug.LogError("Object " + poolObject.name + " should implement 'iPoolableObject'.");
		}
		else if (poolObject.GetComponent<PoolObject> () == null) 
		{
			Debug.LogWarning ("Object " + poolObject.name + " should inherit from PoolObject if possible.");
		}

		for (int i = 1; i <= poolSize; i++) 
		{
			GameObject newObject = MonoBehaviour.Instantiate (poolObject);

			newObject.transform.SetParent (poolTransform);

			objectsInPool.Add (newObject);
			inactivePool.Add (newObject);

			PoolObject po = newObject.GetComponent<PoolObject> ();
			if (po != null) 
			{
				po.objectPool = this;
				po.Initialize ();
			}
			else 
			{
				iPoolableObject iPO = newObject.GetComponent<iPoolableObject> ();
				if (iPO != null)
					iPO.Initialize ();
			}

			newObject.SetActive (false);
		}

	}

}