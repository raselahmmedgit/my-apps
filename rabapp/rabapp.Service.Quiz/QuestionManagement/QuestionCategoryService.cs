using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using rabapp.Model.Quiz.ViewModels;
using rabapp.Repository.Common;
using rabapp.Repository.Quiz.QuestionManagement;
using rabapp.Service.Common;

namespace rabapp.Service.Quiz.QuestionManagement
{
    public class QuestionCategoryService : BaseService<QuestionCategoryViewModel>, IQuestionCategoryService
    {
        private readonly IQuestionCategoryRepository _iQuestionCategoryRepository;
        private readonly AppDbContext _appDbContext;

        public QuestionCategoryService(IBaseRepository<QuestionCategoryViewModel> iBaseRepository, IQuestionCategoryRepository iQuestionCategoryRepository, AppDbContext appDbContext)
            : base(iBaseRepository, appDbContext)
        {
            _iQuestionCategoryRepository = iQuestionCategoryRepository;
            _appDbContext = appDbContext;
        }
    }

    public interface IQuestionCategoryService : IBaseService<QuestionCategoryViewModel>
    {
    }
}
