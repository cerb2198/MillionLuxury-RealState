using Moq;
using AutoMapper;
using Microsoft.Extensions.Logging;
using MillionLuxury.RealEstate.Application.Interfaces.Repositories;
using MillionLuxury.RealEstate.Application.Dtos.Requests;
using MillionLuxury.RealEstate.Application.Dtos.Responses;
using MillionLuxury.RealEstate.Domain.Entities;
using MillionLuxury.RealEstate.UnitTests.Application.UseCases.TestData;
using MillionLuxury.RealEstate.Application.UseCases;
using MillionLuxury.RealEstate.Domain.ValueObjects;

namespace MillionLuxury.RealEstate.UnitTests.Application.UseCases.Helpers;

public class UpdatePropertyMockHelper
{
    public Mock<IPropertyRepository> MockPropertyRepository { get; }
    public Mock<IOwnerRepository> MockOwnerRepository { get; }
    public Mock<IMapper> MockMapper { get; }
    public Mock<ILogger<UpdatePropertyUseCase>> MockLogger { get; }

    public UpdatePropertyMockHelper()
    {
        MockPropertyRepository = new Mock<IPropertyRepository>();
        MockOwnerRepository = new Mock<IOwnerRepository>();
        MockMapper = new Mock<IMapper>();
        MockLogger = new Mock<ILogger<UpdatePropertyUseCase>>();
    }

    public UpdatePropertyMockHelper SetupPropertyExists(int propertyId)
    {
        MockPropertyRepository
            .Setup(x => x.GetByIdAsync(propertyId))
            .ReturnsAsync(UpdatePropertyTestData.GetExistingProperty());
        return this;
    }

    public UpdatePropertyMockHelper SetupPropertyNotFound(int propertyId)
    {
        MockPropertyRepository
            .Setup(x => x.GetByIdAsync(propertyId))
            .ReturnsAsync((Property?)null);
        return this;
    }

    public UpdatePropertyMockHelper SetupOwnerExists(int ownerId)
    {
        MockOwnerRepository
            .Setup(x => x.GetByIdAsync(ownerId))
            .ReturnsAsync(UpdatePropertyTestData.GetValidOwner());
        return this;
    }

    public UpdatePropertyMockHelper SetupOwnerNotFound(int ownerId)
    {
        MockOwnerRepository
            .Setup(x => x.GetByIdAsync(ownerId))
            .ReturnsAsync((Owner?)null);
        return this;
    }

    public UpdatePropertyMockHelper SetupPropertyNameUnique(string name, int ownerId)
    {
        MockPropertyRepository
            .Setup(x => x.ExistsByNameAndOwnerIdAsync(name, ownerId))
            .ReturnsAsync(false);
        return this;
    }

    public UpdatePropertyMockHelper SetupPropertyNameExists(string name, int ownerId)
    {
        MockPropertyRepository
            .Setup(x => x.ExistsByNameAndOwnerIdAsync(name, ownerId))
            .ReturnsAsync(true);
        return this;
    }

    public UpdatePropertyMockHelper SetupSuccessfulUpdate()
    {
        var updatedProperty = UpdatePropertyTestData.GetUpdatedProperty();
        var expectedResponse = UpdatePropertyTestData.GetValidResponse();

        MockPropertyRepository
            .Setup(x => x.UpdateAsync(It.IsAny<Property>()))
            .ReturnsAsync(updatedProperty);

        MockPropertyRepository
            .Setup(x => x.SaveChangesAsync())
            .Returns(Task.CompletedTask);

        MockMapper
            .Setup(x => x.Map<UpdatePropertyResponse>(It.IsAny<Property>()))
            .Returns(expectedResponse);

        return this;
    }

    public UpdatePropertyMockHelper SetupForSuccessfulFullUpdate(UpdatePropertyRequest request)
    {
        return SetupPropertyExists(request.PropertyId)
            .SetupOwnerExists(request.OwnerId!.Value)
            .SetupPropertyNameUnique(request.Name!, request.OwnerId!.Value)
            .SetupSuccessfulUpdate();
    }

    public UpdatePropertyMockHelper SetupForSuccessfulPartialUpdate(UpdatePropertyRequest request)
    {
        return SetupPropertyExists(request.PropertyId)
            .SetupSuccessfulUpdate();
    }

    public void VerifyPropertyWasRetrieved(int propertyId, Times times)
    {
        MockPropertyRepository.Verify(x => x.GetByIdAsync(propertyId), times);
    }

    public void VerifyOwnerWasValidated(int ownerId, Times times)
    {
        MockOwnerRepository.Verify(x => x.GetByIdAsync(ownerId), times);
    }

    public void VerifyPropertyNameWasChecked(string name, int ownerId, Times times)
    {
        MockPropertyRepository.Verify(x => x.ExistsByNameAndOwnerIdAsync(name, ownerId), times);
    }

    public void VerifyPropertyWasUpdated(Times times)
    {
        MockPropertyRepository.Verify(x => x.UpdateAsync(It.IsAny<Property>()), times);
        MockPropertyRepository.Verify(x => x.SaveChangesAsync(), times);
    }

    public void VerifyNoOwnerValidation()
    {
        MockOwnerRepository.Verify(x => x.GetByIdAsync(It.IsAny<int>()), Times.Never);
    }

    public void VerifyNoPropertyNameCheck()
    {
        MockPropertyRepository.Verify(x => x.ExistsByNameAndOwnerIdAsync(It.IsAny<string>(), It.IsAny<int>()), Times.Never);
    }

    public void VerifyNoUpdateOperations()
    {
        MockPropertyRepository.Verify(x => x.UpdateAsync(It.IsAny<Property>()), Times.Never);
        MockPropertyRepository.Verify(x => x.SaveChangesAsync(), Times.Never);
    }
}
