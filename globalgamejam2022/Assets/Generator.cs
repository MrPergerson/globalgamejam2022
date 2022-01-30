using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Generator : MonoBehaviour
{
    PlayerControls playerControls;

    [Header("Status")]
    [SerializeField] float repairProgress = 0;
    [SerializeField] float repairSpeed = 1;
    [SerializeField] bool isRepaired = false;
    [SerializeField] bool isRepairing = false;

    [Header("UI")]
    [SerializeField] Slider slider;

    [Header("Particles")]
    [SerializeField] ParticleSystem particles;

    public delegate void GeneratorRepaired();
    public event GeneratorRepaired OnGeneratorRepaired;

    private void Awake()
    {
        playerControls = new PlayerControls();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    private void Update()
    {
        if(!isRepaired && isRepairing)
        {
            repairProgress += repairSpeed * Time.deltaTime;
            slider.value = repairProgress;

            if (repairProgress >= 100)
            {
                isRepaired = true;
                OnGeneratorRepaired?.Invoke();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            isRepairing = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            isRepairing = false;
        }

        //repairProgress = 0;
        //slider.value = repairProgress;
    }

}
