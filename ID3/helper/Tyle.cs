using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ID3.helper
{
    public class Tyle
    {
        public int precipitationId;
        public int countElement;
        public float tanxuat;

        public Tyle(int precipitationId, int countElement, float tanxuat)
        {
            this.precipitationId = precipitationId;
            this.countElement = countElement;
            this.tanxuat = tanxuat;
        }

    }
}