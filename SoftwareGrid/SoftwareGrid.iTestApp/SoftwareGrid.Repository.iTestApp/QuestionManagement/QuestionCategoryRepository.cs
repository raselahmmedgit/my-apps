using SoftwareGrid.Model.iTestApp.QuestionManagement;
using SoftwareGrid.Repository.iTestApp.Base;

namespace SoftwareGrid.Repository.iTestApp.QuestionManagement
{
    public class QuestionCategoryRepository : BaseRepository<QuestionCategory>, IQuestionCategoryRepository
    {
        private readonly DbContext _dbContext;
        public QuestionCategoryRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }

    public interface IQuestionCategoryRepository : IBaseRepository<QuestionCategory>
    {
    }
}
