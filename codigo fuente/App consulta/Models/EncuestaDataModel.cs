

namespace App_consulta.Models
{
    public class EncuestaMap
    {
        public string IdKobo { get; set; }
        public string User { get; set; }
        public string LocationCode { get; set; }
        public string Datetime { get; set; }
        public string Validation { get; set; }
    }

    public class EncuestaDataModel
    {
        public string IdKobo { get; set; }
        public string User { get; set; }
        public string LocationCode { get; set; }
        public string Dep { get; set; }
        public string Mun { get; set; }
        public string Datetime { get; set; }
        public bool Validation { get; set; }
    }
}
