using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObjectToVector3 : MonoBehaviour
{
    public Transform ObjectToMove;
    public Vector3 Location;
    public Transform ObjectToGoTo;
    // Start is called before the first frame update

    public void SendPlayerToLocation()
    {
        ObjectToMove.position = Location;
        if (ObjectToGoTo != null)
        {
            ObjectToMove.position = ObjectToGoTo.position;
        }
    }

    public void ScaleObjectToSize()
    {
        ObjectToMove.localScale = Location;
        if (ObjectToGoTo != null)
        {
            ObjectToMove.localScale = ObjectToGoTo.localScale;
        }
    }
}
