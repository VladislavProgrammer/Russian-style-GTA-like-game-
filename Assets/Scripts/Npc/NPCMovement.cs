using UnityEngine;
using UnityEngine.AI;

public class NPCMovement : MonoBehaviour, IMovable
{
    private NavMeshAgent agent;

    [SerializeField] private Transform[] points;

    [SerializeField] NPCAnimatorManager npcAnimatorManager;

    public bool CanMove;

	public bool isFear;

    private void Awake() => DefaultParamaetrs();

    void Update()
    {
        if (CanMove) TryMove();
    }


    void DefaultParamaetrs()
    {
        agent = GetComponent<NavMeshAgent>();
        if (agent) agent.SetDestination(points[0].position);
    }

    public void TryMove()
    {
        if (agent)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                int randomValue = Random.Range(0, points.Length);
                agent.SetDestination(points[randomValue].position);

            }
        }

    }

    public void FearMovement()
    {
		if(isFear)
		{
			CanMove = true;
        	Debug.Log("Страшно ! Стреляют !");
        	npcAnimatorManager.SetRunAnim();
        	agent.speed = 9f;
        	Invoke("StopFear", 3);

		}
        
    }

    void StopFear()
    {
        Debug.Log("Уже не страшно в целом");
		isFear = false;
        npcAnimatorManager.StopRunAnim(); 
        agent.speed = 2f;
		

    }

    
}
