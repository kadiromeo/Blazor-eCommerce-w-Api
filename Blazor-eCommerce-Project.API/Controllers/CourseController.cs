using Blazor_eCommerce_Project.Business.Contracts;
using Blazor_eCommerce_Project.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Blazor_eCommerce_Project.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class CourseController : Controller
    {
        private readonly ICourseRepository _courseRepository;

        public CourseController(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetCourses()
        {
            var allcourse = await _courseRepository.GetAllCourse();
            var data = allcourse;
            return Ok(data.Data);
        }

        [HttpGet("{courseId}")]
        public async Task<IActionResult> GetCourse(int? courseId)
        {
            if (courseId is null)

                return BadRequest(new Result<IActionResult>(false, ResultConstant.IdNotNull));
            var courseData = await _courseRepository.GetCourse((int)courseId);
            if (courseData != null)
                return Ok(courseData.Data);
            else
                return BadRequest(new Result<IActionResult>(false, ResultConstant.RecordNotFound));

        }

    }
}
