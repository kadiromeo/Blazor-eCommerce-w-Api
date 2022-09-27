using Blazor_eCommerce_Project.Common;
using Blazor_eCommerce_Project.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blazor_eCommerce_Project.Business.Contracts
{
    public interface ICourseInterface
    {
        public Task<Result<CourseDTO>> CreateCourse(CourseDTO courseDTO);
        public Task<Result<CourseDTO>> UpdateCourse(int courseId,CourseDTO courseDTO);
        public Task<Result<CourseDTO>> GetCourse(int courseId);
        public Task<Result<int>> DeleteCourse(int courseId);
        public Task<Result<IEnumerable<CourseDTO>>> GetAllCourse();

    }
}
