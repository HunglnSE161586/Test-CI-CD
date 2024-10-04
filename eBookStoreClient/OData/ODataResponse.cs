namespace eBookStoreClient.OData
{
    public class ODataResponse<T>
    {
        public string OdataContext { get; set; }
        public List<T> Value { get; set; }
    }
}
