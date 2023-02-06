namespace Model

open CommandLine

type ArgumentOptions = {
    [<Option ('h', "host", Required = true)>]
    Host : string

    [<Option ('p', "port", Default=5432)>]
    Port : int

    [<Value (0, MetaName="network", Required = true)>]
    Network : string
}
