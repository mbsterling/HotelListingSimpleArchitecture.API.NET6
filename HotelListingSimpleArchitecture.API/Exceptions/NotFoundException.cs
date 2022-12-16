namespace HotelListing.API.Exceptions
{
    public class NotFoundException : ApplicationException
    {
        public NotFoundException(string name, object key) : 
            base($"Name {name} and key: {key} was not found.")
        {

        }
    }
}
