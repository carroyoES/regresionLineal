using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panel : MonoBehaviour {

   
    private List<Punto> puntos;
    private Punto puntoGO;
    float b, a, xsuma, ysuma, xmedia, ymedia, sumatorioXY, xsumaCuadrados;

    // Use this for initialization
    void Start ()
    {
        puntos = new List<Punto>();
        puntoGO = FindObjectOfType<Punto>();
        xsuma = 0;
        ysuma = 0;
        sumatorioXY = 0;
        xsumaCuadrados = 0;
	}

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
            PonerPunto();  
    }

    private void PonerPunto()
    {

        Vector3 posicion = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Punto punto = new Punto(posicion);
        Instantiate(puntoGO, new Vector3(posicion.x, posicion.y, -5), Quaternion.identity);
        puntos.Add(punto);
        RegresionLineal();
    }

    private void DibujarLinea()
    {
        float x1 = 0f;
        float x2 = 14f;
        float y2 = x2 * b + a;

        LineRenderer lineRenderer = FindObjectOfType<Linea>().GetComponent<LineRenderer>();
        lineRenderer.SetWidth(0.1f, 0.1f);
        lineRenderer.SetPosition(0, new Vector3(x1, a, -5));
        lineRenderer.SetPosition(1, new Vector3(x2, y2, -5));
    }

    private void RegresionLineal()
    {
        
        //sumo todas las x y todas las y
        for (int i = 0; i<puntos.Count; i++)
        {
            xsuma += puntos[i].GetPosX();
            ysuma += puntos[i].GetPosY();
        }
        
        //hallo sus medias
        xmedia = xsuma / puntos.Count;
        ymedia = ysuma / puntos.Count;
        //hallo sumatorio de xy
        for (int i = 0; i < puntos.Count; i++)
        {
            sumatorioXY += puntos[i].GetPosX() * puntos[i].GetPosY();
        }
        //hallo sumatorio de las x al cuadrado
        for (int i = 0; i < puntos.Count; i++)
            xsumaCuadrados += puntos[i].GetPosX() * puntos[i].GetPosX();
        //hallo a y b
        b = HallarB();
        a = ymedia - (b * xmedia);
        
        print("A:" + a);
        print("B:" + b);
        //dibujo la linea
        DibujarLinea();

        //reinicio variables
        ReiniciarVariables();
    }

    private void ReiniciarVariables()
    {
        xsuma = 0;
        ysuma = 0;
        sumatorioXY = 0;
        xsumaCuadrados = 0;
    }

    private float HallarB()
    {       
        float minuendo = (puntos.Count * sumatorioXY);
        float sustraendo = (xsuma * ysuma);             
        return (minuendo - sustraendo) / ((puntos.Count * xsumaCuadrados) - (xsuma * xsuma));
    }
}
