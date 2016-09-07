using Architecture.Core;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Projects.Account
{
    public class UserQuery : IRequest<IEnumerable<User>>
    {
        public Expression<Func<User, bool>> Expression { get; }

        public UserQuery()
        {
            Expression = _ => true;
        }

        public UserQuery(Expression<Func<User, bool>> expression)
        {
            Expression = expression;
        }
    }
}
