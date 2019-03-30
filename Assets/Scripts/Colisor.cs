using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colisor : MonoBehaviour
{
    private BoxCollider2D colisor;

    // Use this for initialization
    void Start()
    {
        colisor = GetComponent<BoxCollider2D>();
        Vector3 posicao =
        Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        float posicaoX = posicao.x;
        float posicaoY = posicao.y;
        colisor.size = new Vector2(posicaoX, posicaoY) * 2;



    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerExit2D(Collider2D outro)
    {
        Destroy(outro.gameObject);
    }
}
