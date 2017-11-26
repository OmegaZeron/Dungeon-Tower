
public interface iPoolableObject 
{
	void Initialize ();		//called on the object only when the pool is first created.

	void OnExitPool ();		//called whenever the object 'leaves' it's pool

	void OnEnterPool ();	//called whenever the object returns to it's pool
}
