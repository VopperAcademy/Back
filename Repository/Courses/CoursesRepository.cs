using System.Net;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using vopperAcademyBackEnd.Data;
using vopperAcademyBackEnd.Models;
using vopperAcademyBackEnd.Models.DTOs;
using vopperAcademyBackEnd.Models.DTOs.Response.Courses;
using vopperAcademyBackEnd.Models.DTOs.Response.Platforms;

namespace vopperAcademyBackEnd.Repository.Courses;

public class CoursesRepository : ICoursesRepository
{
    private readonly MongoDbContext _context;

    public CoursesRepository(MongoDbContext context)
    {
        _context = context;
    }

    public async Task<DynamicResponse<List<PreviewCourseResponseDTO>>> GetAllCoursesAsync()
    {
        try
        {
            var projection = Builders<Course>.Projection.Expression(c => new PreviewCourseResponseDTO
            {
                Id = c.Id!,
                Title = c.Title,
                Categories = c.Categories,
                Teacher = c.Teacher,
                ImageUrl = c.ImageUrl
            });
            
            var courses = await _context.Courses.Find(new BsonDocument()).Project(projection).ToListAsync();

            if (courses.Count <= 0)
            {
                return DynamicResponse<List<PreviewCourseResponseDTO>>.CreateError("Cursos no encontrados.", 404);
            }
            var orderCourses = courses.OrderBy(c => c.Title).ToList();

            return DynamicResponse<List<PreviewCourseResponseDTO>>.CreateSuccess(orderCourses);

        }
        catch (Exception e)
        {
            return DynamicResponse<List<PreviewCourseResponseDTO>>.CreateError($"Ocurrió un error al obtener los cursos. \n Error: {e.Message}");
        }
    }

    public async Task<DynamicResponse<PaginatedResponseDto<PreviewCourseResponseDTO>>> GetPaginationCoursesAsync(int pageNumber, string? platform)
    {
        try
        {
            var projection = Builders<Course>.Projection.Expression(c => new PreviewCourseResponseDTO
            {
                Id = c.Id!,
                Title = c.Title,
                Categories = c.Categories,
                Teacher = c.Teacher,
                ImageUrl = c.ImageUrl
            });
            
            var courses = platform == null ? await _context.Courses.Find(new BsonDocument()).Project(projection).ToListAsync()
                : await _context.Courses.Find(c => c.Platform == platform).Project(projection).ToListAsync();
            
            if (courses.Count <= 0)
            {
                return DynamicResponse<PaginatedResponseDto<PreviewCourseResponseDTO>>.CreateError("No se encontraron cursos para paginar.", 404);
            }

            var paginatedData = courses
                .Skip((pageNumber - 1) * 12)
                .Take(12)
                .ToList();
            
            if (paginatedData.Count <= 0)
            {
                return DynamicResponse<PaginatedResponseDto<PreviewCourseResponseDTO>>.CreateError("No se encontraron cursos para paginar.", 404);
            }
            
            var response = new PaginatedResponseDto<PreviewCourseResponseDTO>()
            {
                ListCourses = paginatedData,
                TotalCount =  paginatedData.Count,
                PageNumber = pageNumber,
                PageSize = 12
            };

            return DynamicResponse<PaginatedResponseDto<PreviewCourseResponseDTO>>.CreateSuccess(response);

        }
        catch (Exception e)
        {
            return DynamicResponse<PaginatedResponseDto<PreviewCourseResponseDTO>>.CreateError($"Ocurrió un error al paginar los cursos. \n Error: {e.Message}");
        }
    }

    public async Task<DynamicResponse<FilterResponseDTO<PreviewCourseResponseDTO>>> GetFilterCoursesAsync(string? stringSearch, string? platform, string? category, string? teacher)
    {
        try
        {
            var queryCourses = _context.Courses.AsQueryable();

            if (!string.IsNullOrEmpty(stringSearch))
            {
                queryCourses = queryCourses.Where(c =>
                    c.Title.ToLower().Contains(stringSearch.ToLower())
                );
            }
            
            if (!string.IsNullOrEmpty(platform))
            {
                queryCourses = queryCourses.Where(c =>
                    c.Platform.Equals(platform)
                );
            }
            
            if (!string.IsNullOrEmpty(category))
            {
                queryCourses = queryCourses.Where(c =>
                    c.Categories.Contains(category)
                );
            }
            
            if (!string.IsNullOrEmpty(teacher))
            {
                queryCourses = queryCourses.Where(c =>
                    c.Teacher.Equals(teacher)
                );
            }

            if (queryCourses == null || !queryCourses.Any())
            {
                return DynamicResponse<FilterResponseDTO<PreviewCourseResponseDTO>>.CreateError("No se encontro ningun curso.", 404);
            }
            
            var projectedCourses = queryCourses.Select(c => new PreviewCourseResponseDTO
            {
                Id = c.Id!,
                Title = c.Title,
                Categories = c.Categories,
                Teacher = c.Teacher,
                ImageUrl = c.ImageUrl
            });
            

            return DynamicResponse<FilterResponseDTO<PreviewCourseResponseDTO>>.CreateSuccess(new FilterResponseDTO<PreviewCourseResponseDTO>()
            {
                FilterCourses = await projectedCourses.ToListAsync(),
                TotalCourses = projectedCourses.Count()
            });
        }
        catch (Exception e)
        {
            return DynamicResponse<FilterResponseDTO<PreviewCourseResponseDTO>>.CreateError($"Ocurrió un error al paginar los cursos. \n Error: {e.Message}");
        }
    }

    public async Task<DynamicResponse<Course>> GetCourseByIdAsync(string id)
    {
        try
        {
            var course = await _context.Courses.Find(c => c.Id == id).SingleOrDefaultAsync();

            course.Platform = PlatformsDictionary.GetPlatform(course.Platform);

            return course == null ? DynamicResponse<Course>.CreateError("No se encontro el curso buscado.", 404) 
                : DynamicResponse<Course>.CreateSuccess(course);
        }
        catch (Exception e)
        {
            return DynamicResponse<Course>.CreateError($"Ocurrió un error al paginar los cursos. \n Error: {e.Message}");
        }
    }

    public async Task<DynamicResponse<FilterResponseWithPlatformDTO<PreviewCourseResponseDTO>>> GetCoursesByPlatformAsync(string idPlatform)
    {
        try
        {
            var projection = Builders<Course>.Projection.Expression(c => new PreviewCourseResponseDTO
            {
                Id = c.Id!,
                Title = c.Title,
                Categories = c.Categories,
                Teacher = c.Teacher,
                ImageUrl = c.ImageUrl
            });

            var platform = await _context.Platforms.Find(p => p.Id == idPlatform).SingleOrDefaultAsync();
            
            var courses = await _context.Courses.Find(c => c.Platform == idPlatform).Project(projection).ToListAsync();

            return DynamicResponse<FilterResponseWithPlatformDTO<PreviewCourseResponseDTO>>.CreateSuccess(new FilterResponseWithPlatformDTO<PreviewCourseResponseDTO>()
            {
                FilterCourses = courses,
                TotalCourses = courses.Count,
                NamePlatform = platform.Name,
                DescriptionPlatform = platform.Description,
                ImagePlatform = platform.UrlImage
            });
        }
        catch (Exception e)
        {
            return DynamicResponse<FilterResponseWithPlatformDTO<PreviewCourseResponseDTO>>.CreateError($"Ocurrió un error al paginar los cursos. \n Error: {e.Message}");
        }
    }
}