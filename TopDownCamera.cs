using UnityEngine;
using System.Collections;

public class TopDownCamera : MonoBehaviour {

    [System.Serializable]
    public class PositionSettings
    {
        //aqui é o quao alto a camera vai estar
        public float distanceFromTarget = -20;
        //se está habilitado o zoom
        public bool allowZoom = true;
        //velocidade do zoom
        public float zoomSmooth = 100;
        //niveis de zoom
        public float zoomStep = 2;
        //min e max de zoom
        public float maxZoom = -30;
        public float minZoom = -60;
        //aqui voce escolhe se vai querer travar no destino ou viajar até o destino (true) viajando
        public bool smoothFollow = true;
        //velocidade de viagem da camera
        public float smooth = 0.05f;

        [HideInInspector]
        //destino dda camera quando dar zoom
        public float newDistance = -20;

    }
    [System.Serializable]
    public class OrbitSettings
    {
        //atual rotação x e y da camera
        public float xRotation = -25;
        public float yRotation = -180;
        //permite ou nao orbitar
        public bool allowOrbit = true;
        //velocidade de orbita
        public float yOrbitSmooth = 0.5f;

    }
    [System.Serializable]
    public class InputSettings
    {
        //faz com que o Input no project settings use esses parametros para identificar os Inputs
        //Criar novos Axis no Inputmanager 
        public string MOUSE_ORBIT = "MouseOrbit";
        public string ZOOM = "Mouse ScrollWheel";

    }
    //referencia as classes que carregam os atributos.
    public PositionSettings position = new PositionSettings();
    public OrbitSettings orbit = new OrbitSettings();
    public InputSettings input = new InputSettings();

    public Transform target;
    Vector3 destination = Vector3.zero;
    Vector3 camVelocity = Vector3.zero;
    Vector3 currentMousePosition = Vector3.zero;
    Vector3 previousMousePosition = Vector3.zero;
    float mouseOrbitInput, zoomInput;
    
    void Start () {
        SetCameraTarget(target);
        if (target)
        {
            MoveToTarget();
        }
	
	}
	
	void Update () {
        //recebe os inputs
        GetInput();
        //zooming
        if (position.allowZoom)
        {
            ZoomInOnTarget();
        }
    }

    void FixedUpdate()
    {
        if (target)
        {
            MoveToTarget();
            LookAtTarget();
            MouseOrbitTarget();
        }

    }
    void LookAtTarget()
    {
        //faz a camera olhar para o target
        Quaternion targetRotation = Quaternion.LookRotation(target.position - transform.position);
        transform.rotation = targetRotation;
    }

    //seta o target da camera
    public void SetCameraTarget(Transform t)
    {
        target = t;

        if(target == null)
        {
            Debug.LogError("Your Camera needs a target");
        }


    }

    void GetInput()
    {
        //valores das nossas variaveis de entrada
        mouseOrbitInput = Input.GetAxisRaw(input.MOUSE_ORBIT);
        zoomInput = Input.GetAxisRaw(input.ZOOM);
    }

    void MoveToTarget()
    {
        //leva a camera até a posição destino
        destination = target.position;
        destination += Quaternion.Euler(orbit.xRotation, orbit.yRotation, 0) * -Vector3.forward * position.distanceFromTarget;

        if (position.smoothFollow)
        {
            transform.position = Vector3.SmoothDamp(transform.position, destination, ref camVelocity, position.smooth);
        }
        else
        {
            transform.position = destination;
        }
    }

    void MouseOrbitTarget()
    {
        //faz a camera orbitar em volta do target
        previousMousePosition = currentMousePosition;
        currentMousePosition = Input.mousePosition;

        if (mouseOrbitInput > 0)
        {
            orbit.yRotation += (currentMousePosition.x - previousMousePosition.x) * orbit.yOrbitSmooth;
        }
    }
    void ZoomInOnTarget()
    {
        //modifica a distancia do target
        position.newDistance += position.zoomStep * zoomInput;
        position.distanceFromTarget = Mathf.Lerp(position.distanceFromTarget, position.newDistance, position.zoomSmooth * Time.deltaTime);
        if(position.distanceFromTarget > position.maxZoom)
        {
            position.distanceFromTarget = position.maxZoom;
            position.newDistance = position.maxZoom;
        }
        if (position.distanceFromTarget < position.minZoom)
        {
            position.distanceFromTarget = position.minZoom;
            position.newDistance = position.minZoom;
        }
    }

    

}
