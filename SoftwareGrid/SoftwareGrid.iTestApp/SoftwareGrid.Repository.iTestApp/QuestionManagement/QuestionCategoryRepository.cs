using SoftwareGrid.Model.iTestApp.QuestionManagement;
using SoftwareGrid.Repository.iTestApp.Base;

namespace SoftwareGrid.Repository.iTestApp.QuestionManagement
{
    public class QuestionCategoryRepository : BaseRepository<QuestionCategory>, IQuestionCategoryRepository
    {
        private readonly AppDbContext _dbContext;
        public QuestionCategoryRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }

    public interface IQuestionCategoryRepository : IBaseRepository<QuestionCategory>
    {
    }
}
