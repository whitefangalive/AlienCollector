using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class FloatingScrapScript : MonoBehaviour
{
    public Button.ButtonClickedEvent OnGrab;
    private PlayerStats ps;
    public int amountToAdd = 3;
    private void Update()
    {
        if (ps == null)
        {
            ps = GameObject.Find("SaveState").GetComponent<PlayerStats>();
        }
        if (transform.position.x > 10 || transform.position.x < -30)
        {
            Destroy(gameObject);
        }
        if (transform.position.y > 30 || transform.position.y < -30)
        {
            Destroy(gameObject);
        }
    }
    private void OnMouseUp()
    {
        OnGrab.Invoke();
    }
    public void increaseScrap()
    {
        ps.Scrap += amountToAdd;
    }
    public void GiveCow(GameObject cow)
    {
        if (ps.OwnedCows.ContainsKey(cow.name))
        {
            ps.OwnedCows.Add(cow.name, ps.OwnedCows[cow.name] + 1);
        } else
        {
            ps.OwnedCows.Add(cow.name, 1);
        }
        
    }
    public void deleteSelf()
    {
        Destroy(gameObject);
    }

}
