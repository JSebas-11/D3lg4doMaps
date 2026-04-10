using D3lg4doMaps.Core.Public.Exceptions;
using D3lg4doMaps.Routes.Public.Enums;

namespace D3lg4doMaps.Routes.Public.Models.Requests.Common;

public sealed class VehicleInfo {
    public VehicleEmissionType EmissionType { get; }

    public VehicleInfo(VehicleEmissionType emissionType) 
        => EmissionType = emissionType;

    internal void ValidateForRequest() {
        if (EmissionType == VehicleEmissionType.Unknown) 
            throw new MapsInvalidRequestException(
                "Unknown VehicleEmissionType value is for internal use only and cannot be sent to the API."
            );
    }
}