using Microsoft.EntityFrameworkCore;
using SaidalyTechMain.BL.IServices;
using SaidalyTechMain.DB_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SaidalyTechMain.BL.ClsServices
{
    public class ClsService<T> : IService<T> where T : class
    {
        public async Task<bool> Add(T entity)
        {
            try
            {
                using (var _context = new PharmacySystemContext())
                {
                    await _context.Set<T>().AddAsync(entity);
                    if (await _context.SaveChangesAsync() > 0)
                        return true;
                    return false;
                }

            }
            catch
            {
                return false;
            }
        }
        public async Task<bool> AddRange(List<T> entity)
        {
            try
            {
                using (var _context = new PharmacySystemContext())
                {
                    await _context.Set<T>().AddRangeAsync(entity);
                    if (await _context.SaveChangesAsync() == entity.Count)
                        return true;
                    return false;
                }

            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<bool> Any(Expression<Func<T, bool>> expression)
        {
            try
            {
                using (var _context = new PharmacySystemContext())
                {
                    return await _context.Set<T>().AnyAsync(expression);
                }
            }
            catch
            {
                return false;
            }
        }
        public async Task<bool> EditProperties(T entity, List<LambdaExpression> propertyExpression)
        {
            try
            {
                using (var _context = new PharmacySystemContext())
                {
                    foreach (Expression<Func<T, object>> property in propertyExpression)
                    {
                        _context.Entry(entity).Property(property).IsModified = true;
                    }
                    if (await _context.SaveChangesAsync() > 0)
                        return true;
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }
        public async Task<bool> Delete(T entity)
        {
            try
            {
                using (var _context = new PharmacySystemContext())
                {
                    await Task.Run(() => _context.Set<T>().Remove(entity));
                    if (await _context.SaveChangesAsync() > 0)
                        return true;
                    return false;
                }

            }
            catch
            {
                return false;
            }
        }
        public async Task<bool> DeleteBy(Expression<Func<T, bool>> expression)
        {
            try
            {
                T entity = await GetObjectBy(expression);
                if (entity != null)
                {
                    using (var _context = new PharmacySystemContext())
                    {
                        _context.Set<T>().Remove(entity);
                        if (await _context.SaveChangesAsync() > 0)
                            return true;
                    }
                }
                return false;

            }
            catch
            {
                return false;
            }
        }
        public async Task<bool> DeleteRange(List<T> entity)
        {
            try
            {
                using (var _context = new PharmacySystemContext())
                {
                    await Task.Run(() => _context.Set<T>().RemoveRange(entity));
                    if (await _context.SaveChangesAsync() == entity.Count)
                        return true;
                    return false;
                }

            }
            catch
            {
                return false;
            }
        }
        public async Task<bool> Edit(T entity)
        {
            try
            {
                using (var _context = new PharmacySystemContext())
                {
                    _context.Set<T>().Update(entity);
                    if (await _context.SaveChangesAsync() > 0)
                        return true;
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }
        public async Task<List<T>> GetAll()
        {
            try
            {
                using (var _context = new PharmacySystemContext())
                {
                    return await _context.Set<T>().ToListAsync();
                }
            }
            catch
            {
                return new List<T>();
            }
        }
        public async Task<List<T>> GetAllIncload(Expression<Func<T, object>> expression)
        {
            using (var _context = new PharmacySystemContext())
            {
                return await Task.Run(() => _context.Set<T>().Include(expression).ToListAsync());
            }
        }
        public async Task<T> GetObjectBy(Expression<Func<T, bool>> expression)
        {
            try
            {
                using (var _context = new PharmacySystemContext())
                {

                    return await _context.Set<T>().FirstOrDefaultAsync(expression);
                }
            }
            catch
            {
                return null;
            }
        }
        public async Task<T> GetByIncload(Expression<Func<T, bool>> where, Expression<Func<T, object>> Incload)
        {
            using (var _context = new PharmacySystemContext())
            {

                return await Task.Run(() => _context.Set<T>().Where(where).Include(Incload).FirstOrDefaultAsync());
            }
        }
        public async Task<T> GetFirst()
        {
            try
            {
                using (var _context = new PharmacySystemContext())
                {
                    return await _context.Set<T>().FirstOrDefaultAsync();
                }
            }
            catch
            {
                return null;
            }
        }
        public async Task<List<T>> GetListBy(Expression<Func<T, bool>> where)
        {
            try
            {

                using (var _context = new PharmacySystemContext())
                {
                    return await _context.Set<T>().Where(where).ToListAsync();
                }
            }
            catch
            {
                return new List<T>();
            }
        }
        public async Task<bool> EditRange(List<T> entity)
        {
            try
            {
                using (var _context = new PharmacySystemContext())
                {
                    _context.Set<T>().UpdateRange(entity);
                    if (await _context.SaveChangesAsync() > 0)
                        return true;
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }
        [Obsolete]
        public async Task<bool> AddRangeAndSQLQuery(List<T> entity, int id)
        {
            try
            {
                using (var _context = new PharmacySystemContext())
                {
                    await _context.Set<T>().AddRangeAsync(entity);
                    if (await _context.SaveChangesAsync() == entity.Count)
                    {
                        if (await _context.Database.ExecuteSqlCommandAsync($"update TbSalesInvoices set Total = TbSalesInvoices.Tax  +(select sum(TbSalesInvoiceItems.Total) from TbSalesInvoiceItems where  InvoiceId ={id} )  where Id={id} ") == 1)
                            return true;
                    }
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }
        public async Task<T> GetNext(Expression<Func<T, bool>> where, Expression<Func<T, object>> OrderBy)
        {
            try
            {
                using (var _context = new PharmacySystemContext())
                {
                    return await _context.Set<T>().Where(where).OrderBy(OrderBy).FirstOrDefaultAsync();
                }
            }
            catch
            {
                return null;
            }
        }
        public async Task<T> GetPrevious(Expression<Func<T, bool>> where, Expression<Func<T, object>> OrderByDescending)
        {
            try
            {
                using (var _context = new PharmacySystemContext())
                {
                    return await _context.Set<T>().Where(where).OrderByDescending(OrderByDescending).FirstOrDefaultAsync();
                }
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<T>> CallStoredProcedure(string Query)
        {
            try
            {
                using (var _context = new PharmacySystemContext())
                {
                    return await _context.Set<T>().FromSqlRaw(Query).ToListAsync();
                }
            }
            catch
            {
                return new List<T>();
            }
        }

        public async Task<bool> DeleteList(List<T> list)
        {
            try
            {
                using (var _context = new PharmacySystemContext())
                {
                    _context.Set<T>().RemoveRange(list);
                    if (await _context.SaveChangesAsync() > 0)
                        return true;
                }
                return false;

            }
            catch
            {
                return false;
            }
        }

        [Obsolete]
        public async Task<bool> ExecuteSql(string Query)
        {
            try
            {
                using (var _context = new PharmacySystemContext())
                {
                    if (await _context.Database.ExecuteSqlCommandAsync(Query) > 0)
                        return true;
                }
                return false;

            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteListBy(Expression<Func<T, bool>> where)
        {
            try
            {
                List<T> entity = await GetListBy(where);
                if (entity.Count > 0)
                {
                    using (var _context = new PharmacySystemContext())
                    {
                        _context.Set<T>().RemoveRange(entity);
                        if (await _context.SaveChangesAsync() > 0)
                            return true;
                    }
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> EditListProperties(List<T> entity, List<LambdaExpression> propertyExpression)
        {
            try
            {
                using (var _context = new PharmacySystemContext())
                {
                    foreach (var item in entity)
                    {
                        foreach (Expression<Func<T, object>> property in propertyExpression)
                        {
                            _context.Entry(item).Property(property).IsModified = true;
                        }
                    }
                    if (await _context.SaveChangesAsync() > 0)
                        return true;
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> EditListProperty(List<T> entity, Expression<Func<T, object>> property)
        {
            try
            {
                using (var _context = new PharmacySystemContext())
                {
                    foreach (var item in entity)
                    {
                        _context.Entry(item).Property(property).IsModified = true;
                    }
                    if (await _context.SaveChangesAsync() > 0)
                        return true;
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> EditProperty(T entity, Expression<Func<T, object>> property)
        {
            try
            {
                using (var _context = new PharmacySystemContext())
                {
                    _context.Entry(entity).Property(property).IsModified = true;
                    if (await _context.SaveChangesAsync() > 0)
                        return true;
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
