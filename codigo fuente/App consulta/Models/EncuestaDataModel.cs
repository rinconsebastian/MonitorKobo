﻿

namespace App_consulta.Models
{
    public class EncuestaMap
    {
        public string Id { get; set; }
        public string LocationCode { get; set; }
        public string Datetime { get; set; }
    }

    public class EncuestaDataModel
    {
        public string Id { get; set; }
        public string LocationCode { get; set; }
        public string Dep { get; set; }
        public string Mun { get; set; }
        public string Datetime { get; set; }
    }
}
