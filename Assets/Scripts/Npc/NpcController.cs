using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class NpcController : MonoBehaviour
{
    public NavMeshAgent agent;
    public float wanderRadius = 5f;
    public float wanderTimer = 5f;
    public Animator anim; // Adiciona uma referência ao componente Animator
    private bool andarAtivo = false;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>(); // Obtém o componente Animator

        StartCoroutine(Wander());
    }

    private IEnumerator Wander()
    {
        while (true)
        {
            // Espera por um tempo aleatório
            yield return new WaitForSeconds(Random.Range(1f, 5f));

            // Define um destino aleatório dentro do raio especificado
            Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
            agent.SetDestination(newPos);

            // Ativa a animação de "Andar" apenas se não estiver ativa
            if (!andarAtivo)
            {
                Debug.Log("TTgit");
                anim.SetBool("Andar", true);
                andarAtivo = true;
            }

            // Espera até que o NPC alcance o destino
            yield return new WaitUntil(() => agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending);

            // Agora, esperamos até que o NPC esteja completamente parado
            yield return new WaitUntil(() => agent.velocity.magnitude == 0);
            
            

            // Chama o método quando o NPC atinge o destino
            ChegueiNoDestino();
            
            // Espera por um tempo aleatório antes de escolher um novo destino
            yield return new WaitForSeconds(wanderTimer);
        }
    }

    private void ChegueiNoDestino()
    {
        anim.SetBool("Andar", false);
        Debug.Log("Cheguei no destino!");
        andarAtivo = false;
    }

    private Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        // Gera um ponto aleatório dentro de uma esfera
        Vector3 randomDirection = Random.insideUnitSphere * dist;

        randomDirection += origin;

        NavMeshHit navHit;
        NavMesh.SamplePosition(randomDirection, out navHit, dist, layermask);

        return navHit.position;
    }
}
