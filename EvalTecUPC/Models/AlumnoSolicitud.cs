﻿//------------------------------------------------------------------------------
// <auto-generated>
//    Este código se generó a partir de una plantilla.
//
//    Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//    Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EvalTecUPC.Models
{
    using System;
    using System.Collections.Generic;

    public partial class AlumnoSolicitudViewModel
    {
        //public AlumnoSolicitudViewModel(List<SOLICITUD> solicitudes, DETALLE_SOLICITUD detalles_solicitudes)
        //{
        //    this.solicitudes = solicitudes;
        //    this.detalles_solicitudes = detalles_solicitudes;
        //}

        //public List<SOLICITUD> solicitudes { get; private set; }
        //public DETALLE_SOLICITUD detalles_solicitudes { get; private set; }

        public long COD_DETALLE { get; set; }
        public string COD_CURSO { get; set; }
        public string COD_TIPO_PRUEBA { get; set; }
        public Nullable<short> NUM_PRUEBA { get; set; }
        public string SECCION { get; set; }
        public string GRUPO { get; set; }
        public string ESTADO_CURSO { get; set; }


        public string COD_LINEA_NEGOCIO { get; set; }
        public string COD_MODAL_EST { get; set; }
        public string COD_PERIODO { get; set; }
        public Nullable<short> COD_TRAMITE { get; set; }
        public long COD_UNICO { get; set; }
        public string ESTADO { get; set; }
        public string COD_ALUMNO { get; set; }
    }
}