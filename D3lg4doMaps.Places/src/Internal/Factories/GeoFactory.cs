using D3lg4doMaps.Core.Public.Exceptions;
using D3lg4doMaps.Core.Public.Models.Geometry;
using D3lg4doMaps.Places.Public.Models.Geometry;

namespace D3lg4doMaps.Places.Internal.Factories;

internal static class GeoFactory {
    public static GeoCircle CreateCircle(
        double radius, double latitude, double longitude 
    ) {
        if (radius <= 0) throw new MapsInvalidRequestException("Radius must be greater than 0.");
        if (latitude is < -90 or > 90) throw new MapsInvalidRequestException("Latitude must be between -90 and 90.");
        if (longitude is < -180 or > 180) throw new MapsInvalidRequestException("Longitude must be between -180 and 180.");  
        
        return new () {
            Center = new LatLng(latitude, longitude),
            Radius = radius
        };
    }
}