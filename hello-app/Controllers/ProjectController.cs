using Google.Apis.Bigquery.v2.Data;
using Google.Cloud.BigQuery.V2;
using hello_app.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using System.Xml.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace hello_app.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class ProjectController : ControllerBase
    {

        //public static List<Employee> EmployeeList()
        //{
        //    return new List<Employee>
        //    {
        //        { new Employee(){EmployeeId=1,Name="Walt"} },
        //        { new Employee(){EmployeeId=2,Name="Anna"} }
        //    };
        //}

        private readonly ILogger<ProjectController> _logger;

        public ProjectController(ILogger<ProjectController> logger)
        {
            _logger = logger;
        }

        [SwaggerOperation(Summary = "Returns project information as per project id")]
        [HttpGet(Name = "GetProject")]
        public IActionResult Get(string projectId)
        {
            //List<Employee> employees = EmployeeList();
            //var subset = from theEmployee in employees
            //             where theEmployee.EmployeeId == employeeId
            //             select theEmployee;
            //return subset;
            BigQueryClient client = BigQueryClient.Create("sbx-1033-nextgen-f7c2001f");
            BigQueryTable table = client.GetTable("sbx-1033-nextgen-f7c2001f", "sbx-1033-nextgen-f7c2001f.Test_NextGen_DS", "sbx-1033-nextgen-f7c2001f.Test_NextGen_DS.Test_NextGen_Tb");

            string sql = $"SELECT * FROM {table} where Project_Id={projectId}";
            BigQueryParameter[] parameters = null;
            BigQueryResults results = client.ExecuteQuery(sql, parameters);

            return Ok(results);
            //ProjectModelContainer context = new ProjectModelContainer();
            //var query = context.ProjectSet.Where(x => x.ProjectId == projectId);
            //return (query);
        }

        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Project
        ///     {
        ///        "ProjectId": "1",
        ///        "ProjectName": "Blade"
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response>
        [SwaggerOperation(Summary = "Insert project information")]
        [HttpPost(Name = "PostProject")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Post([FromBody] Project project)
        {
            BigQueryClient client = BigQueryClient.Create("sbx-1033-nextgen-f7c2001f");
            BigQueryDataset dataset = client.GetDataset("sbx-1033-nextgen-f7c2001f.Test_NextGen_DS");
            BigQueryTable table = dataset.GetTable("sbx-1033-nextgen-f7c2001f.Test_NextGen_DS.Test_NextGen_Tb");
            table.InsertRow(new BigQueryInsertRow
            {
                {"Project_Id",project.ProjectId },
                {"Project_Name",project.ProjectName }
            });
            return Ok();
            //ProjectModelContainer context = new ProjectModelContainer();
            //context.ProjectSet.Add(project);
            //try
            //{
            //    context.SaveChanges();
            //}
            //catch (DbUpdateConcurrencyException)
            //{
            //    throw new DbUpdateConcurrencyException();
            //}
            //return Ok(project);
        }
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /Project
        ///     {
        ///        "ProjectId": "1",
        ///        "ProjectName": "Blade"
        ///     }
        ///
        /// </remarks>

        [SwaggerOperation(Summary = "Update project information")]
        [HttpPut(Name = "PutProject")]
        public IActionResult Put([FromBody] Project project)
        {
            BigQueryClient client = BigQueryClient.Create("sbx-1033-nextgen-f7c2001f");
            BigQueryTable table = client.GetTable("sbx-1033-nextgen-f7c2001f.Test_NextGen_DS", "sbx-1033-nextgen-f7c2001f.Test_NextGen_DS.Test_NextGen_Tb");
            BigQueryResults result = client.ExecuteQuery(
                    $"UPDATE {table} SET Project_Name = {project.ProjectName} WHERE Project_Id = @projectid", new[] { new BigQueryParameter("projectid", BigQueryDbType.String, project.ProjectId) }).ThrowOnAnyError();
            return Ok(result);
            //ProjectModelContainer context = new ProjectModelContainer();
            //context.ProjectSet.Attach(project);
            //context.Entry(project).State = EntityState.Modified;
            //try
            //{
            //    context.SaveChanges();
            //}
            //catch (DbUpdateConcurrencyException)
            //{
            //    throw new DbUpdateConcurrencyException();
            //}
            //return Ok(project);
        }

        [SwaggerOperation(Summary = "Delete project information")]
        [HttpDelete(Name = "DeleteProject")]
        public IActionResult Delete(string projectid)
        {
            BigQueryClient client = BigQueryClient.Create("sbx-1033-nextgen-f7c2001f");
            BigQueryTable table = client.GetTable("sbx-1033-nextgen-f7c2001f.Test_NextGen_DS", "sbx-1033-nextgen-f7c2001f.Test_NextGen_DS.Test_NextGen_Tb");
            BigQueryResults result = client.ExecuteQuery(
                    $"Delete from {table} WHERE Project_Id = @projectid", new[] { new BigQueryParameter("projectid", BigQueryDbType.String, projectid) }).ThrowOnAnyError();
            return Ok();
            //ProjectModelContainer context = new ProjectModelContainer();
            //var todelete = context.ProjectSet.FirstOrDefault(c => c.ProjectId == projectid);
            //context.ProjectSet.Remove(todelete);
            //context.Entry(todelete).State = EntityState.Deleted;


            //try
            //{
            //    context.SaveChanges();
            //}
            //catch (DbUpdateConcurrencyException)
            //{
            //    throw new DbUpdateConcurrencyException();
            //}
            //return Ok();

        }
    }
}