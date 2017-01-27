using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using rabapp.Model.Quiz.QuestionManagement;
using rabapp.Model.Quiz.ViewModels;
using rabapp.Repository.Common;

namespace rabapp.Repository.Quiz.QuestionManagement
{
    public class QuestionCategoryRepository : BaseRepository<QuestionCategoryViewModel>, IQuestionCategoryRepository
    {
        private readonly AppDbContext _appDbContext;
        public QuestionCategoryRepository(AppDbContext appDbContext)
            : base(appDbContext)
        {
        }
    }

    public interface IQuestionCategoryRepository : IBaseRepository<QuestionCategoryViewModel>
    {
    }
}
