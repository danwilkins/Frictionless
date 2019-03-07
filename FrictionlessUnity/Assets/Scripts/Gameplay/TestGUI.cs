using UnityEngine;
using Frictionless;

public class TestGUI : MonoBehaviour
{
	private float _buttonHeightDelta = 40.0f;
	
	void OnGUI()
	{
		float y = _buttonHeightDelta;
		if (GUI.Button(new Rect(40.0f, y, 230.0f, 40.0f), "Drop Balls"))
		{
			ServiceFactory.Instance.Resolve<MessageRouter>().RaiseMessage(new DropMessage() { Force = 500.0f });
		}

		y += _buttonHeightDelta;
		if (GUI.Button(new Rect(40.0f, y, 230.0f, 40.0f), "Toggle Cube Gravity"))
		{
			ServiceFactory.Instance.Resolve<MessageRouter>().RaiseMessage(new ToggleGravityMessage());		
		}
		
		y += _buttonHeightDelta;
		if (GUI.Button(new Rect(40.0f, y, 230.0f, 40.0f), "Reset Positions"))
		{
			ServiceFactory.Instance.Resolve<MessageRouter>().RaiseMessage(new ResetStateMessage());
		}
		
		y += _buttonHeightDelta;
		if (GUI.Button(new Rect(40.0f, y, 230.0f, 40.0f), "Debug All Message Handlers"))
		{
			Debug.Log(ServiceFactory.Instance.Resolve<MessageRouter>().GetAllHandlerDebugInfo());
		}
		
		y += _buttonHeightDelta;
		if (GUI.Button(new Rect(40.0f, y, 230.0f, 40.0f), "Debug Drop Ball Message Handler"))
		{
			Debug.Log(ServiceFactory.Instance.Resolve<MessageRouter>().GetHandlerDebugInfo<DropMessage>());
		}
	}
}