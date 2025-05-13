using Microsoft.AspNetCore.Mvc; 
using MongoDB.Driver;

[Route("api/vuelos")]

public class VuelosController : ControllerBase {

    [HttpGet("ciudades-origen")]
    public IActionResult CiudadesOrigen(){
        var client = new MongoClient(CadenaConexion.MONGO_DB);
        var db = client.GetDatabase("Aeropuerto");
        var collection = db.GetCollection<Vuelos>("Vuelos");

        var lista = collection.Distinct<string>("CiudadOrigen", FilterDefinition<Vuelos>.Empty).ToList();

        return Ok(lista);
    }

    [HttpGet("ciudades-destino")]
    public IActionResult CiudadesDestino(){
         var client = new MongoClient(CadenaConexion.MONGO_DB);
        var db = client.GetDatabase("Aeropuerto");
        var collection = db.GetCollection<Vuelos>("Vuelos");

        var lista = collection.Distinct<string>("CiudadDestino", FilterDefinition<Vuelos>.Empty).ToList();

        return Ok(lista);



    }

    [HttpGet("estatus")]
    public IActionResult ListarEstatus(){
         var client = new MongoClient(CadenaConexion.MONGO_DB);
        var db = client.GetDatabase("Aeropuerto");
        var collection = db.GetCollection<Vuelos>("Vuelos");

        var lista = collection.Distinct<string>("EstatusVuelo", FilterDefinition<Vuelos>.Empty).ToList();
        
        return Ok();


    }    

    [HttpGet("listar-vuelos")]
    public IActionResult ListarVuelos(string? estatus, string? origen, string? destino, string? fechaInicial, string? fechaFinal){
         var client = new MongoClient(CadenaConexion.MONGO_DB);
        var db = client.GetDatabase("Aeropuerto");
        var collection = db.GetCollection<Vuelos>("Vuelos");

        List<FilterDefinition<Vuelos>> filters = new List<FilterDefinition<Vuelos>>();

        if(!string.IsNullOrWhiteSpace(estatus)){
            var filterEstatus = Builders<Vuelos>.Filter.Eq(x => x.EstatusVuelo, estatus);
            filters.Add(filterEstatus);
        }

        if(!string.IsNullOrWhiteSpace(origen)){
            var filterOrigen  = Builders<Vuelos>.Filter.Eq(x => x.CiudadOrigen, origen);
            filters.Add(filterOrigen);
        }

         if(!string.IsNullOrWhiteSpace(destino)){
            var filterDestino = Builders<Vuelos>.Filter.Eq(x => x.CiudadDestino, destino);
            filters.Add(filterDestino);
        }

        if(!string.IsNullOrWhiteSpace(fechaInicial)){
            if(DateTime.TryParse(fechaInicial, out DateTime fecha)){
            var filtroFechaIni = Builders<Vuelos>.Filter.Gte(x => x.FechaHoraSalida, fecha);
            filters.Add(filtroFechaIni);
            
            }

        }

        if(!string.IsNullOrWhiteSpace(fechaFinal)){
            if(DateTime.TryParse(fechaFinal, out DateTime fecha)){
            var filtroFechaFin = Builders<Vuelos>.Filter.Lte(x => x.FechaHoraSalida,
                new DateTime(fecha.Year, fecha.Month, fecha.Day, 23, 59, 59));
            filters.Add(filtroFechaFin);
        
            }


        }

        List<Vuelos> vuelos;
        if(filters.Count > 0) {
            var filter = Builders<Vuelos>.Filter.And(filters);
            vuelos = collection.Find(filter).ToList();
        }
        else{
            vuelos = collection.Find(FilterDefinition<Vuelos>.Empty).ToList();
        } 


        return Ok(vuelos);
          
   }
}
