using System.Linq;

namespace Common
{
    public class Flag<T> where T : System.Enum
    {
        private int value { get; set; }

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        public Flag() { }

        /// <summary>
        /// �t���O�̒ǉ�
        /// </summary>
        /// <param name="_flag">�t���O�w��</param>
        public void AddFlag(T _flag)
        {
            value |= 1 << CastToInt(_flag);
        }

        /// <summary>
        /// �t���O�̍폜
        /// </summary>
        /// <param name="_flag">�t���O�w��</param>
        public void DelFlag(T _flag)
        {
            value &= ~(1 << CastToInt(_flag));
        }

        /// <summary>
        /// �t���O�̐ݒ�
        /// </summary>
        /// <param name="_flag">�t���O�w��</param>
        /// <param name="_value">ON/OFF</param>
        public void SetFlag(T _flag, bool _value)
        {
            if(_value)
            {
                AddFlag(_flag);
            }
            else
            {
                DelFlag(_flag);
            }
        }

        /// <summary>
        /// �t���O�̊m�F
        /// </summary>
        /// <param name="_flag">�t���O�w��</param>
        /// <param name="_value">ON/OFF</param>
        public bool HasFlag(T _flag)
        {
            return (value & (1 << CastToInt(_flag))) > 0;
        }

        /// <summary>
        /// �t���O�̑��݊m�F
        /// </summary>
        /// <param name="_flags">�t���O�w��</param>
        /// <returns>����Βl��Ԃ�</returns>
        public bool HasFlagAny(params T[] _flags)
        {
            return _flags.Any(HasFlag);
        }

        /// <summary>
        /// �t���O�m�F(�m�F��Ƀt���O��܂�)
        /// </summary>
        /// <param name="_flag"></param>
        /// <returns></returns>
        public bool OneTimeFlag(T _flag)
        {
            var temp = HasFlag(_flag);
            DelFlag(_flag);
            return temp;
        }

        /// <summary>
        /// ���ݗL���ȃt���O�����擾����
        /// </summary>
        /// <returns></returns>
        public int Count()
        {
            var count = 0;
            var bitNum = sizeof(int) * 8;
            for (int i = 0; i < bitNum; ++i)
            {
                if (((uint)value & 1 << i) != 0)
                {
                    count++;
                }
            }
            return count;
        }

        /// <summary>
        /// �t���O�����Z�b�g����
        /// </summary>
        public void ResetFlag()
        {
            SetValue(0);
        }

        /// <summary>
        /// �l��ݒ肷��
        /// </summary>
        /// <param name="_value"></param>
        public void SetValue(int _value)
        {
            value = _value;
        }

        /// <summary>
        /// �l���擾����
        /// </summary>
        /// <returns></returns>
        public int GetValue()
        {
            return value;
        }

        public int CastToInt(T _flag)
        {
            return (int)(object)_flag;
        }
    }
}