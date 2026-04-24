using DelgadoMaps.Core.Exceptions;
using DelgadoMaps.Routes.Enums;

namespace DelgadoMaps.Routes.Models.Requests.Common;

/// <summary>
/// Represents vehicle-specific information used for route calculation.
/// </summary>
/// <remarks>
/// This is primarily used to influence routing decisions such as emissions-based restrictions.
/// </remarks>
public sealed class VehicleInfo {
    /// <summary>
    /// Gets the emission type of the vehicle.
    /// </summary>
    public VehicleEmissionType EmissionType { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="VehicleInfo"/> class.
    /// </summary>
    /// <param name="emissionType">
    /// The emission type of the vehicle.
    /// </param>
    public VehicleInfo(VehicleEmissionType emissionType) 
        => EmissionType = emissionType;

    internal void ValidateForRequest() {
        if (EmissionType == VehicleEmissionType.Unknown) 
            throw new MapsInvalidRequestException(
                "Unknown VehicleEmissionType value is for internal use only and cannot be sent to the API."
            );
    }
}