using CMSys.Core.Exceptions;
using Microsoft.Data.SqlClient;

namespace CMSys.Infrastructure.Helpers;

internal static class SqlExceptionHelper
{
    internal static RepositoryException ToRepositoryException(this SqlException sqlException)
    {
        var repositoryException = new RepositoryException(sqlException.Message, sqlException);
        foreach (SqlError error in sqlException.Errors)
        {
            var text = $"[#{error.Number}] Message: {error.Message}, " +
                       $"LineNumber: {error.LineNumber}, " +
                       $"Source: {error.Source}, " +
                       $"Procedure: {error.Procedure}";
            repositoryException.Errors.Add(text);
        }

        return repositoryException;
    }
}