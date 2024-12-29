using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelResizer : MonoBehaviour
{
   
    void Start()
    {
        CreateCenterPoint();
        checksize();
    }

    private void CreateCenterPoint()
    {
        GameObject updatedpivot = new GameObject("pivot");
        updatedpivot.transform.position = FindCenterPoint();
        transform.parent = updatedpivot.transform;
        updatedpivot.transform.position = Vector3.zero;
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
    private void checksize()
    {
        Vector3 add = Sizeofmodel();
        Debug.Log(add);
        Debug.Log( 1/((add.x+add.y+add.z)/3));
    
    }

    private Vector3 Sizeofmodel()
    {
        Bounds bounds = new Bounds(transform.position, Vector3.zero);
        Renderer[] renderers = GetComponentsInChildren<Renderer>();

        if (renderers == null || renderers.Length == 0)
        {
            Debug.LogWarning($"Didnot Find any renders in this gameobject : {this.name}");
            return Vector3.zero;
        }

        foreach (Renderer renderer in renderers)
            bounds.Encapsulate(renderer.bounds);

        return bounds.size;
    }
    
}
