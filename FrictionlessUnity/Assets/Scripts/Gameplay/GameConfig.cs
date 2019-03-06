using UnityEngine;
using Frictionless;

public class GameConfig : MonoBehaviour
{
	void Awake()
	{
		ServiceFactory.Instance.RegisterSingleton<MessageRouter>();
	}
}
