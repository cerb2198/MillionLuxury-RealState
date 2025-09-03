using MillionLuxury.RealEstate.Application.UseCases;
using MillionLuxury.RealEstate.Application.Validators;
using MillionLuxury.RealEstate.Domain.Exceptions;
using MillionLuxury.RealEstate.UnitTests.Application.UseCases.Helpers;
using MillionLuxury.RealEstate.UnitTests.Application.UseCases.TestData;
using MillionLuxury.RealEstate.Application.Dtos.Requests;
using Moq;

namespace MillionLuxury.RealEstate.UnitTests.Application.UseCases;

[TestFixture]
public class UpdatePropertyUseCaseTests
{
    private UpdatePropertyMockHelper _mockHelper;
    private UpdatePropertyRequestValidator _validator;
    private UpdatePropertyUseCase _useCase;

    [SetUp]
    public void SetUp()
    {
        _mockHelper = new UpdatePropertyMockHelper();
        _validator = new UpdatePropertyRequestValidator();

        _useCase = new UpdatePropertyUseCase(
            _mockHelper.MockPropertyRepository.Object,
            _mockHelper.MockOwnerRepository.Object,
            _validator,
            _mockHelper.MockMapper.Object,
            _mockHelper.MockLogger.Object
        );
    }

    [Test]
    public async Task ExecuteAsync_ValidFullUpdate_ShouldUpdatePropertySuccessfullyAsync()
    {
        var request = UpdatePropertyTestData.GetValidUpdateRequest();
        var expectedResponse = UpdatePropertyTestData.GetValidResponse();

        _mockHelper.SetupForSuccessfulFullUpdate(request);

        var result = await _useCase.ExecuteAsync(request);

        Assert.That(result, Is.Not.Null);
        Assert.That(result.Id, Is.EqualTo(expectedResponse.Id));
        Assert.That(result.Name, Is.EqualTo(expectedResponse.Name));
        Assert.That(result.Country, Is.EqualTo(expectedResponse.Country));
        Assert.That(result.Price, Is.EqualTo(expectedResponse.Price));
        Assert.That(result.OwnerId, Is.EqualTo(expectedResponse.OwnerId));

        _mockHelper.VerifyPropertyWasRetrieved(request.PropertyId, Times.Once());
        _mockHelper.VerifyOwnerWasValidated(request.OwnerId!.Value, Times.Once());
        _mockHelper.VerifyPropertyNameWasChecked(request.Name!, request.OwnerId!.Value, Times.Once());
        _mockHelper.VerifyPropertyWasUpdated(Times.Once());
    }

    [Test]
    public async Task ExecuteAsync_ValidPartialUpdate_ShouldUpdateOnlyProvidedFieldsAsync()
    {
        var request = UpdatePropertyTestData.GetPartialUpdateRequest();

        _mockHelper.SetupForSuccessfulPartialUpdate(request);

        var result = await _useCase.ExecuteAsync(request);

        Assert.That(result, Is.Not.Null);

        _mockHelper.VerifyPropertyWasRetrieved(request.PropertyId, Times.Once());
        _mockHelper.VerifyNoOwnerValidation();
        _mockHelper.VerifyPropertyWasUpdated(Times.Once());
    }

    [Test]
    public async Task ExecuteAsync_AddressFieldsOnly_ShouldUpdateAddressCorrectlyAsync()
    {
        var request = UpdatePropertyTestData.GetAddressOnlyUpdateRequest();

        _mockHelper.SetupForSuccessfulPartialUpdate(request);

        var result = await _useCase.ExecuteAsync(request);

        Assert.That(result, Is.Not.Null);

        _mockHelper.VerifyPropertyWasRetrieved(request.PropertyId, Times.Once());
        _mockHelper.VerifyNoOwnerValidation();
        _mockHelper.VerifyNoPropertyNameCheck();
        _mockHelper.VerifyPropertyWasUpdated(Times.Once());
    }

