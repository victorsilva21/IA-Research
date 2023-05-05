using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class IA_Reativo : MonoBehaviour
{
    List<GameObject> listaDeObjetos = new List<GameObject>();
    
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("PesquisaDeObjetos");        
        

    }
    void FixedUpdate()
    {
        if(listaDeObjetos.Count > 0)
        {
            StartCoroutine("ExecucaoDeAcao");
        }
        else
        {
           StartCoroutine("PesquisaDeObjetos");  
        }

    }

    void PreencherLista()
    {
        int j = GameObject.FindGameObjectsWithTag("Objeto").Length;
        for(int i = 0; i < j; i++)
        {
            listaDeObjetos.Add(GameObject.FindGameObjectsWithTag("Objeto")[i]);
            print(listaDeObjetos[i]);

        }
    }
    void AnaliseDeLista()
    {
        GameObject[] ObjetosParaExcluir = new GameObject[listaDeObjetos.Count];
        for(int i = 0; i < listaDeObjetos.Count;i++)
        {
            if(listaDeObjetos[i].GetComponent<SpriteRenderer>().color == Color.black)
            {                   
                ObjetosParaExcluir[i] = listaDeObjetos[i];                
            }            
        }
        for(int i = 0; i<ObjetosParaExcluir.Length;i++)
        {
            listaDeObjetos.Remove(ObjetosParaExcluir[i]);            
        }
    }
   
    Coroutine PesquisaDeObjetos()
    {
        PreencherLista();
        AnaliseDeLista();
        return null;

    }

    IEnumerator ExecucaoDeAcao()
    {
        for(int i = 0; i < listaDeObjetos.Count;i++)
        {
            while(transform.position != listaDeObjetos[i].transform.position)
            {
                transform.position =  Vector3.MoveTowards(transform.position, listaDeObjetos[i].transform.position,1*Time.deltaTime);
                yield return new WaitForSeconds(0.1f);
                
                
                
            }
            Destroy(listaDeObjetos[i]);
            listaDeObjetos.RemoveAt(i);

        }
        

    }

   
}

