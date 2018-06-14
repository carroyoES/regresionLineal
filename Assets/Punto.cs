using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Punto : MonoBehaviour {

    private float posX, posY;
    

    public Punto(Vector3 posicion)
    {
        posX = posicion.x;
        posY = posicion.y;
    }

    public void SetPosX(float posX)
    {
        this.posX = posX;
    }

    public void SetPosY(float posY)
    {
        this.posY = posY;
    }

    public float GetPosX()
    {
        return posX;
    }

    public float GetPosY()
    {
        return posY;
    }

}
