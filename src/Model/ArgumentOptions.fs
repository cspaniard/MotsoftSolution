namespace Model

open CommandLine

type ArgumentOptions = {
    [<Option ('h', "host", Required = true)>]
    Host : string

    [<Option ('p', "port", Default=5432)>]
    Port : int

    [<Option ('U', "user", Required = true)>]
    User : string

    [<Option ('W', "password", Required = true)>]
    Password : string

    [<Option ('d', "database", Required = true)>]
    Database : string

    [<Option ('c', "company-id", Required = true)>]
    CompanyId : int
}
