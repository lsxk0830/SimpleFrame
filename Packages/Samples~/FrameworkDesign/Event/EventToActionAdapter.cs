using System;
namespace Blue
{
    /// <summary>
    /// Event Action 适配
    /// </summary>
    public struct EventToActionAdapter
    {
        private Action<IEvent> mAdapterAction;
        private int mHashCode;

        /// <summary>
        /// 初始化 hashCode ，Action
        /// </summary>
        /// <param name="hashCode"></param>
        /// <param name="adapterAction"></param>
        public EventToActionAdapter(int hashCode,Action<IEvent> adapterAction)
        {
            mHashCode = hashCode;
            mAdapterAction = adapterAction;
        }

        /// <summary>
        /// 获取适配的 Action
        /// </summary>
        /// <returns></returns>
        public Action<IEvent> GetAdapterAction()
        {
            return mAdapterAction;
        }

        /// <summary>
        /// 获取 Action 的 HashCode
        /// </summary>
        /// <returns></returns>
        public int GetActionHashCode()
        {
            return mHashCode;
        }

        /// <summary>
        /// 清除 Action
        /// </summary>
        public void Clear()
        {
            mAdapterAction = null;
        }
    }
}
