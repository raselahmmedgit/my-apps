using System.Web.Mvc;
using Ninject;
using SoftwareGrid.Repository.iTestApp.Base;
using SoftwareGrid.Repository.iTestApp.DocumentManagement;
using SoftwareGrid.Repository.iTestApp.QuestionManagement;
using SoftwareGrid.Repository.iTestApp.TestManagement;
using SoftwareGrid.Repository.iTestApp.UserManagement;
using SoftwareGrid.Repository.iTestApp.Utility;
using SoftwareGrid.Service.iTestApp.Base;
using SoftwareGrid.Service.iTestApp.DocumentManagement;
using SoftwareGrid.Service.iTestApp.QuestionManagement;
using SoftwareGrid.Service.iTestApp.TestManagement;
using SoftwareGrid.Service.iTestApp.UserManagement;
using SoftwareGrid.Service.iTestApp.Utility;

namespace SoftwareGrid.Service.iTestApp.DI
{
    public class ResolveDependency
    {
        public void Resolve()
        {
            const string paramName = "AppDbContext";
            var kernel = new StandardKernel();
            var dbContext = new AppDbContext();

            kernel.Bind(typeof(IBaseService<>)).To(typeof(BaseService<>)).WithConstructorArgument(paramName, dbContext);
            kernel.Bind(typeof(IBaseRepository<>)).To(typeof(BaseRepository<>)).WithConstructorArgument(paramName, dbContext);

            #region User Management

            kernel.Bind(typeof(IUserRepository)).To(typeof(UserRepository)).WithConstructorArgument(paramName, dbContext);
            kernel.Bind(typeof(IUserService)).To(typeof(UserService)).WithConstructorArgument(paramName, dbContext);

            kernel.Bind(typeof(ICompanyRepository)).To(typeof(CompanyRepository)).WithConstructorArgument(paramName, dbContext);
            kernel.Bind(typeof(ICompanyService)).To(typeof(CompanyService)).WithConstructorArgument(paramName, dbContext);

            kernel.Bind(typeof(ICompanyBranchRepository)).To(typeof(CompanyBranchRepository)).WithConstructorArgument(paramName, dbContext);
            kernel.Bind(typeof(ICompanyBranchService)).To(typeof(CompanyBranchService)).WithConstructorArgument(paramName, dbContext);

            kernel.Bind(typeof(IRoleRepository)).To(typeof(RoleRepository)).WithConstructorArgument(paramName, dbContext);
            kernel.Bind(typeof(IRoleService)).To(typeof(RoleService)).WithConstructorArgument(paramName, dbContext);

            kernel.Bind(typeof(IUserRoleRepository)).To(typeof(UserRoleRepository)).WithConstructorArgument(paramName, dbContext);
            kernel.Bind(typeof(IUserRoleService)).To(typeof(UserRoleService)).WithConstructorArgument(paramName, dbContext);

            #endregion

            #region Test Management
            
            kernel.Bind<IQuestionCategoryService>().To<QuestionCategoryService>().WithConstructorArgument(paramName, dbContext);
            kernel.Bind<IQuestionService>().To<QuestionService>().WithConstructorArgument(paramName, dbContext);
            kernel.Bind<ITestCategoryService>().To<TestCategoryService>().WithConstructorArgument(paramName, dbContext);
            kernel.Bind<ITestService>().To<TestService>().WithConstructorArgument(paramName, dbContext);
            kernel.Bind<IFavoriteTestService>().To<FavoriteTestService>().WithConstructorArgument(paramName, dbContext);
            kernel.Bind<IDocumentInformationService>().To<DocumentInformationService>().WithConstructorArgument(paramName, dbContext);
            kernel.Bind<ITestTakenService>().To<TestTakenService>().WithConstructorArgument(paramName, dbContext);
            kernel.Bind<IUtilityService>().To<UtilityService>().WithConstructorArgument(paramName, dbContext);
            
            kernel.Bind<IQuestionCategoryRepository>().To<QuestionCategoryRepository>().WithConstructorArgument(paramName, dbContext);
            kernel.Bind<IQuestionRepository>().To<QuestionRepository>().WithConstructorArgument(paramName, dbContext);
            kernel.Bind<ITestCategoryRepository>().To<TestCategoryRepository>().WithConstructorArgument(paramName, dbContext);
            kernel.Bind<ITestRepository>().To<TestRepository>().WithConstructorArgument(paramName, dbContext);
            kernel.Bind<IFavoriteTestRepository>().To<FavoriteTestRepository>().WithConstructorArgument(paramName, dbContext);
            kernel.Bind<IDocumentInformationRepository>().To<DocumentInformationRepository>().WithConstructorArgument(paramName, dbContext);
            kernel.Bind<ITestTakenRepository>().To<TestTakenRepository>().WithConstructorArgument(paramName, dbContext);
            kernel.Bind<IUtilityRepository>().To<UtilityRepository>().WithConstructorArgument(paramName, dbContext);

            kernel.Bind<IQuestionAnswerOptionRepository>().To<QuestionAnswerOptionRepository>().WithConstructorArgument(paramName, dbContext);
            kernel.Bind<ITestWiseQuestionRepository>().To<TestWiseQuestionRepository>().WithConstructorArgument(paramName, dbContext);
            kernel.Bind<ITestTakenDetailsRepository>().To<TestTakenDetailsRepository>().WithConstructorArgument(paramName, dbContext);

            #endregion

            DependencyResolver.SetResolver(new NinjectDependencyResolver(kernel));
        }
    }
}
