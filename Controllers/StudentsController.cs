using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApicrud.Data;
using WebApicrud.Models;

namespace WebApicrud.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly ApiDbContext _db;
        //create constructor ctor +tab key
        public StudentsController(ApiDbContext db)
        {
            _db = db;
        }
        //--------------------------
        //show all data
        [HttpGet]
        public IActionResult GetAll()
        {//try + tab tab 2time tab key press
            try
            {
                IList<Students> stdlist = _db.Students.ToList();
                return Ok(stdlist);
            }
            catch (Exception ex)
            {

                return StatusCode(500, ex);
            }
        }
        //----------------

        //get specific data only like example for whose id=2

        [HttpGet("{id}")]  //we pass id value from uri
        public IActionResult Get(int id)
        {
            try
            {
                if (id == null)
                {
                    return StatusCode(500, "null id");
                }
                else
                {
                    var gotstudents = _db.Students.Find(id);
                    return Ok(gotstudents);
                }
            }
            catch (Exception ex)
            {

                return StatusCode(500, ex);
            }
        }

        //---------------------
        //update by id
        [HttpPut("id")]
        public IActionResult update(int id, [FromBody] Students obj)
        {
            try
            {
                if (obj.Id == null)
                {
                    return StatusCode(500, "id null");
                }
                else
                {
                    Students obj1 = new Students()
                    {
                        Id = id,
                        Name = obj.Name
                    };
                    _db.Students.Update(obj1);
                    _db.SaveChanges();
                    return Ok(obj1);
                }
            }
            catch (Exception ex)
            {

                return StatusCode(500, ex);
            }
        }
        //---------------------------
        //add student
        [HttpPost]
        public IActionResult Post(Students obj) 
        {
            try
            {
                if (obj.Name==null)
                {
                    return StatusCode(500, "name null");
                }
                else
                {
                    _db.Students.Add(obj);
                    _db.SaveChanges();
                    return Ok(obj);
                }
            }
            catch (Exception ex)
            {

                return StatusCode(500, ex);
            }
        }
        //------------------------------------
        //delete by id
        [HttpDelete("id")]
        public IActionResult Delete(int id)
        {
            try
            {
                if (id==null)
                {
                    return StatusCode(500, "null not allowed");
                }
                else
                {
                    var gstd=_db.Students.FirstOrDefault(x => x.Id == id);
                    _db.Students.Remove(gstd);
                    _db.SaveChanges();
                    return Ok(gstd);
                }
            }
            catch (Exception ex)
            {

                return StatusCode(500, ex);
            }
        }
    }
}
