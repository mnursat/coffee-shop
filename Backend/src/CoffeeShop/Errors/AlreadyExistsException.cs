namespace CoffeeShop.Errors;

public class AlreadyExistsException(string message)
    : ServiceException(StatusCodes.Status409Conflict, message)
{
}
