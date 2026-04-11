namespace D3lg4doMaps.Routes.Public.Models.Requests.Common;

/// <summary>
/// Represents routing modifiers used to influence route calculation.
/// </summary>
/// <remarks>
/// Allows configuring constraints such as avoiding tolls, highways, or ferries,
/// as well as specifying vehicle-related information.
///
/// Instances are created using <see cref="Create"/> and configured via fluent methods.
/// </remarks>
public sealed class RouteModifiers {
    /// <summary>
    /// Gets whether toll roads should be avoided.
    /// </summary>
    public bool AvoidTolls { get; private set; } = false;

    /// <summary>
    /// Gets whether highways should be avoided.
    /// </summary>
    public bool AvoidHighways { get; private set; } = false;

    /// <summary>
    /// Gets whether ferries should be avoided.
    /// </summary>
    public bool AvoidFerries { get; private set; } = false;

    /// <summary>
    /// Gets whether indoor navigation used for routing.
    /// </summary>
    public bool AvoidIndoor { get; private set; } = false;
    
    /// <summary>
    /// Gets vehicle-specific information used for routing.
    /// </summary>
    public VehicleInfo? VehicleInfo { get; private set; }

    private RouteModifiers() { }

    internal void ValidateForRequest() => VehicleInfo?.ValidateForRequest();

    /// <summary>
    /// Creates a new instance of <see cref="RouteModifiers"/>.
    /// </summary>
    /// <returns>
    /// A new <see cref="RouteModifiers"/> instance.
    /// </returns>
    public static RouteModifiers Create() => new ();

    /// <summary>
    /// Sets vehicle-specific routing information.
    /// </summary>
    /// <param name="vehicleInfo">
    /// The vehicle information to apply.
    /// </param>
    /// <returns>
    /// The current <see cref="RouteModifiers"/> instance.
    /// </returns>
    public RouteModifiers WithVehicleInfo(VehicleInfo vehicleInfo) {
        VehicleInfo = vehicleInfo;
        return this;
    }

    /// <summary>
    /// Configures the route to avoid toll roads.
    /// </summary>
    public RouteModifiers NoTolls() {
        AvoidTolls = true;
        return this;
    }

    /// <summary>
    /// Configures the route to avoid highways.
    /// </summary>
    public RouteModifiers NoHighways() {
        AvoidHighways = true;
        return this;
    }

    /// <summary>
    /// Configures the route to avoid ferries.
    /// </summary>
    public RouteModifiers NoFerries() {
        AvoidFerries = true;
        return this;
    }

    /// <summary>
    /// Configures the route to avoid indoor navigation.
    /// </summary>
    public RouteModifiers NoIndoor() {
        AvoidIndoor = true;
        return this;
    }
}