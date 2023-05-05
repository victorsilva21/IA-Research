using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IA_Tabelas : MonoBehaviour
{
    List<GameObject> listaDeObjetos = new List<GameObject>();

    // Tabela de ações
    Dictionary<string, System.Action> tabelaDeAcoes = new Dictionary<string, System.Action>();

    // Start é chamado antes do primeiro frame
    void Start()
    {
        // Definir tabela de ações
        tabelaDeAcoes["Lista vazia"] = PesquisarObjetos;
        tabelaDeAcoes["Objeto é preto"] = ExcluirObjeto;
        tabelaDeAcoes["Objeto não é preto"] = MoverParaObjeto;

        StartCoroutine("ExecutarIA");
    }

    IEnumerator ExecutarIA()
    {
        while (true)
        {
            // Verificar a situação atual
            string situacaoAtual = VerificarSituacaoAtual();

            // Selecionar a ação correspondente na tabela de ações
            System.Action acao = tabelaDeAcoes[situacaoAtual];

            // Executar a ação
            acao();

            yield return null;
        }
    }

    string VerificarSituacaoAtual()
    {
        if (listaDeObjetos.Count == 0)
        {
            return "Lista vazia";
        }
        else if (listaDeObjetos[0].GetComponent<SpriteRenderer>().color == Color.black)
        {
            return "Objeto é preto";
        }
        else
        {
            return "Objeto não é preto";
        }
    }

    void PesquisarObjetos()
    {
        listaDeObjetos.Clear();
        int j = GameObject.FindGameObjectsWithTag("Objeto").Length;
        for (int i = 0; i < j; i++)
        {
            listaDeObjetos.Add(GameObject.FindGameObjectsWithTag("Objeto")[i]);
            print(listaDeObjetos[i]);
        }
    }

    void ExcluirObjeto()
    {
        GameObject objetoParaExcluir = listaDeObjetos[0];
        listaDeObjetos.RemoveAt(0);
        
    }

    IEnumerator MoveToObject()
    {
        GameObject objetoParaMover = listaDeObjetos[0];
        Vector3 posicaoAlvo = objetoParaMover.transform.position;

        while (transform.position != posicaoAlvo)
        {
            transform.position = Vector3.MoveTowards(transform.position, posicaoAlvo, 1 * Time.deltaTime);
            yield return new WaitForSeconds(0.1f);
        }

        Destroy(objetoParaMover);
        listaDeObjetos.RemoveAt(0);

    }
    
    void MoverParaObjeto()
    {
        StartCoroutine(MoveToObject());
        
    }
}
