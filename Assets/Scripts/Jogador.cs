﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jogador : MonoBehaviour
{
    [SerializeField]
    private Estado estado;

    Animator animator;

    private float moverHorizontal;
    private float moverVertical;
    private Vector2 mover;
    private Rigidbody2D rb2d;
    private AudioSource audioSource;

    private float eixoXMin, eixoXMax;
    private float eixoYMin, eixoYMax;

    private float posicaoX, posicaoY;

    [SerializeField]
    private float velocidade;

    [SerializeField]
    private GameObject instanciarBombas;

    [SerializeField]
    private GameObject prefabBomba;

    [SerializeField]
    private GameObject prefabExplosao;

    private float controle;

    [SerializeField]
    private float atirarTempo;

    void Awake()
    {
        estado.Iniciar();
    }

    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
        controle = 0f;
        eixoXMax = CameraPrincipal.LimitarDireitaX(transform.position);
        eixoXMin = CameraPrincipal.LimitarEsquerdaX(transform.position);
        eixoYMax = CameraPrincipal.LimitarParaCimaY(transform.position);
        eixoYMin = CameraPrincipal.LimitarParaBaixoY(transform.position);

        rb2d = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        moverHorizontal = Input.GetAxis("Horizontal");
        moverVertical = Input.GetAxis("Vertical");
        mover = new Vector2(moverHorizontal, moverVertical);
        rb2d.velocity = mover * velocidade;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Time.time > controle)
            {
                controle = Time.time + atirarTempo;
                Instantiate(prefabBomba, instanciarBombas.transform.position, prefabBomba.transform.rotation);

                audioSource.Play();
            }

        }
        LimitarPosicaoJogador();

    }

    void LimitarPosicaoJogador()
    {
        posicaoX = rb2d.position.x; //transform.position.x;
        posicaoY = rb2d.position.y;

        posicaoX = Mathf.Clamp(posicaoX, eixoXMin, eixoXMax);
        posicaoY = Mathf.Clamp(posicaoY, eixoYMin, eixoYMax);

        if (posicaoX != transform.position.x)
        {
            rb2d.position = new Vector2(posicaoX, rb2d.position.y);
        }

        if (posicaoY != transform.position.y)
        {
            rb2d.position = new Vector2(rb2d.position.x, posicaoY);
        }

    }

    void OnTriggerEnter2D(Collider2D outro)
    {
        PerderVida(outro);
        GanharVida(outro);
    }

    void GanharVida(Collider2D outro)
    {
        if (outro.tag == "Energia")
        {
            if (estado.ValorAtual < estado.MaximoValor)
            {
                estado.ValorAtual += 10;
                Destroy(outro.gameObject);
            }
        }
    }

    void PerderVida(Collider2D outro)
    {
        if (outro.tag == "Asteroide" || outro.tag == "Inimigo")
        {
            if (estado.ValorAtual > 0)
            {
                estado.ValorAtual -= 10;
                animator.SetTrigger("Dano");

            }

            if (estado.ValorAtual <= 0)
            {
                Instantiate(prefabExplosao, transform.position, transform.rotation);
                Destroy(gameObject);
                Mensagens.gameOver = true;
                
            }
        }
    }
}
