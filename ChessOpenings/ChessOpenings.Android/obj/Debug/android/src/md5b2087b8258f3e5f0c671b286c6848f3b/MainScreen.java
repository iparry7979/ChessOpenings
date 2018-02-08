package md5b2087b8258f3e5f0c671b286c6848f3b;


public class MainScreen
	extends android.app.Activity
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"";
		mono.android.Runtime.register ("ChessOpenings.Droid.MainScreen, ChessOpenings.Android, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", MainScreen.class, __md_methods);
	}


	public MainScreen ()
	{
		super ();
		if (getClass () == MainScreen.class)
			mono.android.TypeManager.Activate ("ChessOpenings.Droid.MainScreen, ChessOpenings.Android, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onCreate (android.os.Bundle p0)
	{
		n_onCreate (p0);
	}

	private native void n_onCreate (android.os.Bundle p0);

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
