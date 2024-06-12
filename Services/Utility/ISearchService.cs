namespace Services.Utility
{
    public interface ISearchService
    {
        List<object> Search(string searchText);

        Task<object> SearchProduct(string searchText);

        List<object> SearchDiamond(string searchText);
    }
}
