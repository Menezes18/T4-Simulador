using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class PlantTrigger : MonoBehaviour, IObserverPlanta
{
    [SerializeField] private Plant plant;
    [SerializeField] private Estacao estacaoPlanta;
    public bool EstaEstacao = false;
    private float segundos = 0;
    private float multiplacador;
    private float soma = 86400f;
    public GameObject PrefabFinal;
    public bool cresceu = false;
    [SerializeField] public int aguadaplanta;
    [SerializeField] private int idadePlanta = 0;
    public CanvasInfo CanvasInfoObj;
    private int diaPlanta;
    [SerializeField] private int ciclodia;
    private Transform t;
    public GameObject previousPrefab;
    private bool isWatered = false;

    public void Start()
    {
        
        t = this.transform;
        if (plant == null)
        {
            return;
        }

        diaPlanta = plant.dias;
        multiplacador = 86400f / CicloDiaNoite.ciclo.duracaoDoDia;
        estacaoPlanta = plant.Estacao;
        SubjectPlant.inst.Add(this);
    }

    public void NotifyPlanta(Estacao estacaoAtual)
    {
        if (estacaoPlanta == estacaoAtual)
        {
            EstaEstacao = true;
        }
        else if (estacaoPlanta != estacaoAtual)
        {
            EstaEstacao = false;
        }
    }

    public void AdicionarAgua()
    {
        aguadaplanta += 25;
         verificaAgua();
        Debug.Log(aguadaplanta);
        
    }

    public void MudarTextCanvas(string text)
    {
        CanvasInfoObj = GetComponent<CanvasInfo>();
        CanvasInfoObj.message = text;
    }
    public void verificaAgua()
    {
        if (aguadaplanta > 25)
        {
            isWatered = true;
            MudarTextCanvas("Planta Com água " + aguadaplanta + "%");
        }else if (aguadaplanta < 25)
        {
            MudarTextCanvas("Regar (SEM ÁGUA)");
            isWatered = false;
        }
    }
    public void ColherPlanta()
    {
        if (idadePlanta >= diaPlanta)
        {
            DroparPlanta();
            ExcluirPlanta();
        }
    }

    private void DroparPlanta()
    {
        Vector3 finalPrefabPosition = new Vector3(t.position.x, t.position.y + 0.5f, t.position.z);
        Instantiate(PrefabFinal, finalPrefabPosition, Quaternion.identity);
    }

    private void ExcluirPlanta()
    {
        Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        if (EstaEstacao)
        {

            if (aguadaplanta > 0)
            {
                verificaAgua();
                ciclodiaPlant();
                plantaEstagio();

                if (aguadaplanta <= 0)
                {
                    verificaAgua();
                    aguadaplanta = 0;
                    //sDebug.Log("Planta em " + transform.position + " está sem água!");
                }
            }
        }
    }

    public void ciclodiaPlant()
    {
        if (EstaEstacao && isWatered)
        {
            segundos += Time.deltaTime * multiplacador;

            if (segundos >= soma)
            {
                segundos = 0;
                aguadaplanta -= 25;
                idadePlanta++;
            }
        }
        else
        {
           
        }
    }

    public void plantaEstagio()
    {
        ciclodia = diaPlanta / 3;
        if (idadePlanta == ciclodia)
        {
            t.position = new Vector3(t.position.x, plant.transform, t.position.z);
            GetPrefab(1, t);
        }
        else if (idadePlanta == ciclodia * 2)
        {
            t.position = new Vector3(t.position.x, plant.transform, t.position.z);
            GetPrefab(2, t);
        }
        else if (idadePlanta == diaPlanta)
        {
            t.position = new Vector3(t.position.x, plant.transform, t.position.z);
            cresceu = true;
            MudarTextCanvas("Cresceu!");
            GetPrefab(3, t);
        }
    }

    public GameObject GetPrefab(int estagio, Transform parent)
    {
        if (estagio >= 1 && estagio <= 3)
        {
            if (previousPrefab != null)
            {
                Destroy(previousPrefab);
            }
            GameObject prefab = plant.prefabs[estagio - 1];
            GameObject newPrefab = Instantiate(prefab, parent);
            previousPrefab = newPrefab;
            return newPrefab;
        }
        else
        {
            //Debug.Log("Estágio inválido");
            return null;
        }
    }
}
