namespace Services.Database.DummyService

open Model

type private ISqlBroker = DI.Brokers.SqlDI.ISqlBroker

type Service () =

    //------------------------------------------------------------------------------------------------------------------
    static let mutable _companyId = 0
    //------------------------------------------------------------------------------------------------------------------

    //------------------------------------------------------------------------------------------------------------------
    static member init companyId = _companyId <- companyId
    //------------------------------------------------------------------------------------------------------------------
