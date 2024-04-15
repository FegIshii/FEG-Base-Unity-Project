using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Addressable;

namespace UI
{
    public abstract class Presenter<TView> : MonoBehaviour
    {
        public Transform parentTransform;

        /// <summary>
        /// �r���[�̓ǂݍ��݂��s��
        /// </summary>
        private AddressablePrefabTask viewPrefab { get; set; }

        /// <summary>
        /// �r���[
        /// </summary>
        protected TView view { get; set; }

        /// <summary>
        /// Prefab��ǂݍ���
        /// </summary>
        /// <param name="_path"></param>
        protected void LoadViewAsync(string _path)
        {
            viewPrefab = new AddressablePrefabTask();
            viewPrefab.LoadAsync(_path, _instance =>
            {
                _instance.transform.SetParent(parentTransform, false);

                view = _instance.GetComponent<TView>();

                OnLoaded(view);
            });
        }

        // ���[�h
        public virtual bool Initialize()
        {
            return true;
        }

        // �ǂݍ��݊���
        public virtual void OnLoaded(TView _view)
        {
            // pass
        }
    }
}