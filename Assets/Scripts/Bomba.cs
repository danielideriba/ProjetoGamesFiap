using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomba : MonoBehaviour
{
    private Rigidbody2D rb2d;

    [SerializeField]
    private float velocidade;
    // Use this for initialization
    void Start()
    {

        Movimentar();
    }

    private void Movimentar()
    {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.velocity = transform.right * velocidade;
    }

}
