using AutoMapper;
using CourseCatalogService.API.Dtos;
using CourseCatalogService.API.Model;
using CourseCatalogService.API.Settings;
using CourseServiceCatalog.Shared;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace CourseCatalogService.API.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IMongoCollection<Category> _categoryCollection;
        private readonly IMapper _mapper;
        private readonly DatabaseSettings _databaseSettings;

        public CategoryService(IMapper mapper, IDatabaseSettings databaseSettings,
            IOptions<DatabaseSettings> databaseOptions)
        {

            _databaseSettings = databaseOptions.Value;
            var client = new MongoClient(_databaseSettings.ConnectionString);

            var database = client.GetDatabase(_databaseSettings.DatabaseName);

            _categoryCollection = database.GetCollection<Category>(_databaseSettings.CategoryCollectionName);
            _mapper = mapper;
        }


        public async Task<ResponseDto<List<CategoryDto>>> GetAllAsync()
        {
            var categories = await _categoryCollection.Find(category => true).ToListAsync();

            return ResponseDto<List<CategoryDto>>.Success(_mapper.Map<List<CategoryDto>>(categories), 200);
        }

        public async Task<ResponseDto<CategoryDto>> CreateAsync(CategoryDto categoryDto)
        {
            var category = _mapper.Map<Category>(categoryDto);
            await _categoryCollection.InsertOneAsync(category);

            return ResponseDto<CategoryDto>.Success(_mapper.Map<CategoryDto>(category), 200);
        }

        public async Task<ResponseDto<CategoryDto>> GetByIdAsync(string id)
        {
            var category = await _categoryCollection.Find<Category>(x => x.Id == id).FirstOrDefaultAsync();

            if (category == null)
            {
                ResponseDto<CategoryDto>.Fail("Category nor found", 404);
            }

            return ResponseDto<CategoryDto>.Success(_mapper.Map<CategoryDto>(category), 200);
        }
    }
}
