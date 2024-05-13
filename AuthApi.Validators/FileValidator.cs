using AuthApi.Domain.Adapters.Validators;
using FileTypeChecker.Extensions;
using FileTypeChecker.Types;

namespace AuthApi.Validators;

public class FileValidator : IFileValidator
{
    public bool IsImage(Stream image)
    {
        var validationResult = image.Is<JointPhotographicExpertsGroup>()
            || image.Is<GraphicsInterchangeFormat87>()
            || image.Is<GraphicsInterchangeFormat89>()
            || image.Is<PortableNetworkGraphic>();

        return validationResult;
    }
}
