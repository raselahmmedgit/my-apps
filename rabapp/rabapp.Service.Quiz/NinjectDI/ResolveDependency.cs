using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Ninject;
using rabapp.Repository.Common;
using rabapp.Repository.Quiz.DocumentManagement;
using rabapp.Repository.Quiz.QuestionManagement;
using rabapp.Repository.Quiz.SecurityManagement;
using rabapp.Repository.Quiz.TestManagement;
using rabapp.Service.Common;
using rabapp.Service.Quiz.DocumentManagement;
using rabapp.Service.Quiz.QuestionManagement;
using rabapp.Service.Quiz.SecurityManagement;
using rabapp.Service.Quiz.TestManagement;

namespace rabapp.Service.Quiz.NinjectDI
{
    public class ResolveDependency
    {
        public void Resolve()
        {
            const string paramName = "appDbContext";
            var kernel = new StandardKernel();
            var appDbContext = new AppDbContext();

            #region Security
            kernel.Bind(typeof(IUserRepository)).To(typeof(UserRepository)).WithConstructorArgument(paramName, appDbContext);
            kernel.Bind(typeof(IUserService)).To(typeof(UserService)).WithConstructorArgument(paramName, appDbContext);

            #endregion

            #region ITestApp

            kernel.Bind(typeof(IBaseService<>)).To(typeof(BaseService<>)).WithConstructorArgument(paramName, appDbContext);
            kernel.Bind<IQuestionCategoryService>().To<QuestionCategoryService>().WithConstructorArgument(paramName, appDbContext);
            kernel.Bind<IQuestionService>().To<QuestionService>().WithConstructorArgument(paramName, appDbContext);
            kernel.Bind<ITestCategoryService>().To<TestCategoryService>().WithConstructorArgument(paramName, appDbContext);
            kernel.Bind<ITestService>().To<TestService>().WithConstructorArgument(paramName, appDbContext);
            kernel.Bind<IFavoriteTestService>().To<FavoriteTestService>().WithConstructorArgument(paramName, appDbContext);
            kernel.Bind<IDocumentInformationService>().To<DocumentInformationService>().WithConstructorArgument(paramName, appDbContext);
            kernel.Bind<ITestTakenService>().To<TestTakenService>().WithConstructorArgument(paramName, appDbContext);

            kernel.Bind(typeof(IBaseRepository<>)).To(typeof(BaseRepository<>)).WithConstructorArgument(paramName, appDbContext);
            kernel.Bind<IQuestionCategoryRepository>().To<QuestionCategoryRepository>().WithConstructorArgument(paramName, appDbContext);
            kernel.Bind<IQuestionRepository>().To<QuestionRepository>().WithConstructorArgument(paramName, appDbContext);
            kernel.Bind<IQuestionAnswerOptionRepository>().To<QuestionAnswerOptionRepository>().WithConstructorArgument(paramName, appDbContext);
            kernel.Bind<ITestCategoryRepository>().To<TestCategoryRepository>().WithConstructorArgument(paramName, appDbContext);
            kernel.Bind<ITestRepository>().To<TestRepository>().WithConstructorArgument(paramName, appDbContext);
            kernel.Bind<ITestWiseQuestionRepository>().To<TestWiseQuestionRepository>().WithConstructorArgument(paramName, appDbContext);
            kernel.Bind<IDocumentInformationRepository>().To<DocumentInformationRepository>().WithConstructorArgument(paramName, appDbContext);
            kernel.Bind<ITestTakenRepository>().To<TestTakenRepository>().WithConstructorArgument(paramName, appDbContext);
            kernel.Bind<ITestTakenDetailsRepository>().To<TestTakenDetailsRepository>().WithConstructorArgument(paramName, appDbContext);
            kernel.Bind<IFavoriteTestRepository>().To<FavoriteTestRepository>().WithConstructorArgument(paramName, appDbContext);

            #endregion

            DependencyResolver.SetResolver(new NinjectDependencyResolver(kernel));
        }
    }
}
