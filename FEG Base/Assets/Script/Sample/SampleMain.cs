using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using state;
using UI;

public class SampleMain : MonoBehaviour
{
    [SerializeField]
    Transform trans;

    private enum MainState
    {
        Title,
        MainGame,
    }

    private State<MainState>  mainState = new State<MainState>();


    // Start is called before the first frame update
    void Start()
    {
        var sample = new SamplePresenter(trans);
        sample.Setup();
    }

    // Update is called once per frame
    void Update()
    {
        switch(mainState.GetState())
        {
            case MainState.Title:
                break;

            case MainState.MainGame:
                break;
        }
    }
}
