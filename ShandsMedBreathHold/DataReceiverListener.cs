using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShandsMedBreathHold
{
    public interface DataReceiverListener
    {
        /// <summary>
        /// 事件监听器接口，当数据改变时用于更新数据
        /// </summary>
        void dataReceived(double value);

        /// <summary>
        /// 事件监听器接口，检测当前串口是否可用
        /// </summary>
        void isOpened(bool value);

        /// <summary>
        /// 事件监听器接口，检测当前串口是否开始接收数据
        /// </summary>
        void isRunning(bool value);
    }
}
