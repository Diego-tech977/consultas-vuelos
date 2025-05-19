const BuscadorVuelos = () => {
    return (
        <>
            <div className="h1">Buscador de Vuelos</div>
            <div className="card mt-4"></div>
                <div className="card-header">Búsqueda de vuelos</div>
                <div className="card-body">
                    <div className="row">
                        <div className="col-sm-6"></div>
                            <div className="mb-3">
                                <label>Fecha Inicial</label>
                                <input type="date" className="for-control" />
                            </div>
                        </div >
                    </div >
                </div >
            </div>
        </>
    )
}

export default BuscadorVuelos;