using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace state
{
    public class State<TState>
    {
        /// <summary>
        /// ���݂̃X�e�[�g
        /// </summary>
        private TState state { get; set; }
        /// <summary>
        /// �ЂƂO�̃X�e�[�g
        /// </summary>
        private TState prevState { get; set; }

        /// <summary>
        /// ���݂̃X�e�[�g���擾
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