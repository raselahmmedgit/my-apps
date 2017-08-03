using rabapp.Service.Quiz.SecurityManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rabapp.Utility.DbMigration
{
    public class QuizDbTest
    {
        #region Global Variable Declaration

        private readonly IUserService _iUserService;

        #endregion

        #region Constructor

        public QuizDbTest()
        {
        }

        public QuizDbTest(IUserService iUserService)
        {
            this._iUserService = iUserService;
        }

        #endregion

        #region Action Methods

        public void Data()
        {
            try
            {
                var userList = _iUserService.GetAll();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        #endregion
    }
}
