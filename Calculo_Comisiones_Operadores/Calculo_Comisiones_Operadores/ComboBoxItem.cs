﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Calculo_Comisiones_Operadores
{
    public class ComboBoxItem
    {
        public string Text { get; set; }
        public object Value { get; set; }

        public override string ToString()
        {
            return Text;
        }
    }
}