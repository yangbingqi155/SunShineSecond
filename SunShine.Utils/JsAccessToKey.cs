﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunShine.Utils {
    public class JsAccessToKey
    {
        public string ticket { get; set; }

        public int expires_in { get; set; }

        public DateTime expires { get; set; }

    }
}
