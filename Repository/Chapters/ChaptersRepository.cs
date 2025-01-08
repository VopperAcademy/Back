using MongoDB.Driver;
using vopperAcademyBackEnd.Data;
using vopperAcademyBackEnd.Models;
using vopperAcademyBackEnd.Models.DTOs;

namespace vopperAcademyBackEnd.Repository.Chapters;

public class ChaptersRepository : IChaptersRepository
{
    private readonly MongoDbContext _context;

    public ChaptersRepository(MongoDbContext context)
    {
        _context = context;
    }


    public async Task<DynamicResponse<Chapter>> GetChapterAsync(string idCourse ,int chapterPositionInArray)
    {
        try
        {
            var course = await _context.Courses.Find(c => c.Id == idCourse).SingleOrDefaultAsync();

            if (course == null)
                return DynamicResponse<Chapter>.CreateError("No se encontro el curso.", 404);

            if (chapterPositionInArray > course.Chapters.Count)
                return DynamicResponse<Chapter>.CreateError("No existe ese capitulo dentro del curso.", 404);

            var chapter = course?.Chapters[chapterPositionInArray];

            return DynamicResponse<Chapter>.CreateSuccess(chapter!);
        }
        catch (Exception e)
        {
            return DynamicResponse<Chapter>.CreateError($"Ocurrió un error al Obtener el capitulo. \n Error: {e.Message}");
        }
    }

    public async Task<DynamicResponse<Chapter>> PostChapterAsync(Chapter chapter, string idCourse)
    {
        try
        {
            var filter = Builders<Course>.Filter.Eq(c => c.Id, idCourse);
            
            var update = Builders<Course>.Update.Push(c => c.Chapters, chapter);
            
            var updateResult = await _context.Courses.UpdateOneAsync(filter, update);

            if (updateResult.ModifiedCount <= 0) return DynamicResponse<Chapter>.CreateError("No se encontro el curso.", 404);

            return DynamicResponse<Chapter>.CreateSuccess(chapter);
        }
        catch (Exception e)
        {
            return DynamicResponse<Chapter>.CreateError($"Ocurrió un error al agregar el capitulo. \n Error: {e.Message}");
        }
    }

    public async Task<DynamicResponse<int>> PutViewsInChapterAsync(string idCourse, int chapter)
    { 
        try
        {
            var course = await _context.Courses.Find(c => c.Id == idCourse).SingleOrDefaultAsync();

            if (course == null) return DynamicResponse<int>.CreateError("No se ha encontrado el capitulo selecionado.", 404);

            chapter--;
            
            course.Chapters[chapter].Views++;
            
            var filter = Builders<Course>.Filter.Eq(c => c.Id, idCourse);
            
            var update = Builders<Course>.Update.Inc(c => c.Chapters[chapter].Views, 1);

            var results = await _context.Courses.UpdateOneAsync(filter, update);

            return results.ModifiedCount > 0
                ? DynamicResponse<int>.CreateSuccess(course.Chapters[chapter].Views)
                : DynamicResponse<int>.CreateError("No hubo ninguna actualizacion en las vistas.");

        }
        catch (Exception e)
        {
            return DynamicResponse<int>.CreateError($"Ocurrió un error al contar la vista. \n Error: {e.Message}");
        }
    }

    
}