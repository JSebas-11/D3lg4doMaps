namespace D3lg4doMaps.Routes.Public.Enums;

public enum RouteDetailLevel {
    Summary,
    Standard,
    Full
}

public enum RoutingPreference {
    TrafficUnaware,
    TrafficAware,
    TrafficAwareOptimal,
    Unknown
}

public enum RouteElementCondition {
    RouteExists,
    RouteNotFound,
    Unknown
}