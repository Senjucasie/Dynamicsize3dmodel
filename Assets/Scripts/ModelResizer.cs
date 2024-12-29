using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelResizer : MonoBehaviour
{
   
    void Start()
    {
        CreateCenterPoint();
    }

    private void CreateCenterPoint()
    {
        GameObject updatedpivot = new GameObject("pivot");
        updatedpivot.transform.position = FindCenterPoint();
        transform.parent = updatedpivot.transform;
    }

    private Vector3 FindCenterPoint()
    {
        Bounds bounds = new Bounds(transform.position,Vector3.zero);
        Renderer[] renderers = GetComponentsInChildren<Renderer>();

        if(renderers == null || renderers.Length == 0 )
        {
            Debug.LogWarning($"Didnot Find any renders in this gameobject : {this.name}");
            return Vector3.zero;
        }

        foreach(Renderer renderer in renderers)
            bounds .Encapsulate(renderer.bounds);
        
        return bounds.center;
    }

    
}