    [Test]
    public void ExecuteAsync_PropertyNotFound_ShouldThrowNotFoundException()
    {
        var request = UpdatePropertyTestData.GetRequestForNonExistentProperty();
        _mockHelper.SetupPropertyNotFound(request.PropertyId);

        var exception = Assert.ThrowsAsync<NotFoundException>(
            async () => await _useCase.ExecuteAsync(request)
        );

        Assert.That(exception.Message, Does.Contain("Property"));
        Assert.That(exception.Message, Does.Contain(request.PropertyId.ToString()));

        _mockHelper.VerifyPropertyWasRetrieved(request.PropertyId, Times.Once());
        _mockHelper.VerifyNoOwnerValidation();
        _mockHelper.VerifyNoUpdateOperations();
    }

    [Test]
    public void ExecuteAsync_OwnerNotFound_ShouldThrowNotFoundException()
    {
        var request = UpdatePropertyTestData.GetRequestWithNonExistentOwner();
        
        _mockHelper
            .SetupPropertyExists(request.PropertyId)
            .SetupOwnerNotFound(request.OwnerId!.Value);

        var exception = Assert.ThrowsAsync<NotFoundException>(
            async () => await _useCase.ExecuteAsync(request)
        );

        Assert.That(exception.Message, Does.Contain("Owner"));
        Assert.That(exception.Message, Does.Contain(request.OwnerId.ToString()));

        _mockHelper.VerifyPropertyWasRetrieved(request.PropertyId, Times.Once());
        _mockHelper.VerifyOwnerWasValidated(request.OwnerId.Value, Times.Once());
        _mockHelper.VerifyNoUpdateOperations();
    }

    [Test]
    public void ExecuteAsync_DuplicatePropertyNameForOwner_ShouldThrowDuplicateResourceException()
    {
        var request = UpdatePropertyTestData.GetRequestWithDuplicateName();
        
        _mockHelper
            .SetupPropertyExists(request.PropertyId)
            .SetupPropertyNameExists(request.Name!, 1);

        var exception = Assert.ThrowsAsync<DuplicateResourceException>(
            async () => await _useCase.ExecuteAsync(request)
        );

        Assert.That(exception.Message, Does.Contain("Property"));
        Assert.That(exception.Message, Does.Contain(request.Name));

        _mockHelper.VerifyPropertyWasRetrieved(request.PropertyId, Times.Exactly(2));
        _mockHelper.VerifyPropertyNameWasChecked(request.Name!, 1, Times.Once());
        _mockHelper.VerifyNoUpdateOperations();
    }

    [Test]
    public async Task ExecuteAsync_SameOwnerSameName_ShouldNotCheckDuplicateNameAsync()
    {
        var existingProperty = UpdatePropertyTestData.GetExistingProperty();
        var request = new UpdatePropertyRequest(
            PropertyId: existingProperty.Id,
            Name: existingProperty.Name,
            Country: null,
            City: null,
            Street: null,
            ZipCode: null,
            Price: 3000000m,
            Year: null,
            OwnerId: null
        );

        _mockHelper.SetupForSuccessfulPartialUpdate(request);

        var result = await _useCase.ExecuteAsync(request);

        Assert.That(result, Is.Not.Null);

        _mockHelper.VerifyPropertyWasRetrieved(request.PropertyId, Times.Once());
        _mockHelper.VerifyNoPropertyNameCheck();
        _mockHelper.VerifyPropertyWasUpdated(Times.Once());
    }

    [Test]
    public async Task ExecuteAsync_OwnerChangeWithSameName_ShouldValidateNewOwnerAsync()
    {
        var existingProperty = UpdatePropertyTestData.GetExistingProperty();
        var request = new UpdatePropertyRequest(
            PropertyId: existingProperty.Id,
            Name: existingProperty.Name,
            Country: null,
            City: null,
            Street: null,
            ZipCode: null,
            Price: null,
            Year: null,
            OwnerId: 2
        );

        _mockHelper
            .SetupPropertyExists(request.PropertyId)
            .SetupOwnerExists(request.OwnerId!.Value)
            .SetupSuccessfulUpdate();

        var result = await _useCase.ExecuteAsync(request);

        Assert.That(result, Is.Not.Null);

        _mockHelper.VerifyPropertyWasRetrieved(request.PropertyId, Times.Once());
        _mockHelper.VerifyOwnerWasValidated(request.OwnerId.Value, Times.Once());
        _mockHelper.VerifyNoPropertyNameCheck();
        _mockHelper.VerifyPropertyWasUpdated(Times.Once());
    }
}
