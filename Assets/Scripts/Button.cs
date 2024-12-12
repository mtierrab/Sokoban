using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public bool isActive = false;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Moveable"))
        {
            isActive = true;
            Debug.Log("Button activated");
            GameManager.Instance.CheckLevelComplete();
        }
    }
    // Update is called once per frame
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Moveable"))
        {
            isActive = false;
            Debug.Log("button deactivated");
        }
    }
}
