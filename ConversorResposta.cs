using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConversorDeMoedas
{
    public class ConversorResposta
    {
            public string result { get; set; } = string.Empty;
            public decimal conversion_rate { get; set; } = 0;
            public decimal conversion_result { get; set; } = 0;
            public string error_type { get; set; } = string.Empty;

    }
}
