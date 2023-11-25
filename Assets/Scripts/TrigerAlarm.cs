using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class TrigerAlarm : MonoBehaviour
{
   public bool IsAlarm { get; private set; }
    public static List<Crook> Crooks { get; private set; }

    static TrigerAlarm()
    {
        Crooks = new List<Crook>();
    }

    private void Awake()
    {
        IsAlarm = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (IsAlarm == false)
        {
            if (collision.TryGetComponent<Crook>(out Crook crook))
            {
                Crooks.Add(crook);
                IsAlarm = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (IsAlarm == true)
        {
            if (collision.TryGetComponent<Crook>(out Crook crook))
            {
                Crooks.Remove(crook);
                IsAlarm = false;
            }
        }
    }
}
