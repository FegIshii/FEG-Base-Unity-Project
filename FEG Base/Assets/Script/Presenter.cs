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
        /// ビューの読み込みを行う
        /// </summary>
        private AddressablePrefabTask viewPrefab { get; set; }

        /// <summary>
        /// ビュー
        /// </summary>
        protected TView view { get; set; }

        /// <summary>
        /// Prefabを読み込む
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

        // ロード
        public virtual bool Initialize()
        {
            return true;
        }

        // 読み込み完了
        public virtual void OnLoaded(TView _view)
        {
            // pass
        }
    }
}