using Blazor_eCommerce_Project.Common;
using Blazor_eCommerce_Project.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blazor_eCommerce_Project.Business.Contracts
{
    public interface ICourseRepository
    {
        public Task<Result<CourseDTO>> CreateCourse(CourseDTO courseDto);
        public Task<Result<CourseDTO>> UpdateCourse(int courseId,CourseDTO courseDto);
        public Task<Result<CourseDTO>> UpdateCourseImage(int courseId, string imagePath);
        public Task<Result<CourseDTO>> GetCourse(int courseId);
        public Task<Result<int>> DeleteCourse(int courseId);
        public Task<Result<IEnumerable<CourseDTO>>> GetAllCourse();

    }
}
