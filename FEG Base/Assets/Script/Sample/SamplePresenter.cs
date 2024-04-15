using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class SamplePresenter : Presenter<SampleView>
    {
        public SamplePresenter(Transform transform)
        {
            parentTransform = transform;
        }

        public void Setup()
        {
            LoadViewAsync("sample/object");
        }
    }
}