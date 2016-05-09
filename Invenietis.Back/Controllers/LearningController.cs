using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Q = Invenietis.Repositories.Queries;
using C = Invenietis.Repositories.Commands;
using Invenietis.Data.Entities;
using Invenietis.Common;

namespace Invenietis.Back.Controllers
{
    public class LearningController : Controller
    {
        Q.LearningRepository _qRepo;
        C.LearningRepository _cRepo;

        public LearningController( Q.LearningRepository qRepo, C.LearningRepository cRepo )
        {
            _qRepo = qRepo;
            _cRepo = cRepo;
        }

        [HttpGet]
        public JsonResult GetAll( Q.Filters.LearningFilter lFilter, Q.Filters.OrderFilter oFilter, PaginationInfo pInfo )
        {
            return new JsonResult( _qRepo.GetLearnings( lFilter, oFilter, pInfo ) );
        }

        [HttpGet]
        public JsonResult Edit( int id )
        {
            var model = _qRepo.GetLearningById( id );

            if( model == null )
            {
                id = _cRepo.CreateLearning();
                model = _qRepo.GetLearningById( id );
            }

            return new JsonResult( model );
        }

        [HttpPost]
        public JsonResult Save( [FromBody] Learning learning )
        {
            return new JsonResult( _cRepo.UpdateLearning( learning ) );
        }

        [HttpDelete]
        public JsonResult Delete( int id )
        {
            return new JsonResult( _cRepo.DeleteLearning( id ) );
        }

        [HttpGet]
        public JsonResult GetAllCategories()
        {
            return new JsonResult( _qRepo.GetLearningCategories() );
        }

        [HttpGet]
        public JsonResult EditCategory( int id )
        {
            var model = _qRepo.GetLearningCategoryById( id );

            if( model == null )
            {
                id = _cRepo.CreateCategory();
                model = _qRepo.GetLearningCategoryById( id );
            }

            return new JsonResult( model );
        }

        [HttpPost]
        public JsonResult SaveCategory( LearningCategory category )
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
