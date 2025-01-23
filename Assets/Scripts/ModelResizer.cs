using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelResizer : MonoBehaviour
{
    [Header("Variable to scale the 3d model into")]
    [Range(0.5f,10)]
    [SerializeField] private float _scaleFactor = 1;
    void Start()
    {
        RecalculateSize();
    }

    private void RecalculateSize()
    {
        Transform pivot = CreateCenterPoint();
        transform.parent = pivot;
        pivot.position = Vector3.zero;
        pivot.localScale = Vector3.one * GetScale();
    }

    private Transform CreateCenterPoint()
    {
        GameObject pivot = new GameObject("pivot");
        pivot.transform.position = FindCenterPoint();
        return pivot.transform;

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
    private float GetScale()
    {
        Vector3 totalbounds = ClaculateBounds(GetComponentsInChildren<Renderer>());
        return  _scaleFactor / ((totalbounds.x+totalbounds.y+totalbounds.z)/3);
    
    }

    private Vector3 ClaculateBounds(Renderer[] renderers)
    {
        Bounds bounds = new Bounds(transform.position, Vector3.zero);

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
