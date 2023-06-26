using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SaidalyTechMain.BL.IServices
{
    public interface IService<T> where T : class
    {
        Task<bool> Add(T entity);
        Task<bool> AddRange(List<T> entity);
        Task<bool> AddRangeAndSQLQuery(List<T> entity, int id);
        Task<bool> EditRange(List<T> entity);
        Task<bool> Edit(T entity);
        Task<bool> DeleteBy(Expression<Func<T, bool>> where);
        Task<bool> Delete(T entity);
        Task<bool> DeleteListBy(Expression<Func<T, bool>> where);
        Task<List<T>> GetAllIncload(Expression<Func<T, object>> Incload);
        Task<T> GetByIncload(Expression<Func<T, bool>> where, Expression<Func<T, object>> Incload);
        Task<T> GetObjectBy(Expression<Func<T, bool>> expression);
        Task<T> GetNext(Expression<Func<T, bool>> where, Expression<Func<T, object>> OrderBy);
        Task<T> GetPrevious(Expression<Func<T, bool>> where, Expression<Func<T, object>> OrderByDescending);
        Task<bool> Any(Expression<Func<T, bool>> expression);
        Task<bool> EditProperties(T entity, List<LambdaExpression> propertyExpression);
        Task<bool> EditProperty(T entity, Expression<Func<T, object>> property);
        Task<bool> EditListProperties(List<T> entity, List<LambdaExpression> propertyExpression);
        Task<bool> EditListProperty(List<T> entity, Expression<Func<T, object>> property);
        Task<List<T>> GetListBy(Expression<Func<T, bool>> where);
        Task<List<T>> GetAll();
        Task<T> GetFirst();
        Task<bool> DeleteRange(List<T> entity);
        Task<List<T>> CallStoredProcedure(string Query);
        Task<bool> DeleteList(List<T> list);
        Task<bool> ExecuteSql(string Query);

    }
}
