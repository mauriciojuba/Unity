using UnityEngine;
using System.Collections;

public class InteractiveObjectsShaderHandler : MonoBehaviour {

    //Variavel que guardará o shader inicial do objeto
    private string InitialShader;

    bool gotShader = false;

    //Quando o mouse está em cima do objeto
    void OnMouseOver()
    {
        //se ele já guardou o shader inativo, então ele pode trocar para o shader de highlight
        if (gotShader) GetComponent<Renderer>().material.shader = Shader.Find("Self-Illumin/Outlined Diffuse");
        //se não
        else
        {
            //se ele ainda não guardou o nome do shader inativo, então ele deve guardar.
            InitialShader = GetComponent<Renderer>().material.shader.name;
            //avisa que o nome já foi guardado
            gotShader = true;
        }
    }

    //Mouse fora do objeto
    void OnMouseExit()
    {
        //volta para o shader inicial
        GetComponent<Renderer>().material.shader = Shader.Find("" + InitialShader);
    }
}
