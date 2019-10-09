using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Common.Search
{
    public interface ISearchable
    {
        /// <summary>
        /// Matches the object with the search string. Negative numbers indicate no match. 0-1 indicates a match. 1 is a perferct match, 0 is a horrible match. The implementation decides whether or not to use the sliding scale.
        /// </summary>
        /// <param name="searchString"></param>
        /// <returns></returns>
        double Match(string searchString);
        Guid Id { get; set; }

    }
}
