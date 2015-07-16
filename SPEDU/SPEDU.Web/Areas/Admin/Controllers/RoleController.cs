using SPEDU.Business.Application;
using SPEDU.DomainViewModel.Application;
using SPEDU.Web.Helpers;
using SPEDU.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SPEDU.Web.Areas.Admin.Controllers
{
    public class RoleController : BaseController
    {
        #region Global Variable Declaration

        private readonly IRoleRepository _iRoleRepository;

        #endregion

        #region Constructor

        public RoleController(IRoleRepository iRoleRepository)
        {
            this._iRoleRepository = iRoleRepository;
        }

        #endregion

        #region Action

        //
        // GET: /Role/

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult IndexAjax(KendoUiGridParam request)
        {
            var roleViewModels = GetRoleDataList().AsQueryable();
            var models = KendoUiHelper.ParseGridData<RoleViewModel>(roleViewModels, request);

            return Json(models, JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /Role/Details/By ID

        public ActionResult Details(int id)
        {
            ErrorViewModel errorViewModel;

            try
            {
                var role = _iRoleRepository.GetById(id);
                if (role != null)
                {
                    var viewModel = new RoleViewModel() { RoleId = role.RoleId, RoleName = role.RoleName };

                    return PartialView("_Details", viewModel);
                }

                errorViewModel = ExceptionHelper.ExceptionErrorMessageForNullObject();
            }
            catch (Exception ex)
            {
                errorViewModel = ExceptionHelper.ExceptionErrorMessageFormat(ex);
            }

            return PartialView("_ErrorPopup", errorViewModel);
        }

        //
        // GET: /Role/Add

        public ActionResult Add()
        {
            var viewModel = new RoleViewModel();

            //return View();
            return PartialView("_AddOrEdit", viewModel);
        }

        //
        // GET: /Role/Edit/By ID

        public ActionResult Edit(int id)
        {
            ErrorViewModel errorViewModel;

            try
            {
                var role = _iRoleRepository.GetById(id);
                if (role != null)
                {
                    var viewModel = new RoleViewModel()
                    {
                        RoleId = role.RoleId,
                        RoleName = role.RoleName
                    };
                    return PartialView("_AddOrEdit", viewModel);
                }

                errorViewModel = ExceptionHelper.ExceptionErrorMessageForNullObject();
            }
            catch (Exception ex)
            {
                errorViewModel = ExceptionHelper.ExceptionErrorMessageFormat(ex);
            }

            return PartialView("_ErrorPopup", errorViewModel);
        }

        //
        // POST: /Role/Save

        [HttpPost]
        public ActionResult SaveAjax(RoleViewModel roleViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _iRoleRepository.CreateOrUpdate(roleViewModel);
                    return Content(KendoUiHelper.GetKendoUiWindowAjaxSuccessMethod(Boolean.TrueString, roleViewModel.ActionName, MessageType.success.ToString(), ResourceHelper.Save));
                }

                return Content(KendoUiHelper.GetKendoUiWindowAjaxSuccessMethod(Boolean.FalseString, roleViewModel.ActionName, MessageType.warning.ToString(), ExceptionHelper.ModelStateErrorFormat(ModelState)));
            }
            catch (Exception ex)
            {
                return Content(KendoUiHelper.GetKendoUiWindowAjaxSuccessMethod(Boolean.FalseString, roleViewModel.ActionName, MessageType.warning.ToString(), ExceptionHelper.ExceptionMessageFormat(ex)));
            }
        }

        //
        // POST: /Role/Delete/By ID
        [HttpPost]
        public ActionResult DeleteAjax(int id)
        {
            try
            {
                _iRoleRepository.Delete(id);

                return Json(new { status = Boolean.FalseString, messageType = MessageType.success.ToString(), messageText = ResourceHelper.Delete }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { status = Boolean.FalseString, messageType = MessageType.danger.ToString(), messageText = ExceptionHelper.ExceptionMessageFormat(ex) }, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion

        #region Method

        private List<RoleViewModel> GetRoleDataList()
        {
            var viewModelList = _iRoleRepository.GetAll().ToList();
            return viewModelList;
        }

        #endregion
    }
}