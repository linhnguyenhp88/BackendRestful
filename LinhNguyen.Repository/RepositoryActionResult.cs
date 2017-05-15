using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinhNguyen.Repository
{
    public class RepositoryActionResult<T> where T : class
    {
        public RepositoryActionResult(T entity, RepositoryActionStatus status, Exception exception) : this(entity, status)
        {
            Exception = exception;
        }

        public RepositoryActionResult(T entity, RepositoryActionStatus status)
        {
            Entity = entity;
            Status = status;
        }
        public T Entity { get; set; }

        public RepositoryActionStatus Status { get; set; }

        public Exception Exception { get; set; }


    }
}
