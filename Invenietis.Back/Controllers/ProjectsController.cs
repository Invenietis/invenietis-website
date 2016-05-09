using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Q = Invenietis.Repositories.Queries;
using C = Invenietis.Repositories.Commands;
using Invenietis.Common;
using Invenietis.Data.Entities;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Invenietis.Back.Controllers
{
    public class ProjectsController : Controller
    {
        Q.ProjectRepository _qRepo;
        C.ProjectRepository _cRepo;

        public ProjectsController( Q.ProjectRepository qRepo, C.ProjectRepository cRepo )
        {
            _qRepo = qRepo;
            _cRepo = cRepo;
        }

        [HttpGet]
        public JsonResult GetAll( Q.Filters.ProjectFilter lFilter, Q.Filters.OrderFilter oFilter, PaginationInfo pInfo )
        {
            return new JsonResult( _qRepo.GetProjects( lFilter, oFilter, pInfo ) );
        }

        [HttpGet]
        public JsonResult Edit( int id )
        {
            var model = _qRepo.GetProjectById( id );

            if( model == null )
            {
                id = _cRepo.CreateProject();
                model = _qRepo.GetProjectById( id );
            }

            return new JsonResult( model );
        }

        [HttpPost]
        public JsonResult Save( [FromBody] Project project )
        {
            return new JsonResult( _cRepo.UpdateProject( project ) );
        }

        [HttpDelete]
        public JsonResult Delete( int id )
        {
            return new JsonResult( _cRepo.DeleteProject( id ) );
        }

        [HttpGet]
        public JsonResult GetAllCategories()
        {
            return new JsonResult( _qRepo.GetProjectCategories() );
        }

        [HttpGet]
        public JsonResult EditCategory( int id )
        {
            var model = _qRepo.GetProjectCategoryById( id );

            if( model == null )
            {
                id = _cRepo.CreateCategory();
                model = _qRepo.GetProjectCategoryById( id );
            }

            return new JsonResult( model );
        }

        [HttpPost]
        public JsonResult SaveCategory( ProjectCategory category )
        {
            return new JsonResult( _cRepo.UpdateCategory( category ) );
        }

        [HttpDelete]
        public JsonResult DeleteCategory( int id )
        {
            return new JsonResult( _cRepo.DeleteCategory( id ) );
        }
    }
}
