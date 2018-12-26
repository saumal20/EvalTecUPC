using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EvalTecUPC.Models;
using Oracle.ManagedDataAccess;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using System.Data.SqlClient;

namespace EvalTecUPC.Controllers
{
    public class AlumnoSolicitudController : Controller
    {
        // GET: AlumnoSolicitud
        public ActionResult Index()
        {
            SolucitudesContext db = new SolucitudesContext();
            return View(db.SOLICITUD.ToList());
        }

        // GET: AlumnoSolicitud/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        public ActionResult Consultar()
        {
            return View();
        }


        //[HttpPost]
        public ActionResult ConsultarResultado(SOLICITUD objSolicitud)
        {
            try
            {
                SolucitudesContext db = new SolucitudesContext();
                List<AlumnoSolicitudViewModel> alumVML = new List<AlumnoSolicitudViewModel>();
                AlumnoSolicitudViewModel alumVM = new AlumnoSolicitudViewModel();
                int opcion = 1; // si es 1 busca por SP sino por EF


                if (opcion == 1)
                {
                    DataTable dt = new DataTable();
                    OracleDataReader objReader; //cmd.ExecuteReader();

                    using (OracleConnection objConn = new OracleConnection("Data Source=localhost:1521/orcl.11g; User ID=hr; Password=hr"))
                    {
                        OracleCommand cmd = new OracleCommand();
                        cmd.Connection = objConn;
                        cmd.CommandText = "SP_ReadSolicitudDetalle";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("COD_LINEA_NEGOCIO_ex", OracleDbType.Char).Value = objSolicitud.COD_LINEA_NEGOCIO;
                        cmd.Parameters.Add("COD_MODAL_EST_ex", OracleDbType.Char).Value = objSolicitud.COD_MODAL_EST;
                        cmd.Parameters.Add("COD_PERIODO_ex", OracleDbType.Char).Value = objSolicitud.COD_PERIODO;
                        cmd.Parameters.Add("COD_TRAMITE_ex", OracleDbType.Int32).Value = Convert.ToInt32(objSolicitud.COD_TRAMITE);
                        cmd.Parameters.Add("ESTADO_ex", OracleDbType.Varchar2).Value = objSolicitud.ESTADO;
                        cmd.Parameters.Add("CUR_ObtieneSolicitudDetalle", OracleDbType.RefCursor, ParameterDirection.Output);

                        try
                        {
                            objConn.Open();
                            objReader = cmd.ExecuteReader();
                            if (objReader != null)
                            {
                                while (objReader.Read())
                                {
                                    alumVM.COD_ALUMNO = objReader["COD_ALUMNO"].ToString();
                                    alumVM.COD_LINEA_NEGOCIO = objReader["COD_LINEA_NEGOCIO"].ToString();
                                    alumVM.COD_MODAL_EST = objReader["COD_MODAL_EST"].ToString();
                                    alumVM.COD_TRAMITE = Convert.ToInt16(objReader["COD_TRAMITE"]);
                                    alumVM.COD_MODAL_EST = objReader["COD_MODAL_EST"].ToString();
                                    alumVM.COD_UNICO = Convert.ToInt32(objReader["COD_UNICO"].ToString());
                                    alumVM.ESTADO = objReader["ESTADO"].ToString();
                                    alumVM.COD_PERIODO = objReader["COD_PERIODO"].ToString();
                                    alumVM.NUM_PRUEBA = Convert.ToInt16(objReader["NUM_PRUEBA"]);
                                    alumVM.COD_TIPO_PRUEBA = objReader["COD_TIPO_PRUEBA"].ToString();
                                    alumVM.COD_CURSO = objReader["COD_CURSO"].ToString();
                                    alumVM.COD_DETALLE = Convert.ToInt32(objReader["COD_DETALLE"].ToString());
                                    alumVM.GRUPO = objReader["GRUPO"].ToString();
                                    alumVM.SECCION = objReader["SECCION"].ToString();
                                    alumVM.ESTADO_CURSO = objReader["ESTADO_CURSO"].ToString();
                                    alumVML.Add(alumVM);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            throw;
                        }

                        objConn.Close();
                    }
                }
                else
                {
                    var AlumnoSolicitudes = db.SOLICITUD.Include("DETALLE_SOLICITUD").Where(p => p.COD_LINEA_NEGOCIO == objSolicitud.COD_LINEA_NEGOCIO &&
                                        p.COD_MODAL_EST == objSolicitud.COD_MODAL_EST &&
                                        p.COD_PERIODO == objSolicitud.COD_PERIODO &&
                                        p.COD_TRAMITE == objSolicitud.COD_TRAMITE &&
                                        p.ESTADO == objSolicitud.ESTADO).ToList();

                    foreach (var item in AlumnoSolicitudes)
                    {
                        alumVM.COD_ALUMNO = item.COD_ALUMNO;
                        alumVM.COD_LINEA_NEGOCIO = item.COD_LINEA_NEGOCIO;
                        alumVM.COD_MODAL_EST = item.COD_MODAL_EST;
                        alumVM.COD_TRAMITE = item.COD_TRAMITE;
                        alumVM.COD_MODAL_EST = item.ESTADO;
                        alumVM.COD_UNICO = item.COD_UNICO;
                        alumVM.ESTADO = item.ESTADO;
                        alumVM.COD_PERIODO = item.COD_PERIODO;

                        foreach (var item2 in item.DETALLE_SOLICITUD)
                        {
                            alumVM.NUM_PRUEBA = item2.NUM_PRUEBA;
                            alumVM.COD_TIPO_PRUEBA = item2.COD_TIPO_PRUEBA;
                            alumVM.COD_CURSO = item2.COD_CURSO;
                            alumVM.COD_DETALLE = item2.COD_DETALLE;
                            alumVM.GRUPO = item2.GRUPO;
                            alumVM.SECCION = item2.SECCION;
                            alumVM.ESTADO_CURSO = item2.ESTADO_CURSO;
                        }

                        alumVML.Add(alumVM);
                    }
                }

                return PartialView(alumVML);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
