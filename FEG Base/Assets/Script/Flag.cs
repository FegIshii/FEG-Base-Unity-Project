using System.Linq;

namespace Common
{
    public class Flag<T> where T : System.Enum
    {
        private int value { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public Flag() { }

        /// <summary>
        /// フラグの追加
        /// </summary>
        /// <param name="_flag">フラグ指定</param>
        public void AddFlag(T _flag)
        {
            value |= 1 << CastToInt(_flag);
        }

        /// <summary>
        /// フラグの削除
        /// </summary>
        /// <param name="_flag">フラグ指定</param>
        public void DelFlag(T _flag)
        {
            value &= ~(1 << CastToInt(_flag));
        }

        /// <summary>
        /// フラグの設定
        /// </summary>
        /// <param name="_flag">フラグ指定</param>
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
        /// フラグの確認
        /// </summary>
        /// <param name="_flag">フラグ指定</param>
        /// <param name="_value">ON/OFF</param>
        public bool HasFlag(T _flag)
        {
            return (value & (1 << CastToInt(_flag))) > 0;
        }

        /// <summary>
        /// フラグの存在確認
        /// </summary>
        /// <param name="_flags">フラグ指定</param>
        /// <returns>あれば値を返す</returns>
        public bool HasFlagAny(params T[] _flags)
        {
            return _flags.Any(HasFlag);
        }

        /// <summary>
        /// フラグ確認(確認後にフラグを折る)
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
        /// 現在有効なフラグ数を取得する
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
        /// フラグをリセットする
        /// </summary>
        public void ResetFlag()
        {
            SetValue(0);
        }

        /// <summary>
        /// 値を設定する
        /// </summary>
        /// <param name="_value"></param>
        public void SetValue(int _value)
        {
            value = _value;
        }

        /// <summary>
        /// 値を取得する
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