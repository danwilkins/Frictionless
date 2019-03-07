﻿using UnityEngine;
using Frictionless;

public class Ball : MonoBehaviour
{
    private Rigidbody _rigidbody;

    private Vector3 _initialPosition;
    
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _initialPosition = transform.position;
        
        ServiceFactory.Instance.Resolve<MessageRouter>().AddHandler<DropMessage>(OnDrop);
        ServiceFactory.Instance.Resolve<MessageRouter>().AddHandler<ResetStateMessage>(OnResetState);
    }

    void OnDestroy()
    {
        ServiceFactory.Instance.Resolve<MessageRouter>().RemoveHandler<DropMessage>(OnDrop);
        ServiceFactory.Instance.Resolve<MessageRouter>().RemoveHandler<ResetStateMessage>(OnResetState);
    }
    
    private void OnDrop(DropMessage dropMessage)
    {
        _rigidbody.isKinematic = false;
        _rigidbody.AddForce(new Vector3(0.0f, -Random.value * dropMessage.Force,0.0f));
    }
    
    private void OnResetState(ResetStateMessage obj)
    {
        _rigidbody.isKinematic = true;
        _rigidbody.velocity = Vector3.zero;
        transform.position = _initialPosition;
    }
}