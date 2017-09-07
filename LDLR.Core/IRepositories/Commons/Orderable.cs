using System;
using System.Linq;
using System.Linq.Expressions;
namespace LDLR.Core.IRepositories.Commons
{
    /// <summary>
    /// Linq�ܹ���Լ�������ʵ��
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Orderable<T> : IOrderable<T>
    {
        private IQueryable<T> _queryable;

        /// <summary>
        /// �����Ľ����
        /// </summary>
        /// <param name="enumerable"></param>
        public Orderable(IQueryable<T> enumerable)
        {
            _queryable = enumerable;
        }

        /// <summary>
        /// ����֮��Ľ����
        /// </summary>
        public IQueryable<T> Queryable
        {
            get { return _queryable; }
        }
        /// <summary>
        /// ����
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="keySelector"></param>
        /// <returns></returns>
        public IOrderable<T> Asc<TKey>(Expression<Func<T, TKey>> keySelector)
        {
            _queryable = (_queryable as IOrderedQueryable<T>)
                .OrderBy(keySelector);
            return this;
        }
        /// <summary>
        /// Ȼ�����
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="keySelector"></param>
        /// <returns></returns>
        public IOrderable<T> ThenAsc<TKey>(Expression<Func<T, TKey>> keySelector)
        {
            _queryable = (_queryable as IOrderedQueryable<T>)
                .ThenBy(keySelector);
            return this;
        }
        /// <summary>
        /// �ݼ�
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="keySelector"></param>
        /// <returns></returns>
        public IOrderable<T> Desc<TKey>(Expression<Func<T, TKey>> keySelector)
        {
            _queryable = _queryable
                .OrderByDescending(keySelector);
            return this;
        }
        /// <summary>
        /// Ȼ��ݼ�
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="keySelector"></param>
        /// <returns></returns>
        public IOrderable<T> ThenDesc<TKey>(Expression<Func<T, TKey>> keySelector)
        {
            _queryable = (_queryable as IOrderedQueryable<T>)
                .ThenByDescending(keySelector);
            return this;
        }
    }
}