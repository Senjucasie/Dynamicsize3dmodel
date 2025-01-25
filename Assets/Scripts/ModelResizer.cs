using System;
using UnityEngine;

public class ModelResizer : MonoBehaviour
{
    [Header("Variable to scale the 3d model into")]
    [Range(0.5f,10)]
    public float ScaleFactor = 1;

    private const int CORDINATEAXIS = 3;

   public void RecalculateSize(float scalefatcor = 0)
    {
        scalefatcor = scalefatcor <= 0 ? ScaleFactor : scalefatcor;

        Transform pivot = CreateCenterPoint();
        if(transform.parent != null)
        {
            Transform parent = transform.parent;
            transform.parent = null;
            DestroyImmediate(parent.gameObject);
        }
        transform.parent = pivot;
        pivot.position = Vector3.zero;
        pivot.localScale = Vector3.one * GetScale(scalefatcor);
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
    private float GetScale(float scale)
    {
        Vector3 totalbounds = ClaculateBounds(GetComponentsInChildren<Renderer>());
        return  scale / ((totalbounds.x+totalbounds.y+totalbounds.z)/ CORDINATEAXIS);
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
