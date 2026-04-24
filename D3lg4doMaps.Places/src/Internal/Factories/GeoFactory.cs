using DelgadoMaps.Core.Exceptions;
using DelgadoMaps.Core.Models.Geometry;
using DelgadoMaps.Places.Models.Geometry;

namespace DelgadoMaps.Places.Internal.Factories;

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