using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ContosoWebAPI.Models;

namespace ContosoWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly ContosouniversityContext _context;

        public CoursesController(ContosouniversityContext context)
        {
            _context = context;
        }

        // GET: api/Courses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Course>>> GetCourse()
        {
            return await _context.Course.Where(x=>x.IsDeleted == false).ToListAsync();
        }
        [HttpGet("Department")]
        public async Task<ActionResult<IEnumerable<Department>>> GetDepartment()
        {
            return await _context.Department.Where(x => x.IsDeleted == false).ToListAsync();
        }
        [HttpGet("Person")]
        public async Task<ActionResult<IEnumerable<Person>>> GetPerson()
        {
            return await _context.Person.Where(x => x.IsDeleted == false).ToListAsync();
        }
        // GET: api/Courses/vwCourseStudents
        [HttpGet("vwCourseStudents")]
        public async Task<ActionResult<IEnumerable<VwCourseStudents>>> GetvwCourseStudents()
        {
            return await _context.VwCourseStudents.ToListAsync();
        }
        // GET: api/Courses/vwCourseStudentCount
        [HttpGet("vwCourseStudentCount")]
        public async Task<ActionResult<IEnumerable<VwCourseStudentCount>>> GetvwCourseStudentCount()
        {
            return await _context.VwCourseStudentCount.ToListAsync();
        }
        // GET: api/Courses/vwCourseStudentCount
        [HttpGet("vwDepartmentCourseCount ")]
        public async Task<ActionResult<IEnumerable<VwDepartmentCourseCount>>> GetvwDepartmentCourseCount()
        {
            var vwDepartmentCourseCount = await _context.VwDepartmentCourseCount
                    .FromSqlRaw("SELECT * FROM dbo.VwDepartmentCourseCount")
                    .ToListAsync();
            return vwDepartmentCourseCount;
        }
        // GET: api/Courses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Course>> GetCourse(int id)
        {
            var course = await _context.Course.FindAsync(id);

            if (course == null)
            {
                return NotFound();
            }

            return course;
        }

        // PUT: api/Courses/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCourse(int id, Course course)
        {
            if (id != course.CourseId)
            {
                return BadRequest();
            }
            course.DateModified = DateTime.Now;
            _context.Update(course);
           // _context.Entry(course).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CourseExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
        [HttpPut("Department/{id}")]
        public async Task<IActionResult> PutDepartment(int id, Department department)
        {
            if (id != department.DepartmentId)
            {
                return BadRequest();
            }
            department.DateModified = DateTime.Now;
            _context.Update(department);
            // _context.Entry(course).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CourseExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
        [HttpPut("Person/{id}")]
        public async Task<IActionResult> PutPerson(int id, Person person)
        {
            if (id != person.Id)
            {
                return BadRequest();
            }
            person.DateModified = DateTime.Now;
            _context.Update(person);
            // _context.Entry(course).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CourseExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
        // POST: api/Courses
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Course>> PostCourse(Course course)
        {
            _context.Course.Add(course);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCourse", new { id = course.CourseId }, course);
        }

        // DELETE: api/Courses/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Course>> DeleteCourse(int id)
        {
            var course = await _context.Course.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }

            course.IsDeleted = true;
            _context.Update(course);
            await _context.SaveChangesAsync();

            return course;
        }
        [HttpDelete("Department/{id}")]
        public async Task<ActionResult<Department>> DeleteDepartment(int id)
        {
            var department = await _context.Department.FindAsync(id);
            if (department == null)
            {
                return NotFound();
            }

            department.IsDeleted = true;
            _context.Update(department);
            await _context.SaveChangesAsync();

            return department;
        }
        [HttpDelete("Person/{id}")]
        public async Task<ActionResult<Person>> DeletePerson(int id)
        {
            var person = await _context.Person.FindAsync(id);
            if (person == null)
            {
                return NotFound();
            }

            person.IsDeleted = true;
            _context.Update(person);
            await _context.SaveChangesAsync();

            return person;
        }
        private bool CourseExists(int id)
        {
            return _context.Course.Any(e => e.CourseId == id);
        }
    }
}
