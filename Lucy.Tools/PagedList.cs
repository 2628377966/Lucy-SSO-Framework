using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System;
using System.Linq.Dynamic;

namespace Lucy.Tools
{
    /// <summary>
    /// Paged list
    /// </summary>
    /// <typeparam name="T">T</typeparam>
    public class PagedList<T> : List<T>
    {
        public PagedList() { }
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="source">source</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        public PagedList(IQueryable<T> source, GridPageModel pager)
        {
            if (!string.IsNullOrWhiteSpace(pager.sidx) && pager.sidx.Trim().Length > 0)
            {
                string sortingDir = string.Empty;
                if (pager.sord.ToUpper().Trim() == "ASC")
                    sortingDir = "OrderBy";
                else if (pager.sord.ToUpper().Trim() == "DESC")
                    sortingDir = "OrderByDescending";
                //ParameterExpression param = Expression.Parameter(typeof(T), sortExpression);
                //PropertyInfo pi = typeof(T).GetProperty(sortExpression);
                //Type[] types = new Type[2];
                //types[0] = typeof(T);
                //types[1] = pi.PropertyType;
                //Expression expr = Expression.Call(typeof(Queryable), sortingDir, types, source.Expression, Expression.Lambda(Expression.Property(param, sortExpression), param));
                //source = source.AsQueryable().Provider.CreateQuery<T>(expr);


                source = source.OrderBy(pager.sidx + " " + pager.sord);
            }

            int total = source.Count();
            this.TotalCount = total;
            this.TotalPages = total / pager.rows;

            if (total % pager.rows > 0)
                TotalPages++;

            this.PageSize = pager.rows;
            this.PageIndex = pager.page;
            this.AddRange(source.Skip((pager.page - 1) * pager.rows).Take(pager.rows).ToList());
        }
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="source">source</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        public PagedList(IQueryable<T> source, int pageIndex, int pageSize, string sortExpression, string sortDirection)
        {
            if (!string.IsNullOrWhiteSpace(sortExpression) && sortExpression.Trim().Length > 0)
            {
                string sortingDir = string.Empty;
                if (sortDirection.ToUpper().Trim() == "ASC")
                    sortingDir = "OrderBy";
                else if (sortDirection.ToUpper().Trim() == "DESC")
                    sortingDir = "OrderByDescending";
                //ParameterExpression param = Expression.Parameter(typeof(T), sortExpression);
                //PropertyInfo pi = typeof(T).GetProperty(sortExpression);
                //Type[] types = new Type[2];
                //types[0] = typeof(T);
                //types[1] = pi.PropertyType;
                //Expression expr = Expression.Call(typeof(Queryable), sortingDir, types, source.Expression, Expression.Lambda(Expression.Property(param, sortExpression), param));
                //source = source.AsQueryable().Provider.CreateQuery<T>(expr);


                source = source.OrderBy(sortExpression + " " + sortDirection);
            }

            int total = source.Count();
            this.TotalCount = total;
            this.TotalPages = total / pageSize;

            if (total % pageSize > 0)
                TotalPages++;

            this.PageSize = pageSize;
            this.PageIndex = pageIndex;
            this.AddRange(source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList());
        }

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="source">source</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        public PagedList(IQueryable<T> source, int pageIndex, int pageSize)
        {

            int total = source.Count();
            this.TotalCount = total;
            this.TotalPages = total / pageSize;

            if (total % pageSize > 0)
                TotalPages++;

            this.PageSize = pageSize;
            this.PageIndex = pageIndex;
            this.AddRange(source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList());
        }

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="source">source</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        public PagedList(List<T> source, GridPageModel pager)
        {
            var query = source.AsQueryable();
            if (pager.sidx.Trim().Length > 0)
            {
                string sortingDir = string.Empty;
                if (pager.sord.ToUpper().Trim() == "ASC")
                    sortingDir = "OrderBy";
                else if (pager.sord.ToUpper().Trim() == "DESC")
                    sortingDir = "OrderByDescending";
                ParameterExpression param = Expression.Parameter(typeof(T), pager.sidx);
                PropertyInfo pi = typeof(T).GetProperty(pager.sidx);
                Type[] types = new Type[2];
                types[0] = typeof(T);
                types[1] = pi.PropertyType;
                Expression expr = Expression.Call(typeof(Queryable), sortingDir, types, query.Expression, Expression.Lambda(Expression.Property(param, pager.sidx), param));
                query = query.AsQueryable().Provider.CreateQuery<T>(expr);
            }

            TotalCount = query.Count();
            TotalPages = TotalCount / pager.rows;

            if (TotalCount % pager.rows > 0)
                TotalPages++;

            this.PageSize = pager.rows;
            this.PageIndex = pager.page;
            this.AddRange(query.Skip((pager.page - 1) * pager.rows).Take(pager.rows).ToList());
        }

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="source">source</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        public PagedList(List<T> source, int pageIndex, int pageSize)
        {
            TotalCount = source.Count();
            TotalPages = TotalCount / pageSize;

            if (TotalCount % pageSize > 0)
                TotalPages++;

            this.PageSize = pageSize;
            this.PageIndex = pageIndex;
            this.AddRange(source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList());
        }

        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }

        public bool HasPreviousPage
        {
            get { return (PageIndex > 0); }
        }
        public bool HasNextPage
        {
            get { return (PageIndex + 1 < TotalPages); }
        }
    }
}
