using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeManager : MonoBehaviour
{
   [SerializeField] private bool donebilirmi;

    public Sprite shapeSekil;

    public void SolaHareketPNC()
    {
        transform.Translate(Vector3.left, Space.World);
    }
    
    public  void SagaHareketPNC()
    {
        transform.Translate(Vector3.right, Space.World);
    }
    
    public void AsagiHareketPNC()
    {
        transform.Translate(Vector3.down,Space.World);
    }
    
    public void YukarýHareketPNC()
    {
        transform.Translate(Vector3.up, Space.World);
    }
    
    public  void SagaDonPNC()
    {
        if (donebilirmi)
        {
            transform.Rotate(0,0,-90);
        }
    }
    
    public void SolaDonPNC()
    {
        if (donebilirmi)
        {
            transform.Rotate(0,0,90);
        }
    }

   
}
