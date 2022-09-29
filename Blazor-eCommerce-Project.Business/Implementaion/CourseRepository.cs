using AutoMapper;
using Blazor_eCommerce_Project.Business.Contracts;
using Blazor_eCommerce_Project.Common;
using Blazor_eCommerce_Project.Data.Access.Data;
using Blazor_eCommerce_Project.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazor_eCommerce_Project.Business.Implementaion
{
    public class CourseRepository : ICourseRepository
    {
        private readonly CourseContext _courseContext;
        private readonly IMapper _mapper;
        public CourseRepository (CourseContext courseContext,IMapper mapper)
        {
            _courseContext = courseContext;
            _mapper = mapper;
        }

        public async Task<Result<CourseDTO>> CreateCourse(CourseDTO courseDto)
        {
            var course = _mapper.Map<CourseDTO, Course>(courseDto);
            course.CreatedBy = "Best Codder";
            var addedCourse = await _courseContext.Cources.AddAsync(course);
            await _courseContext.SaveChangesAsync();
            var returnData = _mapper.Map<Course, CourseDTO>(addedCourse.Entity);
            return new Result<CourseDTO>(true, ResultConstant.RecordCreateSuccessfully, returnData);
        }


        public async Task<Result<int>> DeleteCourse(int courseId)
        {
            var courseDetails = await _courseContext.Cources.FindAsync(courseId);
            if (courseDetails!=null)
            {
                _courseContext.Remove(courseDetails);
                var result = await _courseContext.SaveChangesAsync();
                return new Result<int>(true, ResultConstant.RecordRemoveSuccessfully);
            }
             return new Result<int>(true, ResultConstant.RecordRemoveNotSuccessfully);

        }

        public async Task<Result<IEnumerable<CourseDTO>>> GetAllCourse()
        {
            try
            {
                var courseDtos =  _mapper.Map<IEnumerable<Course>, IEnumerable<CourseDTO>>(_courseContext.Cources);
                return new Result<IEnumerable<CourseDTO>>(true, ResultConstant.RecordFound, courseDtos, courseDtos.ToList().Count);

            }
            catch (Exception)
            {

                return new Result<IEnumerable<CourseDTO>>(false, ResultConstant.RecordNotFound);
            }
        }

        public async Task<Result<CourseDTO>> GetCourse(int courseId)
        {
            try
            {
                var data = await _courseContext.Cources.FirstOrDefaultAsync(m => m.Id == courseId);
                var returnData = _mapper.Map<Course, CourseDTO>(data);
                return new Result<CourseDTO>(false, ResultConstant.RecordFound, returnData);
            }
            catch (Exception)
            {
                return new Result<CourseDTO>(false, ResultConstant.RecordNotFound);
            }
        }

        public async Task<Result<CourseDTO>> UpdateCourse(int courseId, CourseDTO courseDto)
        {
            try
            {
                if (courseId== courseDto.Id)
                {
                    var courseDetails = await _courseContext.Cources.FindAsync(courseId);
                    var course = _mapper.Map<CourseDTO, Course>(courseDto, courseDetails);
                    course.UpdatedBy = "Kadir YOLCU";
                    course.UpdatedDate = DateTime.Now;
                    var updateCourse=_courseContext.Update(course);
                    await _courseContext.SaveChangesAsync();
                    var returnData = _mapper.Map<Course, CourseDTO>(updateCourse.Entity);
                    return new Result<CourseDTO>(true, ResultConstant.RecordUpdateSuccessfully, returnData);
                }
                else
                {
                    return new Result<CourseDTO>(false, ResultConstant.RecordUpdateNotSuccessfully);
                }
            }
            catch (Exception)
            {

                return new Result<CourseDTO>(false, ResultConstant.RecordUpdateNotSuccessfully);

            }
        }

        public async Task<Result<CourseDTO>> UpdateCourseImage(int courseId, string imagePath)
        {
            try
            {
                if (courseId >0)
                {
                    var courseDetails = await _courseContext.Cources.FindAsync(courseId);
                    courseDetails.UpdatedBy = "Kadir YOLCU";
                    courseDetails.UpdatedDate = DateTime.Now;
                    courseDetails.ImgUrl = imagePath;
                    var updateCourse = _courseContext.Update(courseDetails);
                    await _courseContext.SaveChangesAsync();
                    var returnData = _mapper.Map<Course, CourseDTO>(updateCourse.Entity);
                    return new Result<CourseDTO>(true, ResultConstant.RecordUpdateSuccessfully, returnData);
                }
                else
                {
                    return new Result<CourseDTO>(false, ResultConstant.RecordUpdateNotSuccessfully);
                }
            }
            catch (Exception)
            {

                return new Result<CourseDTO>(false, ResultConstant.RecordUpdateNotSuccessfully);

            }

        }
    }
}
