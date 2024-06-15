using UnityEngine;
using SimpleFrame;

namespace SimpleFrameTest
{
    public class BindablePropertyTest : MonoBehaviour
    {
        private BindableProperty<int> bindablePropertyInt = new BindableProperty<int>();
        private BindableProperty<string> bindablePropertyStr;
        void Start()
        {
            bindablePropertyInt.mOnValueChanged = newInt =>
            {
                Debug.Log($"新的值：{newInt}");
            };
            bindablePropertyStr = new BindableProperty<string>("InitStr")
            {
                mOnValueChanged = newStr =>
                {
                    Debug.Log($"新的字符串：{newStr}");
                }
            };
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                bindablePropertyInt.Value = 1;
                bindablePropertyStr.Value = "Q";
            }
            if (Input.GetKeyDown(KeyCode.W))
            {
                bindablePropertyInt.Value = 2;
                bindablePropertyStr.Value = "W";
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                bindablePropertyInt.Value = 3;
                bindablePropertyStr.Value = "E";
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                bindablePropertyInt.Value = 4;
                bindablePropertyStr.Value = "R";
            }
            if (Input.GetKeyDown(KeyCode.T))
            {
                bindablePropertyInt.Value = 5;
                bindablePropertyStr.Value = "T";
            }
        }
    }
}