using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {
	
	//atributos	
    public string Name;
    public float health;
    public float damange;
    public float range;
    //navmesh agent, usado para o personagem se mover na navmesh
    private NavMeshAgent agent;
    //Camera principal
    public Camera myCamera;
    //o ponto onde foi clicado, e para onde o jogador irá
    public GameObject marker;

    
    void Awake () {
    	//define o marker como o objeto de nome Target
        marker = GameObject.Find("Target");
        //define a navmeshagent do jogador
        agent = GetComponent<NavMeshAgent>();
    }

	void Update () {
        Move();
    }

    void Attack()
    {

    }
    void Move()
    {
    	//se o jogador clicar
        if (Input.GetMouseButtonDown(0))
        {
        	//marker vai ficar ativo
            marker.SetActive(true);
            //cria um raio entre o local que foi clicado e a camera
            Ray ray = myCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 200))
            {
            	//coloca o marker onde o jogador clicou
                marker.transform.position = hit.point;
            }
        }
        //se a distância entre o marker e o player for menor ou igual a 1
        if(Vector3.Distance(marker.transform.position, transform.position) <= 1)
        {
        	//marker fica inativo
            marker.SetActive(false);
        }
        
        //O agent faz o personagem se mover para a posição do marker
        agent.SetDestination(marker.transform.position);
    }
}
