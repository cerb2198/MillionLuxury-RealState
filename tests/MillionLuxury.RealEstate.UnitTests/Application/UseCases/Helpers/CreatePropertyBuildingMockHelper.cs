using Moq;
using AutoMapper;
using Microsoft.Extensions.Logging;
using MillionLuxury.RealEstate.Application.Interfaces.Repositories;
using MillionLuxury.RealEstate.Application.Dtos.Requests;
using MillionLuxury.RealEstate.Application.Dtos.Responses;
using MillionLuxury.RealEstate.Domain.Entities;
using MillionLuxury.RealEstate.UnitTests.Application.UseCases.TestData;
using MillionLuxury.RealEstate.Application.UseCases;

namespace MillionLuxury.RealEstate.UnitTests.Application.UseCases.Helpers;

public class CreatePropertyBuildingMockHelper
{
    public Mock<IPropertyRepository> MockPropertyRepository { get; }
    public Mock<IOwnerRepository> MockOwnerRepository { get; }
    public Mock<IMapper> MockMapper { get; }
    public Mock<ILogger<CreatePropertyBuildingUseCase>> MockLogger { get; }

    public CreatePropertyBuildingMockHelper()
    {
        MockPropertyRepository = new Mock<IPropertyRepository>();
        MockOwnerRepository = new Mock<IOwnerRepository>();
        MockMapper = new Mock<IMapper>();
        MockLogger = new Mock<ILogger<CreatePropertyBuildingUseCase>>();
    }

    public CreatePropertyBuildingMockHelper SetupValidOwner(int ownerId)
    {
        MockOwnerRepository
            .Setup(x => x.GetByIdAsync(ownerId))
            .ReturnsAsync(CreatePropertyBuildingTestData.GetValidOwner());
        return this;
    }

    public CreatePropertyBuildingMockHelper SetupOwnerNotFound(int ownerId)
    {
        MockOwnerRepository
            .Setup(x => x.GetByIdAsync(ownerId))
            .ReturnsAsync((Owner?)null);
        return this;
    }

    public CreatePropertyBuildingMockHelper SetupPropertyNameUnique(string name, int ownerId)
    {
        MockPropertyRepository
            .Setup(x => x.ExistsByNameAndOwnerIdAsync(name, ownerId))
            .ReturnsAsync(false);
        return this;
    }

    public CreatePropertyBuildingMockHelper SetupPropertyNameExists(string name, int ownerId)
    {
        MockPropertyRepository
            .Setup(x => x.ExistsByNameAndOwnerIdAsync(name, ownerId))
            .ReturnsAsync(true);
        return this;
    }

    public CreatePropertyBuildingMockHelper SetupCodeInternalUnique(int codeInternal)
    {
        MockPropertyRepository
            .Setup(x => x.ExistsByCodeInternalAsync(codeInternal))
            .ReturnsAsync(false);
        return this;
    }

    public CreatePropertyBuildingMockHelper SetupCodeInternalExists(int codeInternal)
    {
        MockPropertyRepository
            .Setup(x => x.ExistsByCodeInternalAsync(codeInternal))
            .ReturnsAsync(true);
        return this;
    }

    public CreatePropertyBuildingMockHelper SetupSuccessfulMapping()
    {
        var property = CreatePropertyBuildingTestData.GetValidProperty();
        var expectedResponse = CreatePropertyBuildingTestData.GetValidResponse();

        MockMapper
            .Setup(x => x.Map<Property>(It.IsAny<CreatePropertyBuildingRequest>()))
            .Returns(property);

        MockMapper
            .Setup(x => x.Map<CreatePropertyBuildingResponse>(It.IsAny<Property>()))
            .Returns(expectedResponse);

        return this;
    }

    public CreatePropertyBuildingMockHelper SetupSuccessfulPropertyCreation()
    {
        var createdProperty = CreatePropertyBuildingTestData.GetCreatedProperty();

        MockPropertyRepository
            .Setup(x => x.AddAsync(It.IsAny<Property>()))
            .ReturnsAsync(createdProperty);

        MockPropertyRepository
            .Setup(x => x.SaveChangesAsync())
            .Returns(Task.CompletedTask);

        return this;
    }

    public CreatePropertyBuildingMockHelper SetupForSuccessfulCreation(CreatePropertyBuildingRequest request)
    {
        return SetupValidOwner(request.OwnerId)
            .SetupPropertyNameUnique(request.Name, request.OwnerId)
            .SetupCodeInternalUnique(request.CodeInternal)
            .SetupSuccessfulMapping()
            .SetupSuccessfulPropertyCreation();
    }

    public void VerifyOwnerWasValidated(int ownerId, Times times)
    {
        MockOwnerRepository.Verify(x => x.GetByIdAsync(ownerId), times);
    }

    public void VerifyPropertyNameWasChecked(string name, int ownerId, Times times)
    {
        MockPropertyRepository.Verify(x => x.ExistsByNameAndOwnerIdAsync(name, ownerId), times);
    }

    public void VerifyCodeInternalWasChecked(int codeInternal, Times times)
    {
        MockPropertyRepository.Verify(x => x.ExistsByCodeInternalAsync(codeInternal), times);
    }

    public void VerifyPropertyWasCreated(Times times)
    {
        MockPropertyRepository.Verify(x => x.AddAsync(It.IsAny<Property>()), times);
        MockPropertyRepository.Verify(x => x.SaveChangesAsync(), times);
    }

    public void VerifyNoPropertyOperations()
    {
        MockPropertyRepository.Verify(x => x.ExistsByNameAndOwnerIdAsync(It.IsAny<string>(), It.IsAny<int>()), Times.Never);
        MockPropertyRepository.Verify(x => x.ExistsByCodeInternalAsync(It.IsAny<int>()), Times.Never);
        MockPropertyRepository.Verify(x => x.AddAsync(It.IsAny<Property>()), Times.Never);
    }
}
