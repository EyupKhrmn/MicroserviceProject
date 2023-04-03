using AutoMapper;
using CourseCatalogService.API.Dtos;
using CourseCatalogService.API.Model;
using CourseCatalogService.API.Settings;
using CourseServiceCatalog.Shares;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using NoContent = Microsoft.AspNetCore.Http.HttpResults.NoContent;

namespace CourseCatalogService.API.Services
{
    public class CourseService : ICourseService
    {
        private readonly IMapper _mapper;
        private readonly IMongoCollection<Course> _courseCollection;
        private readonly IMongoCollection<Category> _categoryCollection;
        private readonly DatabaseSettings _databaseSettings;

        public CourseService(IMapper mapper, IDatabaseSettings databaseSettings, IMongoCollection<Category> categoryCollection, IOptions<DatabaseSettings> databaseOptions)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);

            var database = client.GetDatabase(databaseSettings.DatabaseName);

            _mapper = mapper;
            _databaseSettings = databaseOptions.Value;
            _categoryCollection = database.GetCollection<Category>(_databaseSettings.CategoryCollectionName);
            _courseCollection = database.GetCollection<Course>(_databaseSettings.CourseCollectionName);
        }

        public async Task<ResponseDto<List<CourseDto>>> GetAllAsync()
        {
            var courses = await _courseCollection.Find(course => true).ToListAsync();

            if (courses.Any())
            {
                foreach (var course in courses)
                {
                    course.Category = await _categoryCollection.Find<Category>(x => x.Id == course.CategoryId)
                        .FirstOrDefaultAsync();
                }
            }
            else
            {
                courses = new List<Course>();
            }

            return ResponseDto<List<CourseDto>>.Success(_mapper.Map<List<CourseDto>>(courses), 200);

        }

        public async Task<ResponseDto<CourseDto>> GetByIdAsync(string id)
        {
            var course = await _courseCollection.Find<Course>(x => x.Id == id).FirstOrDefaultAsync();
            if (course == null)
            {
                return ResponseDto<CourseDto>.Fail("Course not Found", 404);
            }

            course.Category =
                await _categoryCollection.Find<Category>(x => x.Id == course.CategoryId).FirstOrDefaultAsync();

            return ResponseDto<CourseDto>.Success(_mapper.Map<CourseDto>(course), 200);
        }

        public async Task<ResponseDto<List<CourseDto>>> GetAllByUserId(string id)
        {
            var courses = await _courseCollection.Find<Course>(x => x.UserId == id).ToListAsync();

            if (!courses.Any())
            {
                return ResponseDto<List<CourseDto>>.Fail("User course not found", 404);
            }

            if (courses.Any())
            {
                foreach (var course in courses)
                {
                    course.Category = await _categoryCollection.Find<Category>(x => x.Id == course.CategoryId)
                        .FirstOrDefaultAsync();
                }
            }
            else
            {
                courses = new List<Course>();
            }

            return ResponseDto<List<CourseDto>>.Success(_mapper.Map<List<CourseDto>>(courses), 200);



        }

        public async Task<ResponseDto<CourseDto>> CreateAsync(CreateCourseDto createCourseDto)
        {
            var newCourse = _mapper.Map<Course>(createCourseDto);

            newCourse.CreatedDate = DateTime.Now;
            await _courseCollection.InsertOneAsync(newCourse);

            return ResponseDto<CourseDto>.Success(_mapper.Map<CourseDto>(newCourse), 200);
        }

        public async Task<ResponseDto<NoContent>> UpdateAsync(UpdateCourseDto updateCourseDto)
        {
            var updateCourse = _mapper.Map<Course>(updateCourseDto);

            var result = await _courseCollection.FindOneAndReplaceAsync(x => x.Id == updateCourseDto.CourseId, updateCourse);

            if (result == null)
            {
                return ResponseDto<NoContent>.Fail("Course not found", 404);
            }

            return ResponseDto<NoContent>.Success(204);
        }

        public async Task<ResponseDto<NoContent>> DeleteAsync(string id)
        {
            var course = await _courseCollection.DeleteOneAsync(x => x.Id == id);
            if (course.DeletedCount > 0)
            {
                return ResponseDto<NoContent>.Success(204);
            }
            else
            {
                return ResponseDto<NoContent>.Fail("Course not found", 404);
            }


        }
    }
}
