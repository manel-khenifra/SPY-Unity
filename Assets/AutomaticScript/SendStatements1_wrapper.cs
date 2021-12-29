using UnityEngine;
using FYFY;

[ExecuteInEditMode]
public class SendStatements1_wrapper : MonoBehaviour
{
	private void Start()
	{
		this.hideFlags = HideFlags.HideInInspector; // Hide this component in Inspector
	}

	public void initGBLXAPI()
	{
		MainLoop.callAppropriateSystemMethod ("SendStatements1", "initGBLXAPI", null);
	}

	public void testSendStatement()
	{
		MainLoop.callAppropriateSystemMethod ("SendStatements1", "testSendStatement", null);
	}

	public void sendStartedStatement(System.String nb_level)
	{
		MainLoop.callAppropriateSystemMethod ("SendStatements1", "sendStartedStatement", nb_level);
	}

	public void sendCompletedStatement()
	{
		MainLoop.callAppropriateSystemMethod ("SendStatements1", "sendCompletedStatement", null);
	}

	public void sendActionStatement(System.String action)
	{
		MainLoop.callAppropriateSystemMethod ("SendStatements1", "sendActionStatement", action);
	}

	public void sendExecuteStatement()
	{
		MainLoop.callAppropriateSystemMethod ("SendStatements1", "sendExecuteStatement", null);
	}

}
