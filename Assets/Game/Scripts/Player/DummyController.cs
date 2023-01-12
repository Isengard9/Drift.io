using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyController : MonoBehaviour
{

    [SerializeField] private Transform player;

    private void Update()
    {
        FollowPlayer();
    }

    private void FollowPlayer()
    {
        transform.position = player.position;
    }
}
