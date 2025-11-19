using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Login.fomrs
{
    internal class Modelos
    {
    }
    public class PlaneacionModelo
    {
        public int Id { get; set; }
        public string TituloProyecto { get; set; }
        public string CampoFormativo { get; set; }
        public string Contenido { get; set; }
        public string ProcesoAprendizaje { get; set; }
        public string Metodologia { get; set; }
        public string Momento { get; set; }
        public string ActividadesInicio { get; set; }
        public string Evaluacion { get; set; }
        public string Materiales { get; set; }
        public DateTime FechaPractica { get; set; }
    }
}
