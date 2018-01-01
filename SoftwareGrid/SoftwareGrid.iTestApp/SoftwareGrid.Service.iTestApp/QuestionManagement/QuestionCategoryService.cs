using SoftwareGrid.Model.iTestApp.QuestionManagement;
using SoftwareGrid.Repository.iTestApp.Base;
using SoftwareGrid.Repository.iTestApp.QuestionManagement;
using SoftwareGrid.Service.iTestApp.Base;

namespace SoftwareGrid.Service.iTestApp.QuestionManagement
{
    public class QuestionCategoryService : BaseService<QuestionCategory>, IQuestionCategoryService
    {
        private readonly IQuestionCategoryRepository _iQuestionCategoryRepository;
        private readonly DbContext _dbContext;

        public QuestionCategoryService(IBaseRepository<QuestionCategory> iBaseRepository, IQuestionCategoryRepository iQuestionCategoryRepository, DbContext dbContext)
            : base(iBaseRepository, dbContext)
        {
            _iQuestionCategoryRepository = iQuestionCategoryRepository;
            _dbContext = dbContext;
        }
    }

    public interface IQuestionCategoryService : IBaseService<QuestionCategory>
    {
    }
}
