public interface IImageProcessor
{
    Task ProcessAsync(ImageFile imageFile, CancellationToken cancellationToken);
}