using vopperAcademyBackEnd.Models;
using vopperAcademyBackEnd.Models.DTOs;

namespace vopperAcademyBackEnd.Repository.Chapters;

public interface IChaptersRepository
{
    Task<DynamicResponse<Chapter>> GetChapterAsync(string idCourse ,int chapterPositionInArray);
    Task<DynamicResponse<Chapter>> PostChapterAsync(Chapter chapter, string idCourse);
    Task<DynamicResponse<int>> PutViewsInChapterAsync(string idCourse, int chapter);
}