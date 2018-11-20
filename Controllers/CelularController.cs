using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using CrudComAngularJsWebApi.Models;
using NHibernate;
using NHibernate.Linq;

namespace CrudComAngularJsWebApi.Controllers
{


    [RoutePrefix("api/v1/public")]
    public class CelularController : ApiController
    {

        //NHibernate Session  
        ISession session = OpenSessionNHibernate.OpenSession();


        [HttpGet]
        [Route("celulares")]
        public List<Celular> ObterCelulares()
        {
            List<Celular> celular = session.Query<Celular>().ToList();
            return celular;
        }

        [HttpGet]
        [Route("celular/{id:int}")]
        public Celular ObterPorId(int id)
        {
            var employee = session.Get<Celular>(id);
            return employee;
        }

        [HttpPut]
        [Route("putcelular")]
        public HttpResponseMessage Alterar(Celular celular)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var emp = session.Get<Celular>(celular.Id);
                    emp.Marca = celular.Marca;
                    emp.MemoriaInterna = celular.MemoriaInterna;
                    emp.Modelo = celular.Modelo;
                    emp.TipoChip = celular.TipoChip;
                    emp.Cor = celular.Cor;

                    using (ITransaction transaction = session.BeginTransaction())
                    {
                        session.Save(emp);
                        transaction.Commit();
                    }
                    return Request.CreateResponse(HttpStatusCode.OK, "Success");
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Error !");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpPost]
        [Route("postcelular")]
        public HttpResponseMessage Incluir(Celular celular)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (ITransaction transaction = session.BeginTransaction())
                    {
                        session.Save(celular);
                        transaction.Commit();
                    }
                    return Request.CreateResponse(HttpStatusCode.OK, "Success");
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Error !");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpDelete]
        [Route("deletecelular/{id:int}")]
        public HttpResponseMessage Excluir(int id)
        {
            try
            {
                var celular = session.Get<Celular>(id);
                if (celular != null)
                {
                    using (ITransaction transaction = session.BeginTransaction())
                    {
                        session.Delete(celular);
                        transaction.Commit();
                    }
                    return Request.CreateResponse(HttpStatusCode.OK, "Success");
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Error !");
                }
            }
            catch (Exception ex)
            {

                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}

