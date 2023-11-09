using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubjectPlant : MonoBehaviour
{
    public static SubjectPlant inst;
    private List<IObserverPlanta> list;

    private void Awake() {
        list = new List<IObserverPlanta>();
        inst = this;
    }
    
    public void Add(IObserverPlanta observer) {
        list.Add(observer);
    }

    public void NotifyPlantaAll(){
        foreach (IObserverPlanta b in list)
        {
            b.NotifyPlanta();
        }
    }
}