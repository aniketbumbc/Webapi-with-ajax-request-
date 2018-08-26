using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EmpInfo;
using EmployeeDataAcess;

namespace EmpInfo.Controllers
{
    public class EmployessController : ApiController
    {
        public  HttpResponseMessage Get(string gender ="All")
        {
            using (EmployeeDBEntities entites = new EmployeeDBEntities())
            {
                switch(gender.ToLower())
                {
                    case "all":
                        return Request.CreateResponse(HttpStatusCode.OK, entites.Employees.ToList());                        
                    case "male":
                        return Request.CreateResponse(HttpStatusCode.OK,entites.Employees.Where
                            (e => e.Gender.ToLower() == "male").ToList());                        
                    case "female":
                        return Request.CreateResponse(HttpStatusCode.OK,entites.Employees.Where
                            (e => e.Gender.ToLower() == "female").ToList());                     
                    default:
                         return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Please check gender");
                        
                }
            }
        }

        [HttpGet]
        public HttpResponseMessage EmployeebyId(int id)
        {
            using (EmployeeDBEntities entites = new EmployeeDBEntities())
            {
                var entity = entites.Employees.FirstOrDefault(e => e.ID == id);
                if(entity != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, entity);
                }else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Not Found");
                }
            }
        }

        [HttpPost]
        public HttpResponseMessage SendEmployee([FromBody]Employee employee)
        {
            using (EmployeeDBEntities entites = new EmployeeDBEntities())
            {
                try
                {
                    entites.Employees.Add(employee);
                    entites.SaveChanges();
                    var message = Request.CreateResponse(HttpStatusCode.Created, employee);
                    message.Headers.Location = new Uri(Request.RequestUri + employee.ID.ToString());
                    return message;
                }
                catch (Exception ex)
                {
                   return Request.CreateErrorResponse(HttpStatusCode.BadRequest,ex);
                }

            }
        }

        [HttpDelete]
        public HttpResponseMessage RemoveEmlpoyee(int id)
        {
            try
            {
                using (EmployeeDBEntities entites = new EmployeeDBEntities())
                {
                    var entity = entites.Employees.Remove(entites.Employees.FirstOrDefault(e => e.ID == id));
                    if (entity != null)
                    {
                        entites.Employees.Remove(entity);
                        entites.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK, entity);
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Not Found");
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
        [HttpPut]
        public HttpResponseMessage UpdateEmlpoyee(int id,Employee employee)
        {
            try
            {
                using (EmployeeDBEntities entites = new EmployeeDBEntities())
                {
                    var entity = entites.Employees.Remove(entites.Employees.FirstOrDefault(e => e.ID == id));
                    entity.FirstName = employee.FirstName;
                    entity.LastName = employee.LastName;
                    entity.Gender = employee.Gender;
                    entity.Salary = employee.Salary;

                    if (entity != null)
                    {
                     
                        entites.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK, entity);
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Not Found");
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}
