using UnityEngine;
using Frictionless;

public class Ball : MonoBehaviour
{
    void Start()
    {
        ServiceFactory.Instance.Resolve<MessageRouter>().AddHandler<DropCommand>(HandleDropCommand);
    }

    private void HandleDropCommand(DropCommand dropCommand)
    {
        GetComponent<Rigidbody>().isKinematic = false;
        GetComponent<Rigidbody>().AddForce(new Vector3(Random.value * dropCommand.Force * 0.1f,
            Random.value * dropCommand.Force,
            Random.value * dropCommand.Force * 0.1f));
    }
}