namespace BooksApp.Infrastructure.Data
{
    public static class CustomPaging
    {
        public static IQueryable<T> Page<T>(this IQueryable<T> query, int pageNumberZeroStart, int pageSize)
        {
            if (pageSize == 0)
            {
                throw new ArgumentException("sayfadaki eleman sayısı 0 olamaz", nameof(pageSize));
            }
            /*
             * 1. sayfa
             * 0 atla -> 3 al
             * 2. sayfa:
             * 3 atla -> 3 al
             * 
             */
            if (pageNumberZeroStart != 0)
            {
                query = query.Skip(pageNumberZeroStart * pageSize);
            }

            return query.Take(pageSize);
        }
    }
}
