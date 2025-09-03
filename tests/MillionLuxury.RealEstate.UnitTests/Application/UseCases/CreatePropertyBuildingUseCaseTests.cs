using MillionLuxury.RealEstate.Application.UseCases;
using MillionLuxury.RealEstate.Application.Validators;
using MillionLuxury.RealEstate.Domain.Exceptions;
using MillionLuxury.RealEstate.UnitTests.Application.UseCases.Helpers;
using MillionLuxury.RealEstate.UnitTests.Application.UseCases.TestData;
using Moq;
using System.ComponentModel.DataAnnotations;

namespace MillionLuxury.RealEstate.UnitTests.Application.UseCases;

[TestFixture]
public class CreatePropertyBuildingUseCaseTests
{
    private CreatePropertyBuildingMockHelper _mockHelper;
    private CreatePropertyBuildingRequestValidator _validator;
    private CreatePropertyBuildingUseCase _useCase;

    [SetUp]
    public void SetUp()
    {
        _mockHelper = new CreatePropertyBuildingMockHelper();
        _validator = new CreatePropertyBuildingRequestValidator();

        _useCase = new CreatePropertyBuildingUseCase(
            _mockHelper.MockPropertyRepository.Object,
            _mockHelper.MockOwnerRepository.Object,
            _validator,
            _mockHelper.MockMapper.Object,
            _mockHelper.MockLogger.Object
        );
    }

    [Test]
    public async Task ExecuteAsync_ValidRequest_ShouldCreatePropertySuccessfullyAsync()
    {
        var request = CreatePropertyBuildingTestData.GetValidRequest();
        var expectedResponse = CreatePropertyBuildingTestData.GetValidResponse();

        _mockHelper.SetupForSuccessfulCreation(request);

        var result = await _useCase.ExecuteAsync(request);

        Assert.That(result, Is.Not.Null);
        Assert.That(result.Id, Is.EqualTo(expectedResponse.Id));
        Assert.That(result.Name, Is.EqualTo(expectedResponse.Name));

        _mockHelper.VerifyOwnerWasValidated(request.OwnerId, Times.Once());
        _mockHelper.VerifyPropertyNameWasChecked(request.Name, request.OwnerId, Times.Once());
        _mockHelper.VerifyCodeInternalWasChecked(request.CodeInternal, Times.Once());
        _mockHelper.VerifyPropertyWasCreated(Times.Once());
    }

    [Test]
    public void ExecuteAsync_OwnerNotFound_ShouldThrowNotFoundException()
    {
        var request = CreatePropertyBuildingTestData.GetValidRequest();
        _mockHelper.SetupOwnerNotFound(request.OwnerId);

        var exception = Assert.ThrowsAsync<NotFoundException>(
            async () => await _useCase.ExecuteAsync(request)
        );

        Assert.That(exception.Message, Does.Contain("Owner"));
        Assert.That(exception.Message, Does.Contain(request.OwnerId.ToString()));

        _mockHelper.VerifyOwnerWasValidated(request.OwnerId, Times.Once());
        _mockHelper.VerifyNoPropertyOperations();
    }

    [Test]
    public void ExecuteAsync_PropertyNameAlreadyExists_ShouldThrowDuplicateResourceException()
    {
        var request = CreatePropertyBuildingTestData.GetValidRequest();

        _mockHelper
            .SetupValidOwner(request.OwnerId)
            .SetupPropertyNameExists(request.Name, request.OwnerId);

        var exception = Assert.ThrowsAsync<DuplicateResourceException>(
            async () => await _useCase.ExecuteAsync(request)
        );

        Assert.That(exception.Message, Does.Contain("Property"));
        Assert.That(exception.Message, Does.Contain(request.Name));

        _mockHelper.VerifyOwnerWasValidated(request.OwnerId, Times.Once());
        _mockHelper.VerifyPropertyNameWasChecked(request.Name, request.OwnerId, Times.Once());
        _mockHelper.VerifyCodeInternalWasChecked(request.CodeInternal, Times.Never());
    }

    [Test]
    public void ExecuteAsync_CodeInternalAlreadyExists_ShouldThrowDuplicateResourceException()
    {
        var request = CreatePropertyBuildingTestData.GetValidRequest();

        _mockHelper
            .SetupValidOwner(request.OwnerId)
            .SetupPropertyNameUnique(request.Name, request.OwnerId)
            .SetupCodeInternalExists(request.CodeInternal);

        var exception = Assert.ThrowsAsync<DuplicateResourceException>(
            async () => await _useCase.ExecuteAsync(request)
        );

        Assert.That(exception.Message, Does.Contain("Property"));
        Assert.That(exception.Message, Does.Contain(request.CodeInternal.ToString()));

        _mockHelper.VerifyOwnerWasValidated(request.OwnerId, Times.Once());
        _mockHelper.VerifyPropertyNameWasChecked(request.Name, request.OwnerId, Times.Once());
        _mockHelper.VerifyCodeInternalWasChecked(request.CodeInternal, Times.Once());
    }
}
