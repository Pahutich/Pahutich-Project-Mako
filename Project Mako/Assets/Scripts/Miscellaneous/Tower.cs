using System;
using System.Collections;
using System.Collections.Generic;
using Mako.Movement;
using Mako.Events;
using UnityEngine;
using Event = Mako.Events.Event;
using Unity.VisualScripting;

namespace Mako.Miscellaneous
{
  public class Tower : MonoBehaviour
  {
    [SerializeField] private float range;
    private bool isPlayerInZone = false;
    private PlayerController player;
    public Event OnPlayerEnteredRange;
    public Event OnPlayerLeftRange;
    void Awake()
    {
      player = GameObject.FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
      bool previousAwareState = isPlayerInZone;
      if (Vector3.Distance(transform.position, player.transform.position) <= range)
      {
        isPlayerInZone = true;
      }
      else
      {
        isPlayerInZone = false;
      }
      if (!StateChangeOccured(previousAwareState))
        return;
      if (isPlayerInZone == true)
      {
        OnPlayerEnteredRange.Occurred(gameObject);
      }
      else
      {
        OnPlayerLeftRange.Occurred(gameObject);
      }
    }

    private bool StateChangeOccured(bool previousState)
    {
      return !previousState == isPlayerInZone;
    }
    private void OnDrawGizmos()
    {
      Gizmos.color = Color.yellow;
      Gizmos.DrawWireSphere(transform.position, range);
    }
    void OnDestroy()
    {
      OnPlayerLeftRange.Occurred(gameObject);
    }
  }

}

