using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastManager : MonoBehaviour
{
    private Vector2 _newPivotPoint;
    // Start is called before the first frame update
    void Start()
    {
        Vector2 pivotOffset = new Vector2(_newPivotPoint.x * transform.localScale.x, _newPivotPoint.y * transform.localScale.y);

        
        transform.position = (Vector2)transform.position - pivotOffset;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
