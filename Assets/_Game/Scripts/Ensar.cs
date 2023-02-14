using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Aezakmi
{
    public enum Religion
    { Christianity, Islam, Buddhism, Hinduism, Agnosticism, Judaism, Atheism, }

    public class Ensar : MonoBehaviour
    {
        public uint age = 13;
        public Religion religion = Religion.Islam;
    }
}
