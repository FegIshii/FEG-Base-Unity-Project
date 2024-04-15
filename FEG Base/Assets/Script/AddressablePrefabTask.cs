using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;

namespace Addressable
{

    public class AddressablePrefabTask
    {

        /// <summary>
        /// path
        /// </summary>
        public string path { get; private set; }

        /// <summary>
        /// asyncOperationHandle
        /// </summary>
        private AsyncOperationHandle<GameObject> asyncOperationHandle { get; set; }

        /// <summary>
        /// onLoadedEvent
        /// </summary>
        private event Action<GameObject> onLoadedEvent;

        public AddressablePrefabTask()
        {
        }

        /// <summary>
        /// LoadAsync
        /// </summary>
        /// <param name="_path"></param>
        /// <param name="_instantiationParameters"></param>
        /// <param name="_onLoaded"></param>
        public void LoadAsync(string _path, InstantiationParameters _instantiationParameters,
                              Action<GameObject> _onLoaded = null)
        {
            path = _path;

#if DEVELOPMENT_BUILD
                    BeginStopwatch($"LoadAsync:[{_path}]");
#endif

            asyncOperationHandle = Addressables.InstantiateAsync(_path, _instantiationParameters);

            if (!asyncOperationHandle.IsValid())
            {
                return;
            }

            onLoadedEvent = _onLoaded;

            // キャッシュからの読み込み(即時復帰する)
            if (asyncOperationHandle.IsDone)
            {
                OnCompleted(asyncOperationHandle);
                return;
            }

            // コンプリートの予約
            asyncOperationHandle.Completed += OnCompleted;
        }

        /// <summary>
        /// LoadAsync
        /// </summary>
        /// <param name="_path"></param>
        /// <param name="_onLoaded"></param>
        public void LoadAsync(string _path, Action<GameObject> _onLoaded = null)
        {
            path = _path;

#if DEVELOPMENT_BUILD
            BeginStopwatch($"LoadAsync:[{_path}]");
#endif

            asyncOperationHandle = Addressables.InstantiateAsync(_path);

            if (!asyncOperationHandle.IsValid())
            {
                return;
            }

            onLoadedEvent = _onLoaded;

            // キャッシュからの読み込み
            if (asyncOperationHandle.IsDone)
            {
                Debug.Log($"LoadAsync Use cache:[{_path}]");
                OnCompleted(asyncOperationHandle);
                return;
            }

            // コンプリートの予約
            asyncOperationHandle.Completed += OnCompleted;
        }

        /// <summary>
        /// OnCompleted
        /// </summary>
        /// <param name="_handle"></param>
        private void OnCompleted(AsyncOperationHandle<GameObject> _handle)
        {
#if DEVELOPMENT_BUILD
            EndStopwatch();
#endif
            switch (_handle.Status)
            {
                case AsyncOperationStatus.Succeeded:
                    {
                        onLoadedEvent?.Invoke(_handle.Result);
                        break;
                    }
                case AsyncOperationStatus.Failed:
                    {
                        Release();
                        throw _handle.OperationException;
                    }
            }
        }

        /// <summary>
        /// 開放する
        /// </summary>
        public void Release()
        {
            onLoadedEvent = null;

            if (asyncOperationHandle.IsValid())
            {
                Addressables.Release(asyncOperationHandle);
            }
        }

        /// <summary>
        /// 破棄する
        /// </summary>
        public void OnDestroy()
        {
            Release();
        }
    }

}