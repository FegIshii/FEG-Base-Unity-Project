using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace state
{
    public class State<TState>
    {
        /// <summary>
        /// 現在のステート
        /// </summary>
        private TState state { get; set; }
        /// <summary>
        /// ひとつ前のステート
        /// </summary>
        private TState prevState { get; set; }

        /// <summary>
        /// 現在のステートを取得
        /// </summary>
        /// <returns></returns>
        public TState GetState()
        {
            return state;
        }

        public void SetState(TState _state)
        {
            prevState = state;
            state = _state;
        }
    }
}